using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models
{
    public class AddActorCommand :IRequest<Response<string>>  
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public IFormFile? Image { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
    }
}
