using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.ReservationMapping
{
    public partial class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            FindReservationByIdMapping();
            GetReservationsPaginatedListResponse();
        }
    }
}
