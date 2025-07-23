using CinemaTicketBookingSystem.Data.AppMetaData;
using CinemaTicketBookingSystem.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CinemaTicketBookingSystem.Core.Filters
{
    public class DataEntryRoleFilter : IAsyncActionFilter
    {
        private readonly ICurrentUserService _currentUserService;

        public DataEntryRoleFilter(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!await _currentUserService.CheckIfRuleExist(Roles.DataEntry))
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
