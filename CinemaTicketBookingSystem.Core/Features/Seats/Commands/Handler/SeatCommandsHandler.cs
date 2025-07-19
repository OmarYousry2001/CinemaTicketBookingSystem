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
        private readonly IMapper _mapper;
        #endregion
        
        #region Constructors
        public SeatCommandsHandler(ISeatService seatService, IMapper mapper, IHallService hallService, ISeatTypeService seatTypeService)
        {
            _seatService = seatService;
            _mapper = mapper;
            _hallService = hallService;
            _seatTypeService = seatTypeService;
        }
        #endregion

        public async Task<Response<string>> Handle(AddSeatCommand request, CancellationToken cancellationToken)
        {
            var seat = _mapper.Map<Seat>(request);
            var savedSeat = await _seatService.AddAsync(seat, Guid.NewGuid());
            return (savedSeat)?  Success(ActionsResources.Accept):  BadRequest<string>();
        }
        public async Task<Response<string>> Handle(EditSeatCommand request, CancellationToken cancellationToken)
        {
            var entity = await _seatService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var entityMapping = _mapper.Map(request, entity);
            var result = await _seatService.SaveAsync(entityMapping, Guid.NewGuid());
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
    }
}
