
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.SeatTypeMapping
{
    public partial class SeatTypeProfile
    {
        public void FindSeatTypeByIdMapping()
        {
            CreateMap<SeatType, FindSeatTypeByIdResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Localize(src.TypeNameAr, src.TypeNameEn)))
                .ForMember(dest => dest.SeatTypePrice, opt => opt.MapFrom(src => src.SeatTypePrice));
     
            
         
        }
    }
}
