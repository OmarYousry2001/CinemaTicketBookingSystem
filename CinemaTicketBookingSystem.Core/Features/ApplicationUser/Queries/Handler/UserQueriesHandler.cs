using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results.Shared;
using CinemaTicketBookingSystem.Core.Features.Users.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Users.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingSystem.Core.Features.Users.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler,
       IRequestHandler<GetAllUsersQuery, Response<List<GetAllUsersResponse>>>,
       IRequestHandler<FindUserByIdQuery, Response<FindUserByIdResponse>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<GetUserReservationsHistoryQuery, Response<List<GetUserReservationsHistoryResponse>>>
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _userService;
        private readonly IReservationService _reservationService;
        #endregion

        #region Constructors
        public UserQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IApplicationUserService userService, IReservationService reservationService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
            _reservationService = reservationService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetAllUsersResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _userService.GetAllUsersQueryable().Select(u => new GetAllUsersResponse
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserName = u.UserName
            }).ToListAsync();

            return Success(usersList);
        }
        public async Task<Response<FindUserByIdResponse>> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _userService.GetAllUsersQueryable().Where(u => u.Id == request.Id)
            .Select(u => new FindUserByIdResponse
            {
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserName = u.UserName
            }).FirstOrDefaultAsync();

            if (user is null)
                return NotFound<FindUserByIdResponse>(ValidationResources.EntityNotFound);

            return Success(user);
        }
        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.ConfirmUserEmail(await _userManager.FindByIdAsync(request.UserId), request.Code);
                return Success(NotifiAndAlertsResources.EmailConfirmed);

            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }
        public async Task<Response<List<GetUserReservationsHistoryResponse>>> Handle(GetUserReservationsHistoryQuery request, CancellationToken cancellationToken)
        {
            var userReservations = await _reservationService.GetAllQueryable()
                .Where(r => r.UserId == request.Id).ToListAsync();

            var response = userReservations
                .Select(ur => new GetUserReservationsHistoryResponse
                {
                    ReservationId = ur.Id,
                    FinalPrice = ur.FinalPrice,
                    ReservationDate = ur.CreatedDateUtc,
                    HallName = ur.ShowTime.Hall.Localize(ur.ShowTime.Hall.NameAr, ur.ShowTime.Hall.NameEn),
                    PaymentStatus = ur.PaymentStatus.ToString(),
                    Seats = ur.ReservationSeats.Select(urs => new SeatsInReservationResponse
                    {
                        Id = urs.Seat.Id,
                        SeatNumber = urs.Seat.SeatNumber,
                    }),
                    ShowTime = new ShowTimeInReservationResponse
                    {
                        Id = ur.ShowTime.Id,
                        MovieName = ur.ShowTime.Movie.Localize(ur.ShowTime.Movie.TitleAr, ur.ShowTime.Movie.TitleEn),
                        Day = ur.ShowTime.Day,
                        StartTime = ur.ShowTime.StartTime,
                        EndTime = ur.ShowTime.EndTime
                    }
                }).ToList();
            return Success(response);
        } 
        #endregion
    }
}
