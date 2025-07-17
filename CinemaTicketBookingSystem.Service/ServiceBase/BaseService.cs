
using CinemaTicketBookingSystem.Data.Base;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.ServiceBase
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly ITableRepositoryAsync<T> _baseRepository;


        public BaseService(ITableRepositoryAsync<T> baseRepository)
        {
            _baseRepository = baseRepository;

        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {

            return await _baseRepository.GetTableNoTracking().Where(x => x.CurrentState == 1).ToListAsync();

        }
        public virtual async Task<T> FindByIdAsync(Guid Id)
        {
            return await _baseRepository.FindByIdAsync(Id);
        }

     
        public virtual async Task<bool> SaveAsync(T entity, Guid userId)
        {
            return await _baseRepository.SaveChangesAsync(entity, userId);
        }

        public virtual async Task<bool> AddAsync(T entity, Guid creatorId)
        {
          return await _baseRepository.AddAsync(entity, creatorId);
        }

        public async Task<bool> UpdateAsync(T entity, Guid updaterId)
        {
            return await _baseRepository.UpdateAsync(entity, updaterId);
        }

        public virtual async Task<bool> DeleteAsync(T entity )
        {
          return await _baseRepository.UpdateCurrentStateAsync(entity);
        }
        public virtual void PrepareEntity(T entity, Guid userId) 
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = userId;
            entity.CreatedDateUtc = DateTime.UtcNow;
            entity.CurrentState = 1;
        }


    }
}
