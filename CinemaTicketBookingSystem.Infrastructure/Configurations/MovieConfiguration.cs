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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.TitleAr).HasMaxLength(255);
            builder.Property(m => m.TitleEn).HasMaxLength(255);
            builder.Property(m => m.DescriptionAr).HasMaxLength(1000);
            builder.Property(m => m.DescriptionEn).HasMaxLength(1000);
            builder.Property(m => m.PosterURL).HasMaxLength(500);

            builder.HasOne(m => m.Director)
                   .WithMany(d => d.Movies)
                   .HasForeignKey(m => m.DirectorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
