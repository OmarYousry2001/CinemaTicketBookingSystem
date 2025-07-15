
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.SeatTypeMapping
{
    public partial class SeatTypeProfile
    {
        public void CreateSeatTypeMapping()
        {
            CreateMap<AddSeatTypeCommand, SeatType>()
                .ForMember(des => des.TypeNameAr, opt => opt.MapFrom(src => src.TypeNameEn))
                .ForMember(des => des.TypeNameEn, opt => opt.MapFrom(src => src.TypeNameEn))
                .ForMember(des => des.SeatTypePrice, opt => opt.MapFrom(src => src.SeatTypePrice));



        }
    }
}
