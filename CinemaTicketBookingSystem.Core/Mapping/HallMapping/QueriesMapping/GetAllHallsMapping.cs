

using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.HallMapping
{
    public partial class HallProfile
    {
        public void GetAllHallsMapping()
        {
            CreateMap<Hall, GetAllHallsResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                .ForMember(des => des.Capacity, opt => opt.MapFrom(src => src.Capacity)); 
        }
    }
}
