using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models
{
    public class AddGenreCommand : IRequest<Response<string>>
    {
        public string NameAr { get; set; } = default!;
        public string NameEn { get; set; } = default!;


        public AddGenreCommand(string nameAr , string nameEn)
        {
            NameAr = nameAr.Trim();
            NameEn= nameEn.Trim();      
        }
    }
}
