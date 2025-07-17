using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketBookingSystem.Service.Abstracts.CMS
{
    public interface IFileUploadService
    {
        public  Task ArchiveFileAsync(string relativePath, string entityName);
        Task<byte[]> GetFileBytesAsync(IFormFile file);
        Task<byte[]> GetFileBytesAsync(string base64String);
        Task<string> UploadFileAsync(IFormFile file , string featureFolder , string oldFileName = null);
        Task<string> UploadFileAsync(byte[] fileBytes, string folderName , string? oldFileName = null);
        bool IsValidFile(IFormFile file );
        bool IsValidFile(string base64File, string fileName);
        (bool isValid, string errorMessage) ValidateFile(IFormFile file);
        (bool isValid, string errorMessage) ValidateFile(string base64String);
    }
}
