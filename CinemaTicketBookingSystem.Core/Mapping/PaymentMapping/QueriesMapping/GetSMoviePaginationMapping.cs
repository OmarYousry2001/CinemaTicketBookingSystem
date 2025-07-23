//using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results;
//using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results.Shared;
//using CinemaTicketBookingSystem.Data.Entities;

//namespace CinemaTicketBookingSystem.Core.Mapping.MovieMapping
//{
//    public partial class MovieProfile
//    {
//        public void GetSMoviePaginationMapping()
//    {
//        CreateMap<Movie, GetMoviesPaginatedListResponse>()
//        .ForMember(des => des.Id, options => options.MapFrom(src => src.Id))
//        .ForMember(des => des.Title, options => options.MapFrom(src => src.Localize(src.TitleAr, src.TitleEn)))
//        .ForMember(des => des.Description, options => options.MapFrom(src => src.Localize(src.DescriptionAr, src.DescriptionEn)))
//        .ForMember(des => des.PosterURL, options => options.MapFrom(src => src.PosterURL))
//        .ForMember(des => des.DurationInMinutes, options => options.MapFrom(src => src.DurationInMinutes))
//        .ForMember(des => des.Rate, options => options.MapFrom(src => src.Rate))
//        .ForMember(des => des.IsActive, options => options.MapFrom(src => src.IsActive))
//        .ForMember(des => des.Director, options => options.MapFrom(src => src.Director))
//        .ForMember(des => des.Genres, options => options.MapFrom(src => src.MovieGenres))
//        .ForMember(des => des.Actors, options => options.MapFrom(src => src.MovieActors))
//        .ForMember(des => des.ShowTimes, options => options.MapFrom(src => src.ShowTimes));



//        CreateMap<Director, DirectorInMovieResponse>()
//            .ForMember(des => des.Id, options => options.MapFrom(src => src.Id))
//            .ForMember(des => des.Name, options => options.MapFrom(src => src.FirstName + " " + src.LastName));


//        CreateMap<MovieGenre, GenreInMovieResponse>()
//        .ForMember(des => des.Id, options => options.MapFrom(src => src.Genre.Id))
//        .ForMember(des => des.Name, options => options.MapFrom(src => src.Genre.Localize(src.Genre.NameAr, src.Genre.NameEn)));





//        CreateMap<MovieActor, ActorInMovieResponse>()
//.ForMember(des => des.Id, options => options.MapFrom(src => src.Actor.Id))
//.ForMember(des => des.Name, options => options.MapFrom(src => src.Actor.FirstName + " " + src.Actor.LastName));

//        CreateMap<ShowTime, ShowTimeInMovieResponse>()
//.ForMember(des => des.Id, options => options.MapFrom(src => src.Id))
//.ForMember(des => des.StartTime, options => options.MapFrom(src => src.StartTime))
//.ForMember(des => des.EndTime, options => options.MapFrom(src => src.EndTime))
//.ForMember(des => des.Day, options => options.MapFrom(src => src.Day))
//.ForMember(des => des.HallName, options => options.MapFrom(src => src.Hall.Localize(src.Hall.NameAr, src.Hall.NameEn)));

//    }
//    }

//}
