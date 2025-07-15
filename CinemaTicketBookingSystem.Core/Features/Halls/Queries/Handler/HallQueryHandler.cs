using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Implementations;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Halls.Queries.Handler
{
    public class HallQueryHandler : ResponseHandler,
        IRequestHandler<GetAllHallsQuery, Response<List<GetAllHallsResponse>>>,
        IRequestHandler<FindHallByIdQuery, Response<FindHallByIdResponse>>
    {
        #region Fields
        private readonly IHallService _hallService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public HallQueryHandler(IHallService hallService, IMapper mapper)
        {
            _hallService = hallService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllHallsResponse>>> Handle(GetAllHallsQuery request, CancellationToken cancellationToken)
        {
            var entityList = await _hallService.GetAllAsync();

            var dtoList = _mapper.Map<List<GetAllHallsResponse>>(entityList);

            return Success(dtoList);
        }

        public async Task<Response<FindHallByIdResponse>> Handle(FindHallByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _hallService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<FindHallByIdResponse>();

            var dto = _mapper.Map<FindHallByIdResponse>(entity);
            return Success<FindHallByIdResponse>(dto);
        }
    }
}
