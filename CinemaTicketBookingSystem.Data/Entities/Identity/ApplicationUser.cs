using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }

    }
}
