using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Enums;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IMovieService : IBaseService<Movie>
    {

        Task<bool> IsExistAsync(Guid id);
        Task<bool> IsExistByNameAsync(string NameEn, string NameAr);
        Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string NameEn, string NameAr);
        public Task<bool> SaveMovieWithRelationsAsync(Movie entity, Guid userId, IFormFile file);
        public Task<bool> DeleteWithImageAsync(Movie entity);
        public IQueryable<Movie> FilterMoviePaginatedQueryable(MovieOrderingEnum orderingEnum, string search);



    }
}
