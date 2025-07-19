
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            //services.AddTransient<IStudentRepository, StudentRepository>();
            //services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            //services.AddTransient<IInstructorsRepository, InstructorsRepository>();
            //services.AddTransient<ISubjectRepository, SubjectRepository>();
            //services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            //services.AddTransient<ISeatRepository, SeatRepository>();


            services.AddTransient(typeof(ITableRepositoryAsync<>), typeof(TableRepositoryAsync<>));

            //views
            //services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

    

            return services;
        }
    }
}
