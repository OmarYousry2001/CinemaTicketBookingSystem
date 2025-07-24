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

        #region Handle Functions
        public async Task<Response<CreateOrUpdatePaymentIntentResult>> Handle(CreateOrUpdatePaymentIntentQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _paymentService.CreateOrUpdatePaymentIntent(request.ReservationId);
            if (reservation is null)
                return BadRequest<CreateOrUpdatePaymentIntentResult>();
            var response = _mapper.Map<CreateOrUpdatePaymentIntentResult>(reservation);
            return Success(response);
        } 
        #endregion
    }
}
