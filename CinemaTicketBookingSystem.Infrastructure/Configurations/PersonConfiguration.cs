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
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirstName).HasMaxLength(100);
            builder.Property(p => p.LastName).HasMaxLength(100);
            builder.Property(p => p.ImageURL).HasMaxLength(500);
            builder.Property(p => p.Bio).HasMaxLength(1000);
        }
    }
}
