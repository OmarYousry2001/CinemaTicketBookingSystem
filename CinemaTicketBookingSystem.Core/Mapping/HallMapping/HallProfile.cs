using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.HallMapping
{
    public partial class HallProfile : Profile
    {
        public HallProfile()
        {
            GetAllHallsMapping();
            FindHallByIdMapping();
            CreateHallMapping();
            EditHallMapping();
        }
    }
}
