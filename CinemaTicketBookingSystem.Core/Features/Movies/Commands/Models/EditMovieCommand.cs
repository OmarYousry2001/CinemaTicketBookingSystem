using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CinemaTicketBookingSystem.Core.Features.Movies.Commands.Models
{
    public class EditMovieCommand : IRequest<Response<string>>
    {
        public EditMovieCommand() { }
        public Guid Id { get; set; }
        public string TitleAr { get; set; } = default!;
        public string TitleEn { get; set; } = default!;
        public string DescriptionAr { get; set; } = default!;
        public string DescriptionEn { get; set; } = default!;
        public IFormFile Poster { get; set; } = default!;
        public string PosterURL { get; set; } = default!;
        public int DurationInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public RatingEnum Rate { get; set; }
        public bool IsActive { get; set; }
        public Guid DirectorId { get; set; }
        public List<Guid> ActorsIds { get; set; }
        public List<Guid> GenresIds { get; set; }
        public EditMovieCommand(string titleAr, string titleEn, string descriptionAr, string descriptionEn)
        {
            TitleAr = titleAr.Trim();
            TitleEn = titleEn.Trim();
            DescriptionAr = descriptionAr.Trim();
            DescriptionEn = descriptionEn.Trim();

        }

    }
}
