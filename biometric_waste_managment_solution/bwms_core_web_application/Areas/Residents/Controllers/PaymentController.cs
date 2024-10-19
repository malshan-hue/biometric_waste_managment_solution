using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using bwms_core_domain.SystemModels;
using Newtonsoft.Json;
using devspark_core_model.SystemModels;

namespace bwms_core_web_application.Areas.Residents.Controllers
{
    [Area("Residents")]
    public class PaymentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            List<Payment> paymentList = new List<Payment>
            {
                new Payment
                {
                    InvoiceNumber = "INV123456",
                    PaymentAmount = 150.00m,  
                    PaymentDate = DateTime.Now.AddDays(-2), 
                    PaymentStatus = "Paid"
                },
                new Payment
                {
                    InvoiceNumber = "INV123457",
                    PaymentAmount = 200.00m,
                    PaymentDate = DateTime.Now.AddDays(-1),
                    PaymentStatus = "Pending"
                },
                new Payment
                {
                    InvoiceNumber = "INV123458",
                    PaymentAmount = 125.50m,
                    PaymentDate = DateTime.Now,
                    PaymentStatus = "Paid"
                }
            };


            var successUrl = Url.Action("PaymentComplete", "Payment", new { area = "Residents" }, Request.Scheme) + "?sessionId={CHECKOUT_SESSION_ID}";
            var cancelUrl = Url.Action("PaymentCancel", "Payment", new { area = "Residents" }, Request.Scheme) + "?sessionId={CHECKOUT_SESSION_ID}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = paymentList.Select(payment => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(payment.PaymentAmount * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Payment for Invoice {payment.InvoiceNumber}",
                        },
                    },
                    Quantity = 1,
                }).ToList(),

                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
            };

            var service = new SessionService();
            try
            {
                Session session = service.Create(options);

                //return Json(new { redirectUrl = session.Url });
                return Redirect(session.Url);
            }
            catch (Exception)
            {
                return BadRequest("Failed to create payment session.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PaymentComplete(string sessionId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            List<SystemNotification> systemNotifications = new List<SystemNotification>();
            SystemNotification systemNotification = new SystemNotification();
            bool status = false;

            try
            {
                var session = new SessionService().Get(sessionId);
                var paymentIntent = new PaymentIntentService().Get(session.PaymentIntentId);
                var charge = new ChargeService().Get(paymentIntent.LatestChargeId);

                OnlinePaymentResponse onlinePaymentResponse = new OnlinePaymentResponse();

                onlinePaymentResponse.PaymentTransactionIdentification = session.PaymentIntentId;
                onlinePaymentResponse.CardCountry = charge?.PaymentMethodDetails?.Card?.Country;
                onlinePaymentResponse.CardLastFourDigits = charge?.PaymentMethodDetails?.Card?.Last4;
                onlinePaymentResponse.CardBrand = charge?.PaymentMethodDetails?.Card?.Brand;

                onlinePaymentResponse.ProvidedEmailAddress = session?.CustomerDetails?.Email;
                onlinePaymentResponse.ProvidedName = session?.CustomerDetails?.Name;

                onlinePaymentResponse.PaymentNetworkStatus = charge?.Outcome?.NetworkStatus;
                onlinePaymentResponse.PaymentRiskLevel = charge?.Outcome?.RiskLevel;
                onlinePaymentResponse.PaymentRiskScore = charge?.Outcome?.RiskScore.ToString();

                onlinePaymentResponse.PaymentGatewayReceiptUrl = charge?.ReceiptUrl;

                onlinePaymentResponse.PaymentErrorCode = paymentIntent?.LastPaymentError?.Code;
                onlinePaymentResponse.PaymentErrorMessage = paymentIntent?.LastPaymentError?.Message;
                onlinePaymentResponse.PaymentErrorType = paymentIntent?.LastPaymentError?.Type;
                onlinePaymentResponse.PaymentErrorDecliendCode = paymentIntent?.LastPaymentError?.DeclineCode;
                onlinePaymentResponse.PaymentErrorDescription = paymentIntent?.LastPaymentError?.ErrorDescription;
                onlinePaymentResponse.OnlinePaymentGlobalIdentity = Guid.NewGuid();
                onlinePaymentResponse.PaymentStatus = paymentIntent.Status;
                onlinePaymentResponse.CustomerId = Convert.ToInt32(userId);

                if (!paymentIntent.Status.Equals("succeeded"))
                {
                    #region SYSTEM NOTIFICATION

                    systemNotification.Title = "Error creating payment";
                    systemNotification.Message = "Payment not saved properly";
                    systemNotification.Time = DateTime.Now.ToString("dd/MM/yyyy");
                    systemNotification.NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Danger);
                    systemNotification.NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight);

                    systemNotifications.Add(systemNotification);

                    TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

                    #endregion

                    return RedirectToAction("Index", "Payment");
                }

            }
            catch (StripeException)
            {
                throw;
            }

            #region SYSTEM NOTIFICATION

            systemNotification.Title = "Payment Success";
            systemNotification.Message = "Payment crated successfully";
            systemNotification.Time = DateTime.Now.ToString("dd/MM/yyyy");
            systemNotification.NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Success);
            systemNotification.NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight);

            systemNotifications.Add(systemNotification);

            TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

            #endregion

            return RedirectToAction("Index", "Payment");
        }

        [HttpGet]
        public async Task<IActionResult> PaymentCancel(string sessionId, Guid onlinePaymentGlobalIdentity)
        {
            List<SystemNotification> systemNotifications = new List<SystemNotification>();
            SystemNotification systemNotification = new SystemNotification();

            try
            {
                var session = new SessionService().Get(sessionId);

                OnlinePaymentResponse onlinePaymentResponse = new OnlinePaymentResponse();

                onlinePaymentResponse.PaymentTransactionIdentification = session.PaymentIntentId;

                onlinePaymentResponse.ProvidedEmailAddress = session?.CustomerDetails?.Email;
                onlinePaymentResponse.ProvidedName = session?.CustomerDetails?.Name;
                onlinePaymentResponse.OnlinePaymentGlobalIdentity = onlinePaymentGlobalIdentity;
                onlinePaymentResponse.PaymentStatus = session?.PaymentStatus;

            }
            catch (StripeException)
            {
                throw;
            }

            #region SYSTEM NOTIFICATION

            systemNotification.Title = "Error creating payment";
            systemNotification.Message = "Payment didn't saved properly";
            systemNotification.Time = DateTime.Now.ToString("dd/MM/yyyy");
            systemNotification.NotificationType = Enum.GetName(typeof(NotificationType), NotificationType.Success);
            systemNotification.NotificationPlacement = Enum.GetName(typeof(NotificationType), NotificationPlacement.TopRight);

            systemNotifications.Add(systemNotification);

            TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

            #endregion

            return RedirectToAction("Index", "Payment");
        }

        public class Payment
        {
            public string InvoiceNumber { get; set; } = string.Empty;
            public decimal PaymentAmount { get; set; }
            public DateTime PaymentDate { get; set; }
            public string PaymentStatus { get; set; } = string.Empty;
        }
    }
}
