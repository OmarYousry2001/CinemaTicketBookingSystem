
using CinemaTicketBookingSystem.Data.Base;
using CinemaTicketBookingSystem.Infrastructure.Context;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;



namespace CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private bool _disposed;
        private IDbContextTransaction _transaction;
        //private readonly ILogger _logger;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            //_logger = logger;
        }

        //public IRepository<T> Repository<T>() where T : class
        //{
        //    var type = typeof(T);
        //    if (!_repositories.TryGetValue(type, out var repo))
        //    {
        //        repo = new Repository<T>(_context, _logger);
        //        _repositories[type] = repo;
        //    }
        //    return (IRepository<T>)repo;
        //}

        public ITableRepositoryAsync<TD> TableRepository<TD>() where TD : BaseEntity
        {
            var type = typeof(TD);
            if (!_repositories.TryGetValue(type, out var repo))
            {
                repo = new TableRepositoryAsync<TD>(_context);
                _repositories[type] = repo;
            }
            return (TableRepositoryAsync<TD>)repo;
        }

        public DbContext GetContext() => _context; // Add this method

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task<int> Commit()
        {
            _transaction?.Commit();
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null!;
        }

        public async Task DisposeAsync()
        {
            if (!_disposed)
            {
                _transaction?.Dispose();
                await _context.DisposeAsync();
                _repositories.Clear();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _transaction?.Dispose();
                _context.Dispose();
                _repositories.Clear();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }


    }

}
