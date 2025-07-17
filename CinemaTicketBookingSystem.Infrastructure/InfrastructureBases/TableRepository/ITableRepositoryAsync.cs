using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories
{
    public interface ITableRepositoryAsync<T> where T : class
    {
        Task DeleteRangeAsync(ICollection<T> entities);
        Task<T> FindByIdAsync(Guid id);
        Task<bool> SaveChangesAsync(T model, Guid userId);
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        public Task<bool> AddAsync(T model, Guid creatorId);

        Task AddRangeAsync(ICollection<T> entities);
        public Task<bool> UpdateAsync(T model, Guid updaterId);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
        public Task<bool> UpdateCurrentStateAsync(T entity, int newValue = 0);
        public Task<T> AddAndReturnAsync(T model, Guid creatorId);
        public Task AddRangeAsync(ICollection<T> entities, Guid creatorId);
    }
}
