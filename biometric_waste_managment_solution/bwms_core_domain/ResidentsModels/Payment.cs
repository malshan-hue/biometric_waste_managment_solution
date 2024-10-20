using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_domain.ResidentsModels
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("Payment Number")]
        public string PaymentNumber { get; set; } = string.Empty;

        [DisplayName("Month")]
        public string Month { get; set; } = string.Empty;

        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [DisplayName("Discount")]
        public decimal Discount { get; set; }

        [DisplayName("Total Payable")]
        public decimal TotalPayable { get; set; }

        [DisplayName("Payment Status")]
        public string PaymentStatus { get; set; } = string.Empty;

    }
}
