using CinemaTicketBookingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Infrastructure.Configurations
{
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });

            builder.HasOne(mg => mg.Movie)
                   .WithMany(m => m.MovieGenres)
                   .HasForeignKey(mg => mg.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mg => mg.Genre)
                   .WithMany(g => g.MovieGenres)
                   .HasForeignKey(mg => mg.GenreId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
