﻿using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicketBookingSystem.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public int CurrentState { get; set; } = 1;
        public DateTime LastLoginDate { get; set; }

    }
}
