using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.SeatTypeMapping
{
    public partial class SeatTypeProfile : Profile
    {
        public SeatTypeProfile()
        {
            GetAllSeatTypesMapping();
            FindSeatTypeByIdMapping();
            CreateSeatTypeMapping();
            EditSeatTypeMapping();
        }
    }
}