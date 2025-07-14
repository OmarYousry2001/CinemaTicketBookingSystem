//using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
//using CinemaTicketBookingSystem.Data.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CinemaTicketBookingSystem.Core.Mapping.ActorMapping.Commands
//{
//    public partial class ActorProfile
//    {
//        public void EditActorMapping()
//        {
//            CreateMap<AddActorCommand, Actor>()
//            .ForPath(dist => dist.FirstName, opt => opt.MapFrom(src => src.FirstName))
//            .ForPath(dist => dist.LastName, opt => opt.MapFrom(src => src.LastName))
//            .ForPath(dist => dist.ImageURL, opt => opt.MapFrom(src => src.Image))
//            .ForPath(dist => dist.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
//            .ForPath(dist => dist.Bio, opt => opt.MapFrom(src => src.Bio));
//        }
//    }
//}
