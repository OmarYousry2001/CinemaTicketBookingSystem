using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class GenreService : BaseService<Genre>, IGenreService
    {
        private readonly ITableRepositoryAsync<Genre> _genreRepository;
        private readonly IFileUploadService _fileUploadService;


        public GenreService(ITableRepositoryAsync<Genre> genreRepository,
                    IFileUploadService fileUploadService) : base(genreRepository)
        {
            _genreRepository = genreRepository;
            _fileUploadService = fileUploadService;
        }


        public IQueryable<Genre> GetAllQueryable()
        {
            return _genreRepository.GetTableAsTracking().AsQueryable();
        }
        public async Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string NameEn, string NameAr)
        {
            return await _genreRepository.GetTableNoTracking().AnyAsync(d =>
                d.Id != id &&
                d.NameEn.ToLower().Trim() == NameEn.ToLower().Trim() &&
                d.NameAr.ToLower().Trim() == NameAr.ToLower().Trim());
        }


        public async Task<bool> IsExistByNameAsync(string NameEn, string NameAr)
        {
            return await _genreRepository.GetTableNoTracking()
                .AnyAsync(d =>
                    d.NameEn.Trim().ToLower() == NameEn.Trim().ToLower() &&
                    d.NameAr.Trim().ToLower() == NameAr.Trim().ToLower());
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _genreRepository.GetTableNoTracking().AnyAsync(g => g.Id == id);
        }
    }
}
