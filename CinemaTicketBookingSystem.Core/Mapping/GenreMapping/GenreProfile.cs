using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.GenreMapping
{
    public partial class GenreProfile : Profile
    {
        public GenreProfile()
        {
            GetAllGenresMapping();
            FindGenreByIdMapping();
            CreateGenreMapping();
            EditGenreMapping();
        }
    }
}