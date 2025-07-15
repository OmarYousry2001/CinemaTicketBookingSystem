
using CinemaTicketBookingSystem.Data.Base;
using CinemaTicketBookingSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories
{
    public class TableRepositoryAsync<T> : ITableRepositoryAsync<T> where T : BaseEntity
    {
        #region Vars / Props

        protected readonly ApplicationDBContext _dbContext;
        protected DbSet<T> DbSet => _dbContext.Set<T>();

        #endregion

        #region Constructor(s)
        public TableRepositoryAsync(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion


        #region Methods

        #endregion

        #region Actions
        // public IEnumerable<T> GetAll()
        //{
        //    return DbSet.AsNoTracking().Where(x => x.CurrentState == 1);
        //}

        public IQueryable<T> GetTableNoTracking()
        {
            return DbSet.AsNoTracking().AsQueryable();
        }



        public IQueryable<T> GetTableAsTracking()
        {
            return DbSet.AsQueryable();

        }

        public virtual async Task<T> FindByIdAsync(Guid id)
        {

            return await DbSet.Where(x => x.Id == id && x.CurrentState == 1)
                              .AsNoTracking()
                              .FirstOrDefaultAsync();
        }



        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

        }
  

        public virtual async Task<bool> UpdateAsync(T model, Guid updaterId)
        {

            var existingEntity = await FindByIdAsync(model.Id);

            if (existingEntity == null)
                return false;

            model.UpdatedDateUtc = DateTime.UtcNow;
            model.UpdatedBy = updaterId;
            model.CreatedBy = existingEntity.CreatedBy;
            model.CurrentState = existingEntity.CurrentState;
            model.CreatedDateUtc = existingEntity.CreatedDateUtc;

            DbSet.Entry(model).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync() > 0;

        }

        public virtual async Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync(T model, Guid userId)
        {
            if (model.Id == Guid.Empty)
                return await AddAsync(model, userId);
            else
                return await UpdateAsync(model, userId);

        }



        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }



        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            DbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollBackAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public virtual async Task<bool> AddAsync(T model, Guid creatorId)
        {

            model.Id = Guid.NewGuid();
            model.CreatedDateUtc = DateTime.UtcNow;
            model.CreatedBy = creatorId;
            model.CurrentState = 1;
            await DbSet.AddAsync(model);

            return await _dbContext.SaveChangesAsync() > 0;
        }


        /// <summary>
        /// Updates the CurrentState of an entity.
        /// </summary>
        public async Task<bool> UpdateCurrentState(T entity, int newValue = 0)
        {
            entity.CurrentState = newValue;
            DbSet.Update(entity);
            return await _dbContext.SaveChangesAsync() > 0;


        }
        #endregion
    }
}
