using CinemaTicketBookingSystem.Core;
using CinemaTicketBookingSystem.Core.MiddleWare;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Infrastructure;
using CinemaTicketBookingSystem.Infrastructure.Context;
using CinemaTicketBookingSystem.Service;
using DAL.ApplicationContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Text;

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
         
            #region Dependency injections
            builder.Services.AddServiceRegisteration(builder.Configuration);
            builder.Services.AddInfrastructureDependencies()
                             .AddServiceDependencies()
                             .AddCoreDependencies();
            #endregion

            #region AllowCORS
            var CORS = "_AllowCORS";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CORS,
                                  policy =>
                                  {
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyOrigin();
                                  });
            });

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
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = "Endpoint"; // Set this to match your API path if needed
                });

                app.MapGet("/", async context =>
                {
                    context.Response.Redirect("/Endpoint/");
                    await context.Response.CompleteAsync(); // Ensure the async Task is returned
                });
            }

            #region Localization Middleware
            //var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(options.Value);
            #endregion

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseCors(CORS);
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            #region Seeding Data
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
            #endregion

            app.Run();
        }
    }
}
