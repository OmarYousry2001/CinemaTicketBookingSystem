using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.DirectorMapping
{
    public partial class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            GetAllDirectorsMapping();
            GetDirectorByIdMapping();
            AddDirectorMapping();
            EditDirectorMapping();
        }
    }
}
