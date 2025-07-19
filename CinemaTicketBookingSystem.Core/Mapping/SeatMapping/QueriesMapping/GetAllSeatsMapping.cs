

using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results;
using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results.Shared;
using CinemaTicketBookingSystem.Data.Entities;


namespace CinemaTicketBookingSystem.Core.Mapping.SeatMapping
{
    public partial class SeatProfile
    {
        public void GetAllSeatsMapping()
        {
            CreateMap<Seat, GetAllSeatsResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(des => des.SeatNumber, opt => opt.MapFrom(x => x.SeatNumber))
                .ForMember(des => des.Hall, opt => opt.MapFrom(x => x.Hall))
                .ForMember(des => des.SeatType, opt => opt.MapFrom(x => x.SeatType));


            CreateMap<Hall, HallInSeatResponse>()
           .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
           .ForMember(des => des.Name, opt => opt.MapFrom(x => x.Localize(x.NameAr, x.NameEn)));
            //.ForMember(des => des.NameEn, opt => opt.MapFrom(x => x.NameEn))
            //.ForMember(des => des.Name, opt => opt.MapFrom(x => x.NameAr));


            CreateMap<SeatType, SeatTypeInSeatResponse>()
            //.ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            //.ForMember(des => des.TypeName, opt => opt.MapFrom(x => x.Localize(x.TypeNameAr, x.TypeNameEn)));
            .ForMember(des => des.TypeName, opt => opt.MapFrom(x => x.TypeNameEn));


        }
    }
}
