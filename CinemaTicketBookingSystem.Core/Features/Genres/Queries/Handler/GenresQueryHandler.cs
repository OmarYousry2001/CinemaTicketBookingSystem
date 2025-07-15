using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Results;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;


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
        public async Task<Response<List<GetAllGenresResponse>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var entityList = await _genreService.GetAllAsync();

            var dtoList = _mapper.Map<List<GetAllGenresResponse>>(entityList);

            return Success(dtoList);
        }

        public async Task<Response<FindGenreByIdResponse>> Handle(FindGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _genreService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<FindGenreByIdResponse>();

            var dto = _mapper.Map<FindGenreByIdResponse>(entity);
            return Success<FindGenreByIdResponse>(dto);
        }
    }
}
