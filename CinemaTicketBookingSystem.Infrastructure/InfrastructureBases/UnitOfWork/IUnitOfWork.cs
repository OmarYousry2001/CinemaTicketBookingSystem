using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Data.Base;


namespace CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<T> Repository<T>() where T : class;
        ITableRepositoryAsync<TD> TableRepository<TD>() where TD : BaseEntity;
        //ITableRepositoryAsync<T> TableRepository<T>() where T : class;
        Task<IDbContextTransaction> BeginTransactionAsync();
         Task<int> Commit() ;
        void Rollback();
        Task DisposeAsync(); // Changed from Task ValueTask to Task for simplicity
        DbContext GetContext(); // Add this method
    }
}
