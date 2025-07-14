using CinemaTicketBookingSystem.Data.AppMetaData;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Enums;
using CinemaTicketBookingSystem.Infrastructure.Context;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ApplicationContext
{
    public class ContextConfigurations
    {
        private static readonly string seedAdminEmail = "admin@gmail.com";
        private static readonly string seedAdminPassword = "Omar_123456";

        public static async Task SeedDataAsync(ApplicationDBContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<Role> roleManager)
        {
            // Seed user first to get admin user ID
            var adminUserId = await SeedUserAsync(userManager, roleManager);

            // Seed E-commerce data
            await SeedECommerceDataAsync(context, adminUserId);
        }
        private static async Task SeedECommerceDataAsync(ApplicationDBContext context, Guid adminUserId)
        {
            // 1. Seed Actors
            if (!context.Actors.Any())
            {
                var actors = new List<Actor>
    {
        new Actor
        {
            Id = Guid.NewGuid(),
            FirstName = "Tom",
            LastName = "Hanks",
            ImageURL = "images/actors/tom-hanks.webp",
            BirthDate = new DateOnly(1956, 7, 9),
            Bio = "American actor and filmmaker known for roles in Forrest Gump and Cast Away.",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Actor
        {
            Id = Guid.NewGuid(),
            FirstName = "Scarlett",
            LastName = "Johansson",
            ImageURL = "images/actors/scarlett-johansson.webp",
            BirthDate = new DateOnly(1984, 11, 22),
            Bio = "American actress known for her role as Black Widow in the Marvel Cinematic Universe.",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Actor
        {
            Id = Guid.NewGuid(),
            FirstName = "Leonardo",
            LastName = "DiCaprio",
            ImageURL = "images/actors/leonardo-dicaprio.webp",
            BirthDate = new DateOnly(1974, 11, 11),
            Bio = "American actor and producer, known for Titanic, Inception, and The Revenant.",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        }
    };

                await context.Actors.AddRangeAsync(actors);
                await context.SaveChangesAsync();
            }
            // 2. Seed Directors
            if (!context.Directors.Any())
            {
                var directors = new List<Director>
    {
        new Director
        {
            Id = Guid.NewGuid(),
            FirstName = "Christopher",
            LastName = "Nolan",
            ImageURL = "images/directors/christopher-nolan.webp",
            BirthDate = new DateOnly(1970, 7, 30),
            Bio = "British-American film director known for Inception, Interstellar, and The Dark Knight Trilogy.",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Director
        {
            Id = Guid.NewGuid(),
            FirstName = "Steven",
            LastName = "Spielberg",
            ImageURL = "images/directors/steven-spielberg.webp",
            BirthDate = new DateOnly(1946, 12, 18),
            Bio = "American director known for E.T., Jurassic Park, and Saving Private Ryan.",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Director
        {
            Id = Guid.NewGuid(),
            FirstName = "Quentin",
            LastName = "Tarantino",
            ImageURL = "images/directors/quentin-tarantino.webp",
            BirthDate = new DateOnly(1963, 3, 27),
            Bio = "American filmmaker known for Pulp Fiction and Django Unchained.",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        }
    };

                await context.Directors.AddRangeAsync(directors);
                await context.SaveChangesAsync();
            }
            // 3. Seed Genres
            if (!context.Genres.Any())
            {
                var genres = new List<Genre>
    {
        new Genre
        {
            Id = Guid.NewGuid(),
            NameAr = "أكشن",
            NameEn = "Action",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Genre
        {
            Id = Guid.NewGuid(),
            NameAr = "دراما",
            NameEn = "Drama",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Genre
        {
            Id = Guid.NewGuid(),
            NameAr = "كوميديا",
            NameEn = "Comedy",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Genre
        {
            Id = Guid.NewGuid(),
            NameAr = "رعب",
            NameEn = "Horror",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Genre
        {
            Id = Guid.NewGuid(),
            NameAr = "خيال علمي",
            NameEn = "Sci-Fi",
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        }
    };

                await context.Genres.AddRangeAsync(genres);
                await context.SaveChangesAsync();
            }
            // 4. Seed Halls
            if (!context.Halls.Any())
            {
                var halls = new List<Hall>
    {
        new Hall
        {
            Id = Guid.NewGuid(),
            NameAr = "القاعة الرئيسية",
            NameEn = "Main Hall",
            Capacity = 150,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Hall
        {
            Id = Guid.NewGuid(),
            NameAr = "قاعة VIP",
            NameEn = "VIP Hall",
            Capacity = 50,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Hall
        {
            Id = Guid.NewGuid(),
            NameAr = "قاعة الأطفال",
            NameEn = "Kids Hall",
            Capacity = 80,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        }
    };

                await context.Halls.AddRangeAsync(halls);
                await context.SaveChangesAsync();
            }
            // 5. Seed SeatTypes
            if (!context.SeatTypes.Any())
            {
                var seatTypes = new List<SeatType>
    {
        new SeatType
        {
            Id = Guid.NewGuid(),
            TypeNameAr = "عادي",
            TypeNameEn = "Standard",
            SeatTypePrice = 50.00m,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new SeatType
        {
            Id = Guid.NewGuid(),
            TypeNameAr = "ممتاز",
            TypeNameEn = "Premium",
            SeatTypePrice = 80.00m,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new SeatType
        {
            Id = Guid.NewGuid(),
            TypeNameAr = "VIP",
            TypeNameEn = "VIP",
            SeatTypePrice = 120.00m,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        }
    };

                await context.SeatTypes.AddRangeAsync(seatTypes);
                await context.SaveChangesAsync();
            }
            // 7. Seed Movies
            if (!context.Movies.Any())
            {
                var directors = await context.Directors.Take(3).ToListAsync();
                var genres = await context.Genres.Take(3).ToListAsync();
                var actors = await context.Actors.Take(3).ToListAsync();

                var movies = new List<Movie>
    {
        new Movie
        {
            Id = Guid.NewGuid(),
            TitleAr = "الاختيار",
            TitleEn = "The Choice",
            DescriptionAr = "فيلم درامي حربي يتناول قصة حقيقية.",
            DescriptionEn = "A war drama based on a true story.",
            PosterURL = "images/movies/the-choice.webp",
            DurationInMinutes = 135,
            ReleaseYear = 2020,
            Rate = RatingEnum.FiveStars,
            IsActive = true,
            DirectorId = directors[0].Id,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Movie
        {
            Id = Guid.NewGuid(),
            TitleAr = "الفيل الأزرق",
            TitleEn = "The Blue Elephant",
            DescriptionAr = "فيلم تشويق وإثارة نفسية.",
            DescriptionEn = "A psychological thriller.",
            PosterURL = "images/movies/blue-elephant.webp",
            DurationInMinutes = 150,
            ReleaseYear = 2019,
            Rate = RatingEnum.FourStars,
            IsActive = true,
            DirectorId = directors[1].Id,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Movie
        {
            Id = Guid.NewGuid(),
            TitleAr = "هيبتا",
            TitleEn = "Hepta",
            DescriptionAr = "فيلم رومانسي عن الحب في مراحله السبعة.",
            DescriptionEn = "A romantic film about the seven stages of love.",
            PosterURL = "images/movies/hepta.webp",
            DurationInMinutes = 130,
            ReleaseYear = 2016,
            Rate = RatingEnum.ThreeStars,
            IsActive = true,
            DirectorId = directors[2].Id,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Movie
        {
            Id = Guid.NewGuid(),
            TitleAr = "تراب الماس",
            TitleEn = "Diamond Dust",
            DescriptionAr = "فيلم غموض وجريمة.",
            DescriptionEn = "A mystery and crime film.",
            PosterURL = "images/movies/diamond-dust.webp",
            DurationInMinutes = 140,
            ReleaseYear = 2018,
            Rate = RatingEnum.FourStars,
            IsActive = true,
            DirectorId = directors[0].Id,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        },
        new Movie
        {
            Id = Guid.NewGuid(),
            TitleAr = "اشتباك",
            TitleEn = "Clash",
            DescriptionAr = "فيلم سياسي داخل عربة ترحيلات.",
            DescriptionEn = "A political film set inside a police van.",
            PosterURL = "images/movies/clash.webp",
            DurationInMinutes = 90,
            ReleaseYear = 2016,
            Rate = RatingEnum.FiveStars,
            IsActive = true,
            DirectorId = directors[1].Id,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow,
            CurrentState = 1
        }
    };

                await context.Movies.AddRangeAsync(movies);
                await context.SaveChangesAsync();
            }
            // 8. Seed MovieActor
            if (!context.MovieActors.Any())
            {
                var movie = await context.Movies.FirstOrDefaultAsync();
                var actors = await context.Actors.Take(2).ToListAsync();

                var movieActors = actors.Select(actor => new MovieActor
                {
                    MovieId = movie.Id,
                    ActorId = actor.Id
                }).ToList();

                await context.MovieActors.AddRangeAsync(movieActors);
                await context.SaveChangesAsync();
            }
            // 9. Seed MovieGenre
            if (!context.MovieGenres.Any())
            {
                var movie = await context.Movies.FirstOrDefaultAsync();
                var genres = await context.Genres.Take(2).ToListAsync();

                var movieGenres = genres.Select(genre => new MovieGenre
                {
                    MovieId = movie.Id,
                    GenreId = genre.Id
                }).ToList();

                await context.MovieGenres.AddRangeAsync(movieGenres);
                await context.SaveChangesAsync();
            }
            // 10. Seed Seats
            if (!context.Seats.Any())
            {
                var hall = await context.Halls.FirstOrDefaultAsync();
                var seatType = await context.SeatTypes.FirstOrDefaultAsync();

                var seats = new List<Seat>();

                for (int i = 1; i <= 30; i++) 
                {
                    seats.Add(new Seat
                    {
                        Id = Guid.NewGuid(),
                        SeatNumber = $"A{i}",
                        HallId = hall.Id,
                        SeatTypeId = seatType.Id,
                        CurrentState = 1,
                        CreatedBy = adminUserId,
                        CreatedDateUtc = DateTime.UtcNow
                    });
                }

                await context.Seats.AddRangeAsync(seats);
                await context.SaveChangesAsync();
            }
            // 11. Seed ShowTimes
            if (!context.ShowTimes.Any())
            {
                var hall = await context.Halls.FirstOrDefaultAsync();
                var movie = await context.Movies.FirstOrDefaultAsync();

                var showTimes = new List<ShowTime>
    {
        new ShowTime
        {
            Id = Guid.NewGuid(),
            Day = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            StartTime = new TimeOnly(18, 0), 
            EndTime = new TimeOnly(20, 0),   
            ShowTimePrice = 100m,
            HallId = hall.Id,
            MovieId = movie.Id,
            CurrentState = 1,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow
        },
        new ShowTime
        {
            Id = Guid.NewGuid(),
            Day = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
            StartTime = new TimeOnly(21, 0), 
            EndTime = new TimeOnly(23, 0),   
            ShowTimePrice = 120m,
            HallId = hall.Id,
            MovieId = movie.Id,
            CurrentState = 1,
            CreatedBy = adminUserId,
            CreatedDateUtc = DateTime.UtcNow
        }
    };

                await context.ShowTimes.AddRangeAsync(showTimes);
                await context.SaveChangesAsync();
            }

        }
        private static async Task<Guid> SeedUserAsync(UserManager<ApplicationUser> userManager,
                    RoleManager<Role> roleManager)
        {
            // Ensure roles exist
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new Role { Name = Roles.Admin });
            }

            if (!await roleManager.RoleExistsAsync(Roles.DataEntry))
            {
                await roleManager.CreateAsync(new Role { Name = Roles.DataEntry });
            }

            if (!await roleManager.RoleExistsAsync(Roles.User))
            {
                await roleManager.CreateAsync(new Role { Name = Roles.User });
            }

            // Ensure admin user exists
            var adminEmail = seedAdminEmail;
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var id = Guid.NewGuid().ToString();
                adminUser = new ApplicationUser
                {
                    Id = id,
                    UserName = adminEmail,
                    FullName="Omar Admin",
                    EmailConfirmed = true,
                    Email = adminEmail,
                    CreatedDateUtc = DateTime.UtcNow
                };
                var result = await userManager.CreateAsync(adminUser, seedAdminPassword);
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }

            // Convert adminUser.Id from string to Guid
            return Guid.Parse(adminUser.Id);  // Convert to Guid
        }
    }
}
