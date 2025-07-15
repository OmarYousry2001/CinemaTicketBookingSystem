
using CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.GenreMapping
{
    public partial class GenreProfile
    {
        public void CreateGenreMapping()
        {
            CreateMap<AddGenreCommand, Genre>()
                .ForMember(des => des.NameEn, option => option.MapFrom(src => src.NameEn))
                .ForMember(des => des.NameAr, option => option.MapFrom(src => src.NameAr));


        }
    }
}
