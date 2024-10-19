using bwms_core_domain.AuthorityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bwms_core_unit_test
{
    public class EmployeeTest
    {
        [Fact]
        public void Employee_DefaultValues_ShouldBeInitializedCorrectly()
        {
            var employee = new Employee();

            Assert.NotNull(employee.EmployeeDevices);
            Assert.Equal(0, employee.EmployeeId);
            Assert.Equal(string.Empty, employee.FullName);
            Assert.Equal(string.Empty, employee.Email);
            Assert.Equal(string.Empty, employee.EmployeeCode);
            Assert.Equal(string.Empty, employee.Phone);
        }

        [Fact]
        public void Employee_ShouldBeAbleToSetProperties()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                EmployeeCode = "E001",
                Phone = "123-456-7890"
            };

            Assert.Equal(1, employee.EmployeeId);
            Assert.Equal("John Doe", employee.FullName);
            Assert.Equal("john.doe@example.com", employee.Email);
            Assert.Equal("E001", employee.EmployeeCode);
            Assert.Equal("123-456-7890", employee.Phone);
        }

        [Fact]
        public void Employee_DefaultValues_ShouldFailDueToIncorrectExpectations()
        {
            var employee = new Employee();

            Assert.Null(employee.EmployeeDevices);
            Assert.Equal(-1, employee.EmployeeId);
            Assert.Equal("Unknown", employee.FullName);
            Assert.Equal("unknown@example.com", employee.Email);
        }

        [Fact]
        public void Employee_ShouldFailDueToIncorrectPropertyValues()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                EmployeeCode = "E001",
                Phone = "123-456-7890"
            };

            Assert.Equal(2, employee.EmployeeId);
            Assert.Equal("Jane Doe", employee.FullName);
            Assert.Equal("jane.doe@example.com", employee.Email);
            Assert.Equal("E002", employee.EmployeeCode);
            Assert.Equal("000-000-0000", employee.Phone);
        }


    }
}
