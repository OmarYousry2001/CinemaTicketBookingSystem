using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.ShowTimeMapping
{
    public partial class ShowTimeProfile : Profile
    {
        public ShowTimeProfile()
        {
            GetShowTimeByIdMapping();
            GetAllShowTimesMapping();
            CreateShowTimeMapping();
            EditShowTimeMapping();
            GetComingShowTimesMapping();
        }
    }
}
