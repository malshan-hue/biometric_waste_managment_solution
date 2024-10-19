using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_domain.SystemModels
{
    public class OnlinePaymentResponse
    {
        public long OnlinePaymentResponseId { get; set; }
        public string PaymentTransactionIdentification { get; set; } = string.Empty;
        public string CardCountry { get; set; } = string.Empty;
        public string CardLastFourDigits { get; set; } = string.Empty;
        public string CardBrand { get; set; } = string.Empty;
        public string ProvidedEmailAddress { get; set; } = string.Empty;
        public string ProvidedName { get; set; } = string.Empty;
        public string PaymentNetworkStatus { get; set; } = string.Empty;
        public string PaymentRiskLevel { get; set; } = string.Empty;
        public string PaymentRiskScore { get; set; } = string.Empty;
        public string PaymentGatewayReceiptUrl { get; set; } = string.Empty;
        public string PaymentErrorCode { get; set; } = string.Empty;
        public string PaymentErrorMessage { get; set; } = string.Empty;
        public string PaymentErrorType { get; set; } = string.Empty;
        public string PaymentErrorDecliendCode { get; set; } = string.Empty;
        public string PaymentErrorDescription { get; set; } = string.Empty;
        public Guid OnlinePaymentGlobalIdentity { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}
