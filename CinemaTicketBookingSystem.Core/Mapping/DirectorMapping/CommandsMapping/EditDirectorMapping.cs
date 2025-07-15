

using CinemaTicketBookingSystem.Core.Features.Directors.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.DirectorMapping
{
    public partial class DirectorProfile
    {
        public void EditDirectorMapping()
        {
            CreateMap<EditDirectorCommand, Director>()
            .ForPath(des => des.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForPath(des => des.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForPath(des => des.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
            .ForPath(des => des.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForPath(des => des.Bio, opt => opt.MapFrom(src => src.Bio));
        }
    }
}
