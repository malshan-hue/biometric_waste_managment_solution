using bwms_core_domain.ResidentsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bwms_core_unit_test
{
    public class PaymentTests
    {
        [Fact]
        public void Payment_DefaultValues_ShouldBeInitializedCorrectly()
        {
            var payment = new Payment();

            Assert.Equal(0, payment.PaymentId);
            Assert.Equal(0, payment.CustomerId);
            Assert.Equal(string.Empty, payment.PaymentNumber);
            Assert.Equal(string.Empty, payment.Month);
            Assert.Equal(0.0M, payment.Amount);
            Assert.Equal(0.0M, payment.Discount);
            Assert.Equal(0.0M, payment.TotalPayable);
            Assert.Equal(string.Empty, payment.PaymentStatus);
        }

        [Fact]
        public void Payment_ShouldSetPropertiesCorrectly()
        {
            var payment = new Payment
            {
                PaymentId = 1,
                CustomerId = 101,
                PaymentNumber = "PMT123",
                Month = "October",
                Amount = 100.0M,
                Discount = 10.0M,
                TotalPayable = 90.0M,
                PaymentStatus = "Paid"
            };

            Assert.Equal(1, payment.PaymentId);
            Assert.Equal(101, payment.CustomerId);
            Assert.Equal("PMT123", payment.PaymentNumber);
            Assert.Equal("October", payment.Month);
            Assert.Equal(100.0M, payment.Amount);
            Assert.Equal(10.0M, payment.Discount);
            Assert.Equal(90.0M, payment.TotalPayable);
            Assert.Equal("Paid", payment.PaymentStatus);
        }

        [Fact]
        public void Payment_ShouldCalculateTotalPayableCorrectly()
        {
            var payment = new Payment
            {
                Amount = 150.0M,
                Discount = 15.0M
            };

            payment.TotalPayable = payment.Amount - payment.Discount;

            Assert.Equal(135.0M, payment.TotalPayable);
        }

        [Fact]
        public void Payment_ShouldFailForNegativeAmount()
        {
            var payment = new Payment
            {
                Amount = -50.0M
            };

            Assert.True(payment.Amount >= 0, "Amount should not be negative.");
        }

        [Fact]
        public void Payment_ShouldFailForNegativeDiscount()
        {
            var payment = new Payment
            {
                Discount = -10.0M
            };

            Assert.True(payment.Discount >= 0, "Discount should not be negative.");
        }

        [Fact]
        public void Payment_ShouldFailWhenTotalPayableExceedsAmount()
        {
            var payment = new Payment
            {
                Amount = 100.0M,
                Discount = 120.0M
            };

            payment.TotalPayable = payment.Amount - payment.Discount;

            Assert.True(payment.TotalPayable >= 0, "Total Payable should not be negative or greater than the Amount.");
        }

        [Fact]
        public void Payment_ShouldHandleEmptyPaymentNumberAndStatus()
        {
            var payment = new Payment();

            Assert.Equal(string.Empty, payment.PaymentNumber);
            Assert.Equal(string.Empty, payment.PaymentStatus);
        }

        //[Fact]
        //public void Payment_ShouldValidatePaymentStatusValues()
        //{
        //    var validStatuses = new[] { "Pending", "Paid", "Failed" };
        //    var payment = new Payment { PaymentStatus = "InvalidStatus" };

        //    Assert.Contains(payment.PaymentStatus, validStatuses, "PaymentStatus should be one of the predefined values.");
        //}
    }
}
