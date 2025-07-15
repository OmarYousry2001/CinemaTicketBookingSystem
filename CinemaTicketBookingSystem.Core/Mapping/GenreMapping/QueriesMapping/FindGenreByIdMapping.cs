

using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.GenreMapping
{
    public partial class GenreProfile
    {
        public void FindGenreByIdMapping()
        {
            CreateMap<Genre, FindGenreByIdResponse>()
                .ForMember(des => des.Id, option => option.MapFrom(src => src.Id))
                .ForMember(des => des.Name, option => option.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

        }
    }
}
