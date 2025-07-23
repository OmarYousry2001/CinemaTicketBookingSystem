using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<FindRoleByIdResponse>>,
        IRequestHandler<EditRoleCommand, Response<FindRoleByIdResponse>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region MyRegion
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        #endregion
        #region MyRegion
        public RoleCommandHandler( IAuthorizationService authorizationService
            , IMapper  mapper) 
        {
            _authorizationService = authorizationService;
            _mapper = mapper;   
        }

        #endregion
        #region MyRegion
        public async Task<Response<FindRoleByIdResponse>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _authorizationService.AddRoleAsync(request.RoleName);
                var mappedRole = new FindRoleByIdResponse { Id = role.Id, Name = role.Name };
                return Created(mappedRole);
            }
            catch (Exception ex)
            {
                return BadRequest<FindRoleByIdResponse>(ex.Message);
            }
        }

        public async Task<Response<FindRoleByIdResponse>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedRole = await _authorizationService.EditRoleAsync(request.Id, request.Name);
                var mappedRole = new FindRoleByIdResponse { Id = updatedRole.Id, Name = updatedRole.Name };
                return Success(mappedRole);
            }
            catch (Exception ex)
            {
                return BadRequest<FindRoleByIdResponse>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var identityResult = await _authorizationService.DeleteRoleAsync(request.Id);
                if (!identityResult.Succeeded)
                    return BadRequest<string>(identityResult.Errors.FirstOrDefault().Description);

                return Deleted<string>();
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }

        }
        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRoles = new UpdateUserRolesRequest();
                var mappedUserRoles = _mapper.Map(request, userRoles);
                var  result = await _authorizationService.UpdateUserRoles(mappedUserRoles);
                if (result == NotifiAndAlertsResources.Success)
                    return Success<string>(result);

                return BadRequest<string>();
            }
            catch(Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }

        }
        #endregion

    }
}
