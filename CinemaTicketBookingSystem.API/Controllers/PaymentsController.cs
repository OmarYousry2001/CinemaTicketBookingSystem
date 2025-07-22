using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Payments.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    public class PaymentsController : AppControllerBase
    {
        private readonly string _webHookSecret;
        private readonly IPaymentService _paymentService;
        #region Constructors
        public PaymentsController(IPaymentService paymentService, IConfiguration configuration)
        {
            _webHookSecret = configuration["StripeSettings:webHookSecret"];
            _paymentService = paymentService;
        }
        #endregion



        [Authorize]
        [HttpPost(Router.PaymentRouting.Create)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrUpdatePaymentIntentQuery(Guid reservationId)
        {
            var response = await Mediator.Send(new CreateOrUpdatePaymentIntentQuery { ReservationId = reservationId });
            return NewResult(response);
        }

        [HttpPost(Router.PaymentRouting.webhook)]
        public async Task<IActionResult> HandleWebhookAsync()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _webHookSecret
                );

                // Handle the event
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                switch (stripeEvent.Type)
                {
                    case "payment_intent.succeeded":
                        await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, true);
                        break;
                    case "payment_intent.failed":
                        await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, false);
                        break;
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}