

using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.ActorMapping
{
    public partial class ActorProfile
    {
        public void AddActorMapping()
        {
            CreateMap<AddActorCommand, Actor>()
            .ForPath(dist => dist.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForPath(dist => dist.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForPath(dist => dist.ImageURL, opt => opt.MapFrom(src => src.Image))
            .ForPath(dist => dist.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForPath(dist => dist.Bio, opt => opt.MapFrom(src => src.Bio));
        }
    }
}
