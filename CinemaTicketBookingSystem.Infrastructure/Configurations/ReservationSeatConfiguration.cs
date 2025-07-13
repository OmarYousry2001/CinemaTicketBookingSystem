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
    public class ReservationSeatConfiguration : IEntityTypeConfiguration<ReservationSeat>
    {
        public void Configure(EntityTypeBuilder<ReservationSeat> builder)
        {
            builder.HasKey(rs => new { rs.ReservationId, rs.SeatId });

            builder.HasOne(rs => rs.Reservation)
                   .WithMany(r => r.ReservationSeats)
                   .HasForeignKey(rs => rs.ReservationId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(rs => rs.Seat)
                   .WithMany(s => s.ReservationSeats)
                   .HasForeignKey(rs => rs.SeatId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
