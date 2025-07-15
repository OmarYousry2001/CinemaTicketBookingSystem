

using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.ActorMapping
{
    public partial class ActorProfile
    {
        public void GetAllActorsMapping()
        {
            CreateMap<Actor, GetAllActorsResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(des => des.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
                .ForMember(des => des.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(des => des.Bio, opt => opt.MapFrom(src => src.Bio));
        }
    }
}
