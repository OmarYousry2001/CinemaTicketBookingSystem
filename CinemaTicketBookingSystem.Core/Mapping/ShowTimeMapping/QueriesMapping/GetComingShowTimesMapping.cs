using CinemaTicketBookingSystem.Core.Features.ShowTimes.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.ShowTimeMapping
{
    public partial class ShowTimeProfile
    {
        public void GetComingShowTimesMapping()
        {
            CreateMap<ShowTime, GetComingShowTimesResponse>()
                    .ForMember(dis => dis.HallName, options => options.MapFrom(src => src.Hall.Localize(src.Hall.NameAr, src.Hall.NameEn)))
                .ForMember(dis => dis.MovieTitle, options => options.MapFrom(src => src.Movie.Localize(src.Movie.TitleAr, src.Movie.TitleEn)));
        }
    }
}

