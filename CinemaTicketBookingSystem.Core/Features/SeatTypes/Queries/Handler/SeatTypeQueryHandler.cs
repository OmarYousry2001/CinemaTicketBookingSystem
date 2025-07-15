using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Implementations;
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
        public async Task<Response<List<GetAllSeatTypesResponse>>> Handle(GetAllSeatTypesQuery request, CancellationToken cancellationToken)
        {
            var entityList = await _seatService.GetAllAsync();

            var dtoList = _mapper.Map<List<GetAllSeatTypesResponse>>(entityList);

            return Success(dtoList);
        }

        public async Task<Response<FindSeatTypeByIdResponse>> Handle(FindSeatTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _seatService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<FindSeatTypeByIdResponse>();

            var dto = _mapper.Map<FindSeatTypeByIdResponse>(entity);
            return Success<FindSeatTypeByIdResponse>(dto);
        }
    }
}
