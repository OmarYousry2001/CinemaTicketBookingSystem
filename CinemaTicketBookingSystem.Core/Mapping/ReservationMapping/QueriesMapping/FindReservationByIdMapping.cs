
using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results;
using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results.Shared;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Entities.Identity;

namespace CinemaTicketBookingSystem.Core.Mapping.ReservationMapping
{
    public partial class ReservationProfile
    {
        public void FindReservationByIdMapping()
        {
            CreateMap<Reservation, FindReservationByIdResponse>()
                .ForMember(des => des.HallName, option => option.MapFrom(src => src.ShowTime.Hall.Localize(src.ShowTime.Hall.NameAr , src.ShowTime.Hall.NameEn)));

            CreateMap<ShowTime, ShowTimeInReservationResponse>()
                .ForMember(des => des.MovieName, option => option.MapFrom(src => src.Movie.Localize(src.Movie.TitleAr, src.Movie.TitleEn)));

            CreateMap<ApplicationUser, UserInReservationResponse>();

            CreateMap<ReservationSeat, SeatsInReservationResponse>()
                .ForMember(des => des.SeatNumber, option => option.MapFrom(src => src.Seat.SeatNumber))
                .ForMember(des => des.Id, option => option.MapFrom(src => src.Seat.Id));

        }
    }
}
