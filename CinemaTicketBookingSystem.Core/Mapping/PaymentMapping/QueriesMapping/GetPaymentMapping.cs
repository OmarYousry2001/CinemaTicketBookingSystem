

using CinemaTicketBookingSystem.Core.Features.Payments.Queries.Results;
using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results.Shared;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Entities.Identity;

namespace CinemaTicketBookingSystem.Core.Mapping.PaymentMapping
{
    public partial class PaymentProfile
    {
        public void GetPaymentMapping()
        {

            CreateMap<Reservation, CreateOrUpdatePaymentIntentResult>()
                .ForMember(des => des.ReservationDate, opt => opt.MapFrom(x => x.CreatedDateUtc))
                .ForMember(des => des.HallName, opt => opt.MapFrom(x => x.ShowTime.Hall.Localize(x.ShowTime.Hall.NameAr, x.ShowTime.Hall.NameEn)));



            CreateMap<ShowTime, ShowTimeInReservationResponse>()
                .ForMember(des => des.MovieName, option => option.MapFrom(src => src.Movie.Localize(src.Movie.TitleAr, src.Movie.TitleEn)));

            CreateMap<ApplicationUser, UserInReservationResponse>();

            CreateMap<ReservationSeat, SeatsInReservationResponse>()
                .ForMember(des => des.SeatNumber, option => option.MapFrom(src => src.Seat.SeatNumber))
                .ForMember(des => des.Id, option => option.MapFrom(src => src.Seat.Id));
        }
    }
}
