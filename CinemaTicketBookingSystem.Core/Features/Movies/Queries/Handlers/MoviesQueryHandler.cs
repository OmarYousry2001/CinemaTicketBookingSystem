using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Wrappers;

namespace CinemaTicketBookingSystem.Core.Features.Movies.Queries.Handlers
{
    public class MoviesQueryHandler : ResponseHandler,
        IRequestHandler<GetAllMoviesQuery, Response<List<GetAllMoviesResponse>>>,
        IRequestHandler<FindMovieByIdQuery, Response<FindMovieByIdResponse>>,
        IRequestHandler<GetMoviesPaginatedListQuery, PaginatedResult<GetMoviesPaginatedListResponse>>
    {
        #region Fields
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors
        public MoviesQueryHandler(IMovieService movieService, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _movieService = movieService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetAllMoviesResponse>>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var moviesList = await _movieService.GetAllAsync();

            var mappedMoviesList = _mapper.Map<List<GetAllMoviesResponse>>(moviesList);

            return Success(mappedMoviesList);
        }

        public async Task<Response<FindMovieByIdResponse>> Handle(FindMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieService.FindByIdAsync(request.Id);
            if (movie == null)
                return NotFound<FindMovieByIdResponse>();

            var mappedMovie = _mapper.Map<FindMovieByIdResponse>(movie);

            return Success(mappedMovie);
        }
        public async Task<PaginatedResult<GetMoviesPaginatedListResponse>> Handle(GetMoviesPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var FilterQuery = _movieService.FilterMoviePaginatedQueryable( request.MovieOrdering , request.Search);
            var PaginatedList = await _mapper.ProjectTo<GetMoviesPaginatedListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;

        }
        #endregion
    }
}