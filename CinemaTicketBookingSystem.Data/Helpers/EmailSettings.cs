using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Data.Helpers
{
    public class EmailSettings
    {
        public string Host { get; set; } = default!;
        public string App { get; set; } = default!;
        public int Port { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool EnableSsl { get; set; }
        public string FromEmail { get; set; } = default!;
    }
}
