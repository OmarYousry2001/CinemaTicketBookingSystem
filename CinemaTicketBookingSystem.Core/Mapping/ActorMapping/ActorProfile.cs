using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.ActorMapping
{
    public partial class ActorProfile : Profile
    {
        public ActorProfile()
        {
            GetAllActorsMapping();
            FindActorByIdMapping();
            AddActorMapping();
            EditActorMapping();
        }
    }
}
