using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Enums;
using CinemaTicketBookingSystem.Service.Abstracts;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IReservationService _reservationService;

        public PaymentService(IConfiguration configuration, IReservationService reservationService)
        {
            _configuration = configuration;
            _reservationService = reservationService;
        }
        public async Task<Reservation?> CreateOrUpdatePaymentIntent(Guid reservationId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var reservation = await _reservationService.FindByIdAsync(reservationId);

            if (reservation == null)
                return null;

            PaymentIntent paymentIntent;
            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(reservation.PaymentIntentId)) //Create Payment Intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)reservation.FinalPrice * 100,
                    Currency = "EGP",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);

                reservation.PaymentIntentId = paymentIntent.Id;
                reservation.ClientSecret = paymentIntent.ClientSecret;
            }
            else //Update Payment Intent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)reservation.FinalPrice * 100
                };
                await paymentIntentService.UpdateAsync(reservation.PaymentIntentId, options);
            }

            await _reservationService.UpdateAsync(reservation , Guid.NewGuid());
            return reservation;
        }

        public async Task UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded)
        {
            var reservation = await _reservationService.GetByPaymentIntentAsync(paymentIntentId);
            if (reservation is null)
                return;

            if (isSucceeded)
                reservation.PaymentStatus = PaymentStatusEnum.Completed;
            else
                reservation.PaymentStatus = PaymentStatusEnum.Failed;

            await _reservationService.UpdateAsync(reservation, Guid.NewGuid());
            return;
        }
    }
}
