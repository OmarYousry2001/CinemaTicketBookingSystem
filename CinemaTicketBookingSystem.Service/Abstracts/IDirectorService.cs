using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IDirectorService
    {
        Task<bool> IsExistAsync(Guid id);
        Task<bool> IsExistByNameAsync(string firstName, string lastName);
        Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string firstName, string lastName);
    }
}
