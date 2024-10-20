using bwms_core_domain.ResidentsModels;
using bwms_core_domain.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bwms_core_unit_test
{
    public class WastePickupScheduleTests
    {
        [Fact]
        public void WastePickupSchedule_DefaultValues_ShouldBeInitializedCorrectly()
        {
            var schedule = new WastePickupSchedule();

            Assert.Equal(0, schedule.WastePickupScheduleId);
            Assert.Equal(0, schedule.CustomerId);
            Assert.Equal(DateTime.MinValue, schedule.ScheduledDate);
            Assert.Equal(WasteTypeEnum.None, schedule.WasteTypeEnum);
            Assert.Equal(PickupStatusEnum.Pending, schedule.PickupStatusEnum);
            Assert.Equal(string.Empty, schedule.Address);
            Assert.Equal(0, schedule.EstimatedVolume);
            Assert.Null(schedule.DriverId);
            Assert.Equal(string.Empty, schedule.MapLocation);
            Assert.NotNull(schedule.Customer);
        }

        [Fact]
        public void WastePickupSchedule_ShouldSetPropertiesCorrectly()
        {
            var schedule = new WastePickupSchedule
            {
                WastePickupScheduleId = 1,
                CustomerId = 101,
                ScheduledDate = new DateTime(2024, 10, 20),
                WasteTypeEnum = WasteTypeEnum.Organic,
                PickupStatusEnum = PickupStatusEnum.Completed,
                Address = "123 Green St",
                EstimatedVolume = 50.5M,
                DriverId = 10,
                MapLocation = "37.7749,-122.4194",
                Customer = new Customer { CustomerId = 101, CustomerName = "John Doe" }
            };

            Assert.Equal(1, schedule.WastePickupScheduleId);
            Assert.Equal(101, schedule.CustomerId);
            Assert.Equal(new DateTime(2024, 10, 20), schedule.ScheduledDate);
            Assert.Equal(WasteTypeEnum.Organic, schedule.WasteTypeEnum);
            Assert.Equal(PickupStatusEnum.Completed, schedule.PickupStatusEnum);
            Assert.Equal("123 Green St", schedule.Address);
            Assert.Equal(50.5M, schedule.EstimatedVolume);
            Assert.Equal(10, schedule.DriverId);
            Assert.Equal("37.7749,-122.4194", schedule.MapLocation);
            Assert.Equal(101, schedule.Customer.CustomerId);
            Assert.Equal("John Doe", schedule.Customer.CustomerName);
        }

        [Fact]
        public void WastePickupSchedule_ShouldFailForNegativeEstimatedVolume()
        {
            var schedule = new WastePickupSchedule
            {
                EstimatedVolume = -10.0M 
            };

            Assert.True(schedule.EstimatedVolume > 0);
        }

        [Fact]
        public void WastePickupSchedule_ShouldHaveEmptyOrNullByDefault()
        {
            var schedule = new WastePickupSchedule();

            Assert.Equal(string.Empty, schedule.Address);
            Assert.Equal(string.Empty, schedule.MapLocation);
            Assert.Null(schedule.DriverId);
        }

        [Fact]
        public void WastePickupSchedule_ShouldFailWhenScheduledDateIsInThePast()
        {
            var schedule = new WastePickupSchedule
            {
                ScheduledDate = DateTime.Now.AddDays(-1)
            };

            Assert.True(schedule.ScheduledDate >= DateTime.Now);
        }


    }
}
