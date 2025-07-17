using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class HallService : BaseService<Hall>, IHallService
    {
        private readonly ITableRepositoryAsync<Hall> _tableRepositoryAsync;
        private readonly IFileUploadService _fileUploadService;


        public HallService(ITableRepositoryAsync<Hall> tableRepositoryAsync,
                    IFileUploadService fileUploadService) : base(tableRepositoryAsync)
        {
            _tableRepositoryAsync = tableRepositoryAsync;
            _fileUploadService = fileUploadService;
        }
 

        public async Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string NameEn, string NameAr)
        {
            return await _tableRepositoryAsync.GetTableNoTracking().AnyAsync(d =>
                d.Id != id &&
                d.NameEn.ToLower().Trim() == NameEn.ToLower().Trim() &&
                d.NameAr.ToLower().Trim() == NameAr.ToLower().Trim());
        }


        public async Task<bool> IsExistByNameAsync(string NameEn, string NameAr)
        {
            return await _tableRepositoryAsync.GetTableNoTracking()
                .AnyAsync(d =>
                    d.NameEn.Trim().ToLower() == NameEn.Trim().ToLower() &&
                    d.NameAr.Trim().ToLower() == NameAr.Trim().ToLower());
        }


        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _tableRepositoryAsync.GetTableNoTracking()
         .AnyAsync(d => d.Id == id);
        }
    }
}
