using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Handler
{
    public class ShowTimeCommandsHandler : ResponseHandler,
        IRequestHandler<AddShowTimeCommand, Response<string>>,
        IRequestHandler<EditShowTimeCommand, Response<string>>,
        IRequestHandler<DeleteShowTimeCommand, Response<string>>
    {
        #region Fields
        private readonly IShowTimeService _showTimeService;
        private readonly IHallService _hallService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructors
        public ShowTimeCommandsHandler(IShowTimeService showTimeService
            , IMapper mapper, IHallService hallService
            , IMovieService movieService
            , ICurrentUserService currentUserService)
        {
            _showTimeService = showTimeService;
            _mapper = mapper;
            _hallService = hallService;
            _movieService = movieService;
            _currentUserService = currentUserService;   

        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddShowTimeCommand request, CancellationToken cancellationToken)
        {
            var hall = await _hallService.FindByIdAsync(request.HallId);
            var movie = await _movieService.FindByIdAsync(request.MovieId);
            if (hall == null)
                throw new Exception("no hall exist");

            if (movie == null)
                throw new Exception("no movie exist");

            var showTime = _mapper.Map<ShowTime>(request);
            showTime.Hall = hall;
            showTime.Movie = movie;

            var savedShowTime = await _showTimeService.AddAsync(showTime, _currentUserService.GetUserId());
            return savedShowTime ? Created(ActionsResources.Accept) : BadRequest<string>();
        }
        public async Task<Response<string>> Handle(EditShowTimeCommand request, CancellationToken cancellationToken)
        {
            var oldShowTime = await _showTimeService.FindByIdAsync(request.Id);
            var mappedShowTime = _mapper.Map(request, oldShowTime);
            var savedShowTime = await _showTimeService.SaveAsync(mappedShowTime, _currentUserService.GetUserId());
            return savedShowTime ? Created(NotifiAndAlertsResources.ItemUpdated) : BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteShowTimeCommand request, CancellationToken cancellationToken)
        {
            var showTime = await _showTimeService.FindByIdAsync(request.Id);
            var isDeleted = await _showTimeService.DeleteAsync(showTime);
            return isDeleted ? Deleted<string>() : BadRequest<string>();
        } 
        #endregion
    }
}
