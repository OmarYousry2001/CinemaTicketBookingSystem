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
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SeatNumber).HasMaxLength(10);

            builder.HasOne(s => s.Hall)
                   .WithMany(h => h.Seats)
                   .HasForeignKey(s => s.HallId);

            builder.HasOne(s => s.SeatType)
                   .WithMany(st => st.Seats)
                   .HasForeignKey(s => s.SeatTypeId);
        }
    }
}
