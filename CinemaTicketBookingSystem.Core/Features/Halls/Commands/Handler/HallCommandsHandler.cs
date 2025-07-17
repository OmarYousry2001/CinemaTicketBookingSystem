using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Halls.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Implementations;
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
        #endregion

        #region Constructors
        public SeatCommandsHandler(IHallService hallService, IMapper mapper)
        {
            _hallService = hallService;
            _mapper = mapper;
        }
        #endregion

        public async Task<Response<string>> Handle(AddHallCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Hall>(request);
            var isSaved = await _hallService.AddAsync(entity , Guid.NewGuid());
            if (isSaved)
                return Success(ActionsResources.Accept);
            else
                return NotFound<string>();
            
        }
        public async Task<Response<string>> Handle(EditHallCommand request, CancellationToken cancellationToken)
        {
            var entity = await _hallService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var entityMapping = _mapper.Map(request, entity);
            //Call service that make Edit
            var result = await _hallService.SaveAsync(entityMapping, Guid.NewGuid());
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteHallCommand request, CancellationToken cancellationToken)
        {
            var entity = await _hallService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var isDeleted = await _hallService.DeleteAsync(entity);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();
        }
    }
}
