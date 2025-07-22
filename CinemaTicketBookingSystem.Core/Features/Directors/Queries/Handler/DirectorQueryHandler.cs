using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
namespace CinemaTicketBookingSystem.Core.Features.Directors.Queries.Handler
{
    public class DirectorQueryHandler : ResponseHandler,
        IRequestHandler<GetAllDirectorsQuery, Response<List<GetAllDirectorsResponse>>>,
        IRequestHandler<FindDirectorByIdQuery, Response<FindDirectorByIdResponse>>
    {
        #region Fields
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public DirectorQueryHandler(IDirectorService directorService, IMapper mapper)
        {
            _directorService = directorService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetAllDirectorsResponse>>> Handle(GetAllDirectorsQuery request, CancellationToken cancellationToken)
        {
            var directorList = await _directorService.GetAllAsync();

            var mappedDirectorList = _mapper.Map<List<GetAllDirectorsResponse>>(directorList);

            return Success(mappedDirectorList);
        }

        public async Task<Response<FindDirectorByIdResponse>> Handle(FindDirectorByIdQuery request, CancellationToken cancellationToken)
        {
            var director = await _directorService.FindByIdAsync(request.Id);
            if (director == null) return NotFound<FindDirectorByIdResponse>();

            var mappedDirector = _mapper.Map<FindDirectorByIdResponse>(director);
            return Success<FindDirectorByIdResponse>(mappedDirector);

        } 
        #endregion
    }
}
