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
    public class SeatTypeConfiguration : IEntityTypeConfiguration<SeatType>
    {
        public void Configure(EntityTypeBuilder<SeatType> builder)
        {
            builder.HasKey(st => st.Id);
            builder.Property(st => st.TypeNameAr).HasMaxLength(100);
            builder.Property(st => st.TypeNameEn).HasMaxLength(100);
        }
    }
}
