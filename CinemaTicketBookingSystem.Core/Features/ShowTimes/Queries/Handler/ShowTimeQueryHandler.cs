using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.ShowTimes.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.ShowTimes.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.ShowTimes.Queries.Handler
{
    public class ShowTimeQueryHandler : ResponseHandler,
        IRequestHandler<GetAllShowTimesQuery, Response<List<GetAllShowTimesResponse>>>,
        IRequestHandler<FindShowTimeByIdQuery, Response<FindShowTimeByIdResponse>>,
        IRequestHandler<GetComingShowTimesQuery, Response<List<GetComingShowTimesResponse>>>
    {
        #region Fields
        private readonly IShowTimeService _showTimeService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ShowTimeQueryHandler(IShowTimeService showTimeService, IMapper mapper)
        {
            _showTimeService = showTimeService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetAllShowTimesResponse>>> Handle(GetAllShowTimesQuery request, CancellationToken cancellationToken)
        {
            var showTimesList = await _showTimeService.GetAllAsync();

            var mappedShowTimesList = _mapper.Map<List<GetAllShowTimesResponse>>(showTimesList);

            return Success(mappedShowTimesList);
        }

        public async Task<Response<FindShowTimeByIdResponse>> Handle(FindShowTimeByIdQuery request, CancellationToken cancellationToken)
        {
            var showTime = await _showTimeService.FindByIdAsync(request.Id);

            if (showTime == null)
                return NotFound<FindShowTimeByIdResponse>();

            var mappedShowTime = _mapper.Map<FindShowTimeByIdResponse>(showTime);

            return Success(mappedShowTime);
        }

        public async Task<Response<List<GetComingShowTimesResponse>>> Handle(GetComingShowTimesQuery request, CancellationToken cancellationToken)
        {
            var showTimesList = await _showTimeService.GetComingShowTimesAsync();

            var mappedShowTimesList = _mapper.Map<List<GetComingShowTimesResponse>>(showTimesList);

            return Success(mappedShowTimesList);
        } 
        #endregion
    }
}
