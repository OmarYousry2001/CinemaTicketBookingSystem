using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.ActorMapping
{
    public partial class ActorProfile : Profile
    {
        public ActorProfile()
        {
            //GetAllActorsMapping();
            //GetActorByIdMapping();
            AddActorMapping();
            //EditActorMapping();
        }
    }
}
