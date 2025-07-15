
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.DirectorMapping
{
    public partial class DirectorProfile
    {
        public void GetAllDirectorsMapping()
        {
            CreateMap<Director, GetAllDirectorsResponse>()
                 .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(des => des.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
                .ForMember(des => des.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(des => des.Bio, opt => opt.MapFrom(src => src.Bio));
        }
    }
}
