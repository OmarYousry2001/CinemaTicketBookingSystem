using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Infrastructure.Abstracts
{
    public interface ISeatRepository : ITableRepositoryAsync<Seat>
    {
        public IQueryable<Seat> FindByIdWithIncludes();
    }
}
