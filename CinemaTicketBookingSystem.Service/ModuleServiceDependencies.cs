using BL.GeneralService.CMS;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.UnitOfWork;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.Implementations;
using CinemaTicketBookingSystem.Service.Implementations.CMS;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaTicketBookingSystem.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IActorService, ActorService>();
            services.AddTransient<IDirectorService, DirectorService>();
            services.AddTransient<IHallService, HallService>();
            services.AddTransient<ISeatTypeService, SeatTypeService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IShowTimeService, ShowTimeService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IApplicationUserService,ApplicationUserService>();
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<IImageProcessingService, ImageProcessingService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}
