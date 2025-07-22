using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Halls.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Halls.Commands.Handler
{
    public class SeatCommandsHandler : ResponseHandler,
        IRequestHandler<AddHallCommand, Response<string>>,
        IRequestHandler<EditHallCommand, Response<string>>,
        IRequestHandler<DeleteHallCommand, Response<string>>
    {
        #region Fields
        private readonly IHallService _hallService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructors
        public SeatCommandsHandler(IHallService hallService, IMapper mapper, ICurrentUserService currentUserService)
        {
            _hallService = hallService;
            _mapper = mapper;
            _currentUserService = currentUserService;

        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddHallCommand request, CancellationToken cancellationToken)
        {
            var hall = _mapper.Map<Hall>(request);
            var isSaved = await _hallService.AddAsync(hall, _currentUserService.GetUserId());
            if (isSaved)
                return Success(ActionsResources.Accept);
            else
                return NotFound<string>();

        }
        public async Task<Response<string>> Handle(EditHallCommand request, CancellationToken cancellationToken)
        {
            var oldHall = await _hallService.FindByIdAsync(request.Id);
            if (oldHall == null) return NotFound<string>();
            var mappedHall = _mapper.Map(request, oldHall);
            //Call service that make Edit
            var result = await _hallService.SaveAsync(mappedHall, _currentUserService.GetUserId());
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(DeleteHallCommand request, CancellationToken cancellationToken)
        {
            var hall = await _hallService.FindByIdAsync(request.Id);
            if (hall == null) return NotFound<string>();
            var isDeleted = await _hallService.DeleteAsync(hall);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();
        } 
        #endregion
    }
}
