using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class DirectorService : BaseService<Director>, IDirectorService
    {
        #region Fields
        private readonly ITableRepositoryAsync<Director> _tableRepositoryAsync;
        private readonly IFileUploadService _fileUploadService;
        #endregion

        #region Constructors
        public DirectorService(ITableRepositoryAsync<Director> tableRepositoryAsync,
                 IFileUploadService fileUploadService) : base(tableRepositoryAsync)
        {
            _tableRepositoryAsync = tableRepositoryAsync;
            _fileUploadService = fileUploadService;
        }
        #endregion

        #region Methods
        public async Task<bool> SaveAndUploadImageAsync(Director entity, Guid userId, IFormFile file)
        {
            if (!string.IsNullOrEmpty(entity.ImageURL))
            {
                entity.ImageURL = await _fileUploadService.UploadFileAsync(file, "Directors", entity.ImageURL);

                return await _tableRepositoryAsync.SaveChangesAsync(entity, userId);

            }
            return false;
        }
        public async Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string firstName, string lastName)
        {
            return await _tableRepositoryAsync.GetTableNoTracking().AnyAsync(d =>
                d.Id != id &&
                d.FirstName.ToLower().Trim() == firstName.ToLower().Trim() &&
                d.LastName.ToLower().Trim() == lastName.ToLower().Trim());
        }


        public async Task<bool> IsExistByNameAsync(string firstName, string lastName)
        {
            return await _tableRepositoryAsync.GetTableNoTracking()
                .AnyAsync(d =>
                    d.FirstName.Trim().ToLower() == firstName.Trim().ToLower() &&
                    d.LastName.Trim().ToLower() == lastName.Trim().ToLower());
        }


        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _tableRepositoryAsync.GetTableNoTracking()
         .AnyAsync(d => d.Id == id);
        }


        #endregion
    }
}