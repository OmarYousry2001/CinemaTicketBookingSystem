using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IHallService : IBaseService<Hall>
    {
        Task<bool> IsExistAsync(Guid id);
        Task<bool> IsExistByNameAsync(string NameEn, string NameAr);
        Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string NameEn, string NameAr);

    }
}
