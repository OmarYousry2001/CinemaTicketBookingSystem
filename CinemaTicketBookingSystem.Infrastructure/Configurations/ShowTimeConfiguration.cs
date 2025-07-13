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
    public class ShowTimeConfiguration : IEntityTypeConfiguration<ShowTime>
    {
        public void Configure(EntityTypeBuilder<ShowTime> builder)
        {
            builder.HasKey(st => st.Id);

            builder.HasOne(st => st.Hall)
                   .WithMany(h => h.ShowTimes)
                   .HasForeignKey(st => st.HallId);

            builder.HasOne(st => st.Movie)
                   .WithMany(m => m.ShowTimes)
                   .HasForeignKey(st => st.MovieId);
        }
    }
}
