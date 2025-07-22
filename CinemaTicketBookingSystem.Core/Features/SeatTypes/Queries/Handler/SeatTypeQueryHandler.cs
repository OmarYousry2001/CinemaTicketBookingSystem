using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Handler
{
    public class SeatTypeQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSeatTypesQuery, Response<List<GetAllSeatTypesResponse>>>,
        IRequestHandler<FindSeatTypeByIdQuery, Response<FindSeatTypeByIdResponse>>
    {
        #region Fields
        private readonly ISeatTypeService _seatService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public SeatTypeQueryHandler(ISeatTypeService seatService, IMapper mapper)
        {
            _seatService = seatService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetAllSeatTypesResponse>>> Handle(GetAllSeatTypesQuery request, CancellationToken cancellationToken)
        {
            var seatTypeList = await _seatService.GetAllAsync();

            var mappedSeatTypeList = _mapper.Map<List<GetAllSeatTypesResponse>>(seatTypeList);

            return Success(mappedSeatTypeList);
        }
        public async Task<Response<FindSeatTypeByIdResponse>> Handle(FindSeatTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var SeatType = await _seatService.FindByIdAsync(request.Id);
            if (SeatType == null) return NotFound<FindSeatTypeByIdResponse>();

            var mappedSeatType = _mapper.Map<FindSeatTypeByIdResponse>(SeatType);
            return Success<FindSeatTypeByIdResponse>(mappedSeatType);
        } 
        #endregion
    }
}
