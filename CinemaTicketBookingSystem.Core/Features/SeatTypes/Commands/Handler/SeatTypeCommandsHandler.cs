using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Handler
{
    public class SeatTypeCommandsHandler : ResponseHandler,
        IRequestHandler<AddSeatTypeCommand, Response<string>>,
        IRequestHandler<EditSeatTypeCommand, Response<string>>,
        IRequestHandler<DeleteSeatTypeCommand, Response<string>>
    {
        #region Fields
        private readonly ISeatTypeService _seatTypeService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructors
        public SeatTypeCommandsHandler(ISeatTypeService seatTypeService, IMapper mapper, ICurrentUserService currentUserService )
        {
            _seatTypeService = seatTypeService;
            _mapper = mapper;
            _currentUserService = currentUserService;

        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var SeatType = _mapper.Map<SeatType>(request);
            var savedASeatType = await _seatTypeService.SaveAsync(SeatType, _currentUserService.GetUserId());
            if (savedASeatType)
                return Success(ActionsResources.Accept);
            else
                return NotFound<string>();
        }
        public async Task<Response<string>> Handle(EditSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var oldSeatType = await _seatTypeService.FindByIdAsync(request.Id);
            if (oldSeatType == null) return NotFound<string>();
            var seatTypeMapping = _mapper.Map(request, oldSeatType);
            var result = await _seatTypeService.SaveAsync(seatTypeMapping, _currentUserService.GetUserId());
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(DeleteSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var SeatType = await _seatTypeService.FindByIdAsync(request.Id);
            if (SeatType == null) return NotFound<string>();
            var isDeleted = await _seatTypeService.DeleteAsync(SeatType);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();
        } 
        #endregion
    }
}
