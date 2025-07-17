using CinemaTicketBookingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketBookingSystem.Infrastructure.Configurations
{
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {

            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });

  
            builder.Property(mg => mg.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(mg => mg.Movie)
                   .WithMany(m => m.MovieGenres)
                   .HasForeignKey(mg => mg.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mg => mg.Genre)
                   .WithMany(g => g.MovieGenres)
                   .HasForeignKey(mg => mg.GenreId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.Property(mg => mg.CurrentState)
                   .HasDefaultValue(1);

            builder.Property(mg => mg.CreatedDateUtc)
                   .IsRequired();

            builder.Property(mg => mg.CreatedBy)
                   .IsRequired();


            builder.Property(mg => mg.UpdatedBy);
            builder.Property(mg => mg.UpdatedDateUtc);
        }
    }
}
