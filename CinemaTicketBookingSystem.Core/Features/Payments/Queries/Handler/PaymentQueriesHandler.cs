using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Payments.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Payments.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Payments.Queries.Handler
{
    public class PaymentQueriesHandler : ResponseHandler,
        IRequestHandler<CreateOrUpdatePaymentIntentQuery, Response<CreateOrUpdatePaymentIntentResult>>
    {
        #region Fields
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public PaymentQueriesHandler(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;  
        }
        #endregion
        public async Task<Response<CreateOrUpdatePaymentIntentResult>> Handle(CreateOrUpdatePaymentIntentQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _paymentService.CreateOrUpdatePaymentIntent(request.ReservationId);
            if (reservation is null)
                return BadRequest<CreateOrUpdatePaymentIntentResult>();
            var response  = _mapper.Map<CreateOrUpdatePaymentIntentResult>(reservation);
            //var response = new CreateOrUpdatePaymentIntentResult()
            //{
            //    ReservationId = reservation.Id,
            //    ClientSecret = reservation.ClientSecret,
            //    HallName = reservation.ShowTime.Hall.name,
            //    ShowTime = new ShowTimeInReservationResponse()
            //    {
            //        Day = reservation.ShowTime.Day,
            //        EndTime = reservation.ShowTime.EndTime,
            //        ShowTimeId = reservation.ShowTime.ShowTimeId,
            //        StartTime = reservation.ShowTime.StartTime,
            //        MovieName = reservation.ShowTime.Movie.Title
            //    },
            //    PaymentIntentId = reservation.PaymentIntentId,
            //    ReservationDate = reservation.CreatedAt,
            //    Seats = reservation.ReservedSeats.Select(rs => new SeatsInReservationResponse
            //    {
            //        SeatId = rs.SeatId,
            //        SeatNumber = rs.SeatNumber,
            //    }).ToList(),
            //    User = new UserInReservationResponse
            //    {
            //        Id = reservation.UserId,
            //        UserName = reservation.User.UserName
            //    },
            //};

            return Success(response);
        }
    }
}
