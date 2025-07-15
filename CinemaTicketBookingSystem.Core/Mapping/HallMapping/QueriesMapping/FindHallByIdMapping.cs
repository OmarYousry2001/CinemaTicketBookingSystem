

using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.HallMapping
{
    public partial class HallProfile
    {
        public void FindHallByIdMapping()
        {
            CreateMap<Hall, FindHallByIdResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.NameEn, src.NameAr)))
                .ForMember(des => des.Capacity, opt => opt.MapFrom(src => src.Capacity));
        }
    }
}
