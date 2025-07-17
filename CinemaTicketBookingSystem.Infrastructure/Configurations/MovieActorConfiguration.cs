using CinemaTicketBookingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketBookingSystem.Infrastructure.Configurations
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {

            builder.HasKey(ma => new { ma.MovieId, ma.ActorId });


            builder.Property(ma => ma.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(ma => ma.Movie)
                   .WithMany(m => m.MovieActors)
                   .HasForeignKey(ma => ma.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ma => ma.Actor)
                   .WithMany(a => a.MovieActors)
                   .HasForeignKey(ma => ma.ActorId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.Property(ma => ma.CurrentState)
                   .HasDefaultValue(1);

            builder.Property(ma => ma.CreatedDateUtc)
                   .IsRequired();

            builder.Property(ma => ma.CreatedBy)
                   .IsRequired();

            builder.Property(ma => ma.UpdatedBy);
            builder.Property(ma => ma.UpdatedDateUtc);
        }
    }
}
