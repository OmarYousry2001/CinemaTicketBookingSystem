using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Genres.Queries.Handler
{
    public class GenreQueryHandler : ResponseHandler,
        IRequestHandler<GetAllGenresQuery, Response<List<GetAllGenresResponse>>>,
        IRequestHandler<FindGenreByIdQuery, Response<FindGenreByIdResponse>>
    {
        #region Fields
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GenreQueryHandler(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetAllGenresResponse>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genreList = await _genreService.GetAllAsync();

            var mappedGenreList = _mapper.Map<List<GetAllGenresResponse>>(genreList);

            return Success(mappedGenreList);
        }
        public async Task<Response<FindGenreByIdResponse>> Handle(FindGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await _genreService.FindByIdAsync(request.Id);
            if (genre == null) return NotFound<FindGenreByIdResponse>();

            var mappedGenre = _mapper.Map<FindGenreByIdResponse>(genre);
            return Success<FindGenreByIdResponse>(mappedGenre);
        } 
        #endregion
    }
}
