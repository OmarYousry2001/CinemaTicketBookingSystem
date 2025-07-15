

using CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.GenreMapping
{
    public partial class GenreProfile
    {
        public void EditGenreMapping()
        {
            CreateMap<EditGenreCommand, Genre>()
                .ForMember(des => des.NameEn, option => option.MapFrom(src => src.NameEn))
                .ForMember(des => des.NameAr, option => option.MapFrom(src => src.NameAr))
                .ForMember(des => des.Id, option => option.MapFrom(src => src.Id));


        }
    }
}