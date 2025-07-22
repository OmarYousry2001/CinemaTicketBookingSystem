using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
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

        #region Handle Functions
        public async Task<Response<List<GetAllHallsResponse>>> Handle(GetAllHallsQuery request, CancellationToken cancellationToken)
        {
            var hallList = await _hallService.GetAllAsync();

            var mappedHallList = _mapper.Map<List<GetAllHallsResponse>>(hallList);

            return Success(mappedHallList);
        }
        public async Task<Response<FindHallByIdResponse>> Handle(FindHallByIdQuery request, CancellationToken cancellationToken)
        {
            var hall = await _hallService.FindByIdAsync(request.Id);
            if (hall == null) return NotFound<FindHallByIdResponse>();

            var mappedHall = _mapper.Map<FindHallByIdResponse>(hall);
            return Success<FindHallByIdResponse>(mappedHall);
        } 
        #endregion
    }
}
