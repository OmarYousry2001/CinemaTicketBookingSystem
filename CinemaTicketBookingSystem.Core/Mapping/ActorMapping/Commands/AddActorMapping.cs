

using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.ActorMapping
{
    public partial class ActorProfile
    {
        public void AddActorMapping()
        {
            CreateMap<AddActorCommand, Actor>()
            .ForPath(des => des.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForPath(des => des.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForPath(des => des.ImageURL, opt => opt.MapFrom(src => src.Image))
            .ForPath(des => des.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForPath(des => des.Bio, opt => opt.MapFrom(src => src.Bio));
        }
    }
}
