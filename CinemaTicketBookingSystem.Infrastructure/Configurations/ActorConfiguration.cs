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
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(a => a.Id);

            //builder.HasOne(a => a.Person)
            //       .WithOne(p => p.Actor)
            //       .HasForeignKey<Actor>(a => a.PersonId)
            //       .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.MovieActors)
                   .WithOne(ma => ma.Actor)
                   .HasForeignKey(ma => ma.ActorId);
        }
    }
}
