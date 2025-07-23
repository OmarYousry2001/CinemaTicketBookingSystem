using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class ActorService : BaseService<Actor>, IActorService
    {
        private readonly ITableRepositoryAsync<Actor> _actorRepository;
        private readonly IFileUploadService _fileUploadService;

        public ActorService(ITableRepositoryAsync<Actor> actorRepository,
                    IFileUploadService fileUploadService) : base(actorRepository)
        {
            _actorRepository = actorRepository;
            _fileUploadService = fileUploadService;
        }
        public IQueryable<Actor> GetAllQueryable()
        {
            return _actorRepository.GetTableAsTracking().AsQueryable();

        }
        public  async Task<bool> SaveAndUploadImageAsync(Actor entity, Guid userId , IFormFile file)
        {
            if (!string.IsNullOrEmpty(entity.ImageURL))
            {
                entity.ImageURL = await _fileUploadService.UploadFileAsync(file, "Actors", entity.ImageURL);  

                return await _actorRepository.SaveChangesAsync(entity, userId);

            }
            return false;
        }

        public async Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string firstName, string lastName)
        {
            return await _actorRepository.GetTableNoTracking().AnyAsync(d =>
                d.Id != id &&
                d.FirstName.ToLower().Trim() == firstName.ToLower().Trim() &&
                d.LastName.ToLower().Trim() == lastName.ToLower().Trim());
        }

        public async Task<bool> IsExistByNameAsync(string firstName, string lastName)
        {
            return await _actorRepository.GetTableNoTracking()
                .AnyAsync(d =>
                    d.FirstName.Trim().ToLower() == firstName.Trim().ToLower() &&
                    d.LastName.Trim().ToLower() == lastName.Trim().ToLower());
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _actorRepository.GetTableNoTracking()
         .AnyAsync(d => d.Id == id);
        }
    }
}
