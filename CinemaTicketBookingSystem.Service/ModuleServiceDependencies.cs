using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.Implementations;
using CinemaTicketBookingSystem.Service.Implementations.CMS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            


            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<IImageProcessingService, ImageProcessingService>();

            return services;
        }
    }
}
