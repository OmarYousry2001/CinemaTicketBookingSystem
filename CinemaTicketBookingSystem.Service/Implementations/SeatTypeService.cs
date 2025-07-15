using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class SeatTypeService : BaseService<SeatType>, ISeatTypeService
    {
        private readonly ITableRepositoryAsync<SeatType> _tableRepositoryAsync;
        private readonly IFileUploadService _fileUploadService;


        public SeatTypeService(ITableRepositoryAsync<SeatType> tableRepositoryAsync,
                    IFileUploadService fileUploadService) : base(tableRepositoryAsync)
        {
            _tableRepositoryAsync = tableRepositoryAsync;
            _fileUploadService = fileUploadService;
        }



        public async Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string NameEn, string NameAr)
        {
            return await _tableRepositoryAsync.GetTableNoTracking().AnyAsync(d =>
                d.Id != id &&
                d.TypeNameEn.ToLower().Trim() == NameEn.ToLower().Trim() &&
                d.TypeNameAr.ToLower().Trim() == NameAr.ToLower().Trim());
        }


        public async Task<bool> IsExistByNameAsync(string NameEn, string NameAr)
        {
            return await _tableRepositoryAsync.GetTableNoTracking()
                .AnyAsync(d =>
                    d.TypeNameEn.Trim().ToLower() == NameEn.Trim().ToLower() &&
                    d.TypeNameAr.Trim().ToLower() == NameAr.Trim().ToLower());
        }



    }
}
