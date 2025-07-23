

//using CinemaTicketBookingSystem.Core.Features.Halls.Commands.Models;
//using CinemaTicketBookingSystem.Data.Entities;

//namespace CinemaTicketBookingSystem.Core.Mapping.HallMapping
//{
//    public partial class HallProfile
//    {
//        public void EditHallMapping()
//        {
//            CreateMap<EditHallCommand, Hall>()
//                .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
//                .ForMember(des => des.NameAr, opt => opt.MapFrom(x => x.NameAr))
//                .ForMember(des => des.NameEn, opt => opt.MapFrom(x => x.NameEn))
//                .ForMember(des => des.Capacity, opt => opt.MapFrom(x => x.Capacity));
//        }
//    }
//}
