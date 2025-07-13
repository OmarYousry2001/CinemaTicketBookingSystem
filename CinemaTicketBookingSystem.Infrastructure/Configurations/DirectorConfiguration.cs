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
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasKey(d => d.Id);

            //builder.HasOne(d => d.Person)
            //       .WithOne(p => p.Director)
            //       .HasForeignKey<Director>(d => d.PersonId)
            //       .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Movies)
                   .WithOne(m => m.Director)
                   .HasForeignKey(m => m.DirectorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
