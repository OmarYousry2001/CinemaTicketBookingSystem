
using CinemaTicketBookingSystem.Core;
using CinemaTicketBookingSystem.Core.MiddleWare;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Infrastructure;
using CinemaTicketBookingSystem.Infrastructure.Context;
using CinemaTicketBookingSystem.Service;
using DAL.ApplicationContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace CinemaTicketBookingSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //Connection To SQL Server
            builder.Services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            #region Dependency injections
            builder.Services.AddServiceRegisteration(builder.Configuration);
            builder.Services.AddInfrastructureDependencies()
                             .AddServiceDependencies()
                             .AddCoreDependencies();
            #endregion

            var app = builder.Build();

            #region Loaclation
            // Use resources for multi-language support
            var supportedCultures = new List<CultureInfo>
                  {
    new CultureInfo("en-US"),
    new CultureInfo("ar-EG")
};
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ar-EG"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    }
            });
            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            #region Localization Middleware
            //var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(options.Value);
            #endregion

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            // Apply migrations and seed data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                var dbContext = services.GetRequiredService<ApplicationDBContext>();

                // Apply migrations
                await dbContext.Database.MigrateAsync();

                // Seed data
                await ContextConfigurations.SeedDataAsync(dbContext, userManager, roleManager);
            }
            app.Run();
        }
    }
}
