using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models
{
    public class EditGenreCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; } = default!;
        public string NameEn { get; set; } = default!;

        public EditGenreCommand( Guid id, string nameAr, string nameEn)
        {
            NameAr = nameAr.Trim();
            NameEn = nameEn.Trim();
            Id= id;     
        }
    }
}
