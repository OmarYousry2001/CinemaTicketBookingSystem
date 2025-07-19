

using CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.ShowTimeMapping
{
    public partial class ShowTimeProfile
    {
        public void CreateShowTimeMapping()
        {
            CreateMap<AddShowTimeCommand, ShowTime>();
        }
    }
}
