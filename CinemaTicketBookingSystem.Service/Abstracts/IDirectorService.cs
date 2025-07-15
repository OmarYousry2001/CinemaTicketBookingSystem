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
    public interface IDirectorService  : IBaseService<Director>
    {
        Task<bool> IsExistAsync(Guid id);
        Task<bool> IsExistByNameAsync(string firstName, string lastName);
        Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string firstName, string lastName);
        public Task<bool> SaveAndUploadImageAsync(Director entity, Guid userId, IFormFile file);
    }
}
