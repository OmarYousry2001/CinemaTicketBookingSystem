using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;


namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IActorService : IBaseService<Actor>
    {
        Task<bool> IsExistAsync(Guid id);
        Task<bool> IsExistByNameAsync(string firstName, string lastName);
        Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string firstName, string lastName);
        public Task<bool> SaveAndUploadImageAsync(Actor entity, Guid userId, IFormFile file);
        public IQueryable<Actor> GetAllQueryable();
    }
}
