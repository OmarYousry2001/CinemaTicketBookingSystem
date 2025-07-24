
using CinemaTicketBookingSystem.Core.Filters;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace CinemaTicketBookingSystem.API
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {
            #region Connection To SQL Server
            services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, Role>(option =>
           {
               // Password settings.
               option.Password.RequireDigit = true;
               option.Password.RequireLowercase = true;
               option.Password.RequireNonAlphanumeric = true;
               option.Password.RequireUppercase = true;
               option.Password.RequiredLength = 6;
               option.Password.RequiredUniqueChars = 1;

               // Lockout settings.
               option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
               option.Lockout.MaxFailedAccessAttempts = 5;
               option.Lockout.AllowedForNewUsers = true;

               // User settings.
               option.User.AllowedUserNameCharacters =
               "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
               option.User.RequireUniqueEmail = true;
               option.SignIn.RequireConfirmedEmail = true;

           }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders(); 
            #endregion

            #region Localization
            services.AddControllersWithViews();
            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "";
            });

            #endregion

            #region Swagger Gn

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinema Ticket Booking System Project", Version = "v1" });
              c.EnableAnnotations();

              c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
              {
                  Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                  Name = "Authorization",
                  In = ParameterLocation.Header,
                  Type = SecuritySchemeType.ApiKey,
                  Scheme = JwtBearerDefaults.AuthenticationScheme
              });

              c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
            }
         });
          });

            #endregion

            #region Emails Settings
            var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            services.AddSingleton(emailSettings);
            #endregion

            #region UrlHelper
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
            #endregion

            #region Filters
            services.AddTransient<DataEntryRoleFilter>();
            #endregion

            #region Claims
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Create", policy =>
                {
                    policy.RequireClaim("Create", "True");
                });
                option.AddPolicy("Delete", policy =>
                {
                    policy.RequireClaim("Delete", "True");
                });
                option.AddPolicy("Edit", policy =>
                {
                    policy.RequireClaim("Edit", "True");
                });
            });
            #endregion

            #region JWT Authentication
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuers = new[] { jwtSettings.Issuer },
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidAudience = jwtSettings.Audience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifeTime,
                };
            });
            #endregion

            #region Serilog
            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
            services.AddSerilog();
            #endregion

         

            return services;
        }
   
}
}
