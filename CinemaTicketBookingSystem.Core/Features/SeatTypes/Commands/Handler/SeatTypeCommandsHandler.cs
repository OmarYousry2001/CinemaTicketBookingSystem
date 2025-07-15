using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Implementations;
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
        #endregion

        #region Constructors
        public SeatTypeCommandsHandler(ISeatTypeService seatTypeService, IMapper mapper)
        {
            _seatTypeService = seatTypeService;
            _mapper = mapper;
        }
        #endregion

        public async Task<Response<string>> Handle(AddSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<SeatType>(request);
            var savedActor = await _seatTypeService.SaveAsync(entity, Guid.NewGuid());
            if (savedActor)
                return Success(ActionsResources.Accept);
            else
                return NotFound<string>();
        }
        public async Task<Response<string>> Handle(EditSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _seatTypeService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var entityMapping = _mapper.Map(request, entity);
            var result = await _seatTypeService.SaveAsync(entityMapping, Guid.NewGuid());
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteSeatTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _seatTypeService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var isDeleted = await _seatTypeService.Delete(entity);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();
        }
    }
}
