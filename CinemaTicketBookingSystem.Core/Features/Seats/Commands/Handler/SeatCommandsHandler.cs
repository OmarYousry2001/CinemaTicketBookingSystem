using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Seats.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Seats.Commands.Handler
{
    public class SeatCommandsHandler : ResponseHandler,
        IRequestHandler<AddSeatCommand, Response<string>>,
        IRequestHandler<EditSeatCommand, Response<string>>,
        IRequestHandler<DeleteSeatCommand, Response<string>>
    {
        #region Fields
        private readonly ISeatService _seatService;
        private readonly IHallService _hallService;
        private readonly ISeatTypeService _seatTypeService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        #endregion
        
        #region Constructors
        public SeatCommandsHandler(ISeatService seatService
            , IMapper mapper
            , IHallService hallService
            , ISeatTypeService seatTypeService
            , ICurrentUserService currentUserService)
        {
            _seatService = seatService;
            _mapper = mapper;
            _hallService = hallService;
            _seatTypeService = seatTypeService;
            _currentUserService = currentUserService;   
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddSeatCommand request, CancellationToken cancellationToken)
        {
            var seat = _mapper.Map<Seat>(request);
            var savedSeat = await _seatService.AddAsync(seat, _currentUserService.GetUserId());
            return (savedSeat) ? Success(ActionsResources.Accept) : BadRequest<string>();
        }
        public async Task<Response<string>> Handle(EditSeatCommand request, CancellationToken cancellationToken)
        {
            var oldSeat = await _seatService.FindByIdAsync(request.Id);
            if (oldSeat == null) return NotFound<string>();
            var mappedSeat = _mapper.Map(request, oldSeat);
            var result = await _seatService.SaveAsync(mappedSeat, _currentUserService.GetUserId());
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(DeleteSeatCommand request, CancellationToken cancellationToken)
        {
            var seat = await _seatService.FindByIdAsync(request.Id);
            var isDeleted = await _seatService.DeleteAsync(seat);
            return isDeleted ? Deleted<string>() : BadRequest<string>();
        } 
        #endregion
    }
}
