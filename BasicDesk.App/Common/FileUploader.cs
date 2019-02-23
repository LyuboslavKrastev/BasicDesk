using BasicDesk.App.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.App.Common
{
    public class FileUploader : IFileUploader
    {
        public readonly string directory = Directory.GetCurrentDirectory() +  "/Files";

        public async Task<string> CreateAttachmentAsync(string subject, IEnumerable<IFormFile> attachments, string folder)
        {
            string destination = directory + $"/{folder}/" + subject;

            foreach (var attachment in attachments)
            {
                string extension = attachment.FileName.Split('.').Last();
                bool isAllowedFileFormat = FileFormatValidator.IsValidFormat(extension);

                if (!isAllowedFileFormat)
                {
                    throw new InvalidDataException($"{attachment.FileName} failed to upload because the file format is forbidden.");
                }

                Directory.CreateDirectory(destination);
                string path = Path.Combine(destination, attachment.FileName);

                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    await attachment.CopyToAsync(fileStream);
                }
            }

            return destination;
        }
    }
}
