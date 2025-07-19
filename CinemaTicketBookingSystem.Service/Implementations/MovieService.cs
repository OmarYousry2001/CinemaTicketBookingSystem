using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Enums;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.UnitOfWork;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class MovieService : BaseService<Movie>, IMovieService
    {
        #region Fields
        private readonly ITableRepositoryAsync<Movie> _movieRepositoryAsync;
        private readonly ITableRepositoryAsync<MovieGenre> _movieGenreRepositoryAsync;
        private readonly ITableRepositoryAsync<MovieActor> _movieActorRepositoryAsync;
        private readonly IFileUploadService _fileUploadService;
        #endregion

        #region Constructors
        public MovieService(ITableRepositoryAsync<Movie> movieRepositoryAsync,
                ITableRepositoryAsync<MovieGenre> movieGenreRepositoryAsync,
                ITableRepositoryAsync<MovieActor> movieActorRepositoryAsync,
                IFileUploadService fileUploadService
           ) : base(movieRepositoryAsync)
        {
            _movieRepositoryAsync = movieRepositoryAsync;
            _fileUploadService = fileUploadService;
            _movieGenreRepositoryAsync = movieGenreRepositoryAsync;
            _movieActorRepositoryAsync = movieActorRepositoryAsync;
        }
        #endregion

        public override async Task<IEnumerable<Movie>> GetAllAsync()
        {

            var movies= await _movieRepositoryAsync.GetTableNoTracking()
                .Include(d => d.Director)
                .Include(g => g.MovieGenres).ThenInclude(mg => mg.Genre)    
                .Include(a => a.MovieActors).ThenInclude(ma => ma.Actor)    
                .Include(x => x.ShowTimes).ThenInclude(x => x.Hall)
                .Where(x => x.CurrentState == 1)
                .AsSplitQuery()
                .ToListAsync();
            return movies;
        }
        public override async Task<Movie> FindByIdAsync(Guid Id)
        {
            return await _movieRepositoryAsync.GetTableAsTracking()
                  .Include(d => d.Director)
                .Include(g => g.MovieGenres).ThenInclude(mg => mg.Genre)
                .Include(a => a.MovieActors).ThenInclude(ma => ma.Actor)
                .Include(x => x.ShowTimes).ThenInclude(x => x.Hall)
                .FirstOrDefaultAsync(x => x.CurrentState == 1 && x.Id == Id);

        }
        public IQueryable<Movie> FilterMoviePaginatedQueryable(MovieOrderingEnum orderingEnum, string search)
        {
            var queryable =  _movieRepositoryAsync.GetTableNoTracking()
               .Include(d => d.Director)
               .Include(g => g.MovieGenres).ThenInclude(mg => mg.Genre)
               .Include(a => a.MovieActors).ThenInclude(ma => ma.Actor)
               .Include(x => x.ShowTimes).ThenInclude(x => x.Hall)
               .Where(x => x.CurrentState == 1)
               .AsSplitQuery()
               .AsQueryable();
            if (search != null)
            {
                queryable.Where(x => x.TitleEn.Contains(search) || x.TitleAr.Contains(search));
            }
            switch (orderingEnum)
            {
                case MovieOrderingEnum.Title:
                    queryable = queryable.OrderBy(x => x.TitleEn);
                    break;
                case MovieOrderingEnum.ReleaseYear:
                    queryable = queryable.OrderBy(x => x.ReleaseYear);
                    break;
                case MovieOrderingEnum.Rate:
                    queryable = queryable.OrderBy(x => x.Rate);
                    break;
                case MovieOrderingEnum.Duration:
                    queryable = queryable.OrderBy(x => x.DurationInMinutes);
                    break;
            }
            return queryable; 
        }
        public async Task<bool> SaveMovieWithRelationsAsync(Movie movie, Guid userId, IFormFile poster)
        {

            // check if the movie already exists and has a poster URL   
            if (movie.Id != default && string.IsNullOrEmpty(movie.PosterURL)) return false;

            // Upload image and assign URL
            movie.PosterURL = await _fileUploadService.UploadFileAsync(poster, "Movies");

            // ✅ Save movie and get the full entity with Id
            //var createdMovie = await _movieRepositoryAsync.AddAndReturnAsync(movie, userId);

            //var createdMovie = await _movieRepositoryAsync.AddAndReturnAsync(movie, userId);
            return await _movieRepositoryAsync.SaveChangesAsync(movie, userId);

        }
        public async Task<bool> DeleteWithImageAsync(Movie entity)
        {
            if (!string.IsNullOrEmpty(entity.PosterURL))
            {
                // Delete the image file from storage
                await _fileUploadService.ArchiveFileAsync(entity.PosterURL, entity.TitleEn);
            }
            return await _movieRepositoryAsync.UpdateCurrentStateAsync(entity);
        }
        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _movieRepositoryAsync.GetTableNoTracking()
       .AnyAsync(d => d.Id == id);
        }
        public async Task<bool> IsExistByNameAsync(string NameEn, string NameAr)
        {
            return await _movieRepositoryAsync.GetTableNoTracking()
         .AnyAsync(d =>
             d.TitleEn.Trim().ToLower() == NameEn.Trim().ToLower() &&
             d.TitleAr.Trim().ToLower() == NameAr.Trim().ToLower());
        }
        public async Task<bool> IsExistByNameExcludeItselfAsync(Guid id, string NameEn, string NameAr)
        {
            return await _movieRepositoryAsync.GetTableNoTracking().AnyAsync(d =>
      d.Id != id &&
      d.TitleEn.ToLower().Trim() == NameEn.ToLower().Trim() &&
      d.TitleAr.ToLower().Trim() == NameAr.ToLower().Trim());
        }

        
        //public async Task<bool> SaveMovieWithRelationsAsync(Movie movie, Guid userId, IFormFile poster)
        //{
        //    await _movieRepositoryAsync.BeginTransactionAsync();
        //    try
        //    {
        //        // check if the movie already exists and has a poster URL   
        //        if (movie.Id != default && string.IsNullOrEmpty(movie.PosterURL)) return false;

        //        // Upload image and assign URL
        //        movie.PosterURL = await _fileUploadService.UploadFileAsync(poster, "Movies");

        //        // ✅ Save movie and get the full entity with Id
        //        //var createdMovie = await _movieRepositoryAsync.AddAndReturnAsync(movie, userId);

        //        //var createdMovie = await _movieRepositoryAsync.AddAndReturnAsync(movie, userId);
        //        var createdMovie = await _movieRepositoryAsync.SaveChangesAsync(movie, userId);

        //        await _movieRepositoryAsync.CommitAsync();
        //            return true;


        //    }
        //    catch
        //    {
        //        _movieRepositoryAsync.RollBack();
        //        return false;
        //    }


        //}

    }
}
