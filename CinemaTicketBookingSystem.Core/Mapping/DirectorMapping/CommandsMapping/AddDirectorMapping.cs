

using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Directors.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.DirectorMapping
{
    public partial class DirectorProfile
    {
        public void AddDirectorMapping()
        {
            CreateMap<AddDirectorCommand, Director>()
            .ForPath(des => des.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForPath(des => des.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForPath(des => des.ImageURL, opt => opt.MapFrom(src => src.Image))
            .ForPath(des => des.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForPath(des => des.Bio, opt => opt.MapFrom(src => src.Bio));
        }
    }
}
