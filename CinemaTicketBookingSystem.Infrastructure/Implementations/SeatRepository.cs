using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.Abstracts;
using CinemaTicketBookingSystem.Infrastructure.Context;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Infrastructure.Implementations
{
    public class SeatRepository : TableRepositoryAsync<Seat>, ISeatRepository     
    {
        public SeatRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public   IQueryable<Seat> FindByIdWithIncludes()
        {
            return DbSet.AsNoTracking().AsQueryable().Where(x => x.CurrentState == 1)
                 .Include(x => x.Hall)
                 .Include(x => x.SeatType);
        }
    }
}
