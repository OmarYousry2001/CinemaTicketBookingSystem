﻿namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IEmailService
    {
        Task SendEmail(string email, string receiverName, string message, string subject);
    }
}