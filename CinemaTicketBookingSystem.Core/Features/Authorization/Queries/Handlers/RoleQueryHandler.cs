using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Authorization.Quaries.Models;
using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Core.Features.Authorization.Quaries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
       IRequestHandler<GetAllRolesQuery, Response<List<GetRolesListResponse>>>,
       IRequestHandler<FindRoleByIdQuery, Response<FindRoleByIdResponse>>,
       IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResponse>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion
        #region Constructors
        public RoleQueryHandler(IAuthorizationService authorizationService,
                                IMapper mapper,
                                UserManager<ApplicationUser> userManager) 
        {
            _authorizationService=authorizationService;
            _mapper=mapper;
            _userManager=userManager;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetRolesListResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetAllRolesAsync();
            var result = _mapper.Map<List<GetRolesListResponse>>(roles);
            return Success(result);
        }

        public async Task<Response<FindRoleByIdResponse>> Handle(FindRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.FindRoleByIdAsync(request.Id);
            if (role==null) return NotFound<FindRoleByIdResponse>(ValidationResources.RoleNotExist);
            var result = _mapper.Map<FindRoleByIdResponse>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user==null) return NotFound<ManageUserRolesResponse>(ValidationResources.UserNotFound);
            var manageUserRoles = await _authorizationService.ManageUserRolesData(user);
            var MappedManageUserRoles = _mapper.Map<ManageUserRolesResponse>(manageUserRoles);
            return Success(MappedManageUserRoles);
        }
        #endregion
    }
}
