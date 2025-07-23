
using CinemaTicketBookingSystem.Core.Filters;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.API
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {
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
            services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });
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

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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

            return services;
        }
   
}
}
