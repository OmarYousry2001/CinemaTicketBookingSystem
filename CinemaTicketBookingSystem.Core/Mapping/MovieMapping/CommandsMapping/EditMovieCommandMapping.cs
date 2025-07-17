
using CinemaTicketBookingSystem.Core.Features.Movies.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.MovieMapping
{
    public partial class MovieProfile
    {
        public void EditMovieCommandMapping()
        {
            CreateMap<EditMovieCommand, Movie>()
                .ForMember(dsc => dsc.PosterURL, option => option.Ignore())
                .ForMember(dsc => dsc.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dsc => dsc.TitleAr, option => option.MapFrom(src => src.TitleAr))
                .ForMember(dsc => dsc.TitleEn, option => option.MapFrom(src => src.TitleEn))
                .ForMember(dsc => dsc.DescriptionAr, option => option.MapFrom(src => src.DescriptionAr))
                .ForMember(dsc => dsc.DescriptionEn, option => option.MapFrom(src => src.DescriptionEn));
        }
    }
}
