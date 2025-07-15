using CinemaTicketBookingSystem.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface ISeatTypeService : IBaseService<Data.Entities.SeatType>    
    {

        Task<bool> IsExistByNameAsync(string NameEn, string NameAr);
        Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string NameEn, string NameAr);
    }
}
