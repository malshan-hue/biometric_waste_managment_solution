using bwms_core_domain.ResidentsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bwms_core_unit_test
{
    public class CustomerTest
    {
        [Fact]
        public void Customer_DefaultValues_ShouldBeInitializedCorrectly()
        {
            var customer = new Customer();

            Assert.Equal(0, customer.CustomerId);
            Assert.Equal(string.Empty, customer.CustomerName);
            Assert.Equal(string.Empty, customer.CustomerEmail);
            Assert.Equal(string.Empty, customer.CustomerPhone);
            Assert.Equal(0, customer.CustomerLocationLatitude);
            Assert.Equal(0, customer.CustomerLocationLongitude);
        }

        [Fact]
        public void Customer_ShouldBeAbleToSetProperties()
        {
            var customer = new Customer
            {
                CustomerId = 101,
                CustomerName = "Alice Smith",
                CustomerEmail = "alice.smith@example.com",
                CustomerPhone = "555-1234",
                CustomerLocationLatitude = 37.7749,
                CustomerLocationLongitude = -122.4194
            };

            Assert.Equal(101, customer.CustomerId);
            Assert.Equal("Alice Smith", customer.CustomerName);
            Assert.Equal("alice.smith@example.com", customer.CustomerEmail);
            Assert.Equal("555-1234", customer.CustomerPhone);
            Assert.Equal(37.7749, customer.CustomerLocationLatitude);
            Assert.Equal(-122.4194, customer.CustomerLocationLongitude);
        }

        [Fact]
        public void Customer_ShouldFailForInvalidEmailFormat()
        {
            var customer = new Customer
            {
                CustomerEmail = "invalid-email-format"
            };

            Assert.Contains("@", customer.CustomerEmail);
        }

        [Fact]
        public void Customer_LocationCoordinates_ShouldFailForOutOfRangeValues()
        {
            var customer = new Customer
            {
                CustomerLocationLatitude = 100,
                CustomerLocationLongitude = 200 
            };

            Assert.InRange(customer.CustomerLocationLatitude, -90, 90);
            Assert.InRange(customer.CustomerLocationLongitude, -180, 180);
        }

        [Fact]
        public void Customer_ShouldHaveEmptyStringsByDefault()
        {
            var customer = new Customer();

            Assert.Equal(string.Empty, customer.CustomerName);
            Assert.Equal(string.Empty, customer.CustomerEmail);
            Assert.Equal(string.Empty, customer.CustomerPhone);
        }


    }
}
