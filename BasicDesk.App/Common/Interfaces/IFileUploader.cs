using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BasicDesk.App.Common.Interfaces
{
    public interface IFileUploader
    {
        Task<string> CreateAttachmentAsync(string subject, IEnumerable<IFormFile> attachments, string folder);
    }
}