using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.SeatMapping
{
    public partial class SeatProfile : Profile
    {
        public SeatProfile()
        {
            GetAllSeatsMapping();
            FindSeatByIdMapping();
            EditSeatMapping();
            AddSeatMapping();
        }
    }
}
