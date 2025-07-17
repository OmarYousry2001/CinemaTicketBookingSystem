namespace CinemaTicketBookingSystem.Service.ServiceBase
{
    public interface IBaseService<T>
    {
        public  Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(Guid Id);
        Task<bool> SaveAsync(T entity, Guid userId);
        Task<bool> AddAsync(T entity, Guid creatorId);
        Task<bool> UpdateAsync(T entity, Guid updaterId);
        Task<bool> DeleteAsync(T entity );
        public  void PrepareEntity(T entity, Guid userId);
    }
}
