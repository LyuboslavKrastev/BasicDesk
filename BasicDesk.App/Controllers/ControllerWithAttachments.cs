using BasicDesk.App.Common;
using BasicDesk.Data.Models.Interfaces;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.App.Controllers
{
    public abstract class ControllerWithAttachments<T> : Controller
        where T : class, IEntity, IAttachment
    {
        private readonly AttachmentService<T> attachmentService;

        protected ControllerWithAttachments(AttachmentService<T> attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        public async Task<IActionResult> Download(string fileName, string filePath, string attachmentId)
        {
            //if (fileName == null)
            //    return Content("filename not present");

            var memory = new MemoryStream();
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
            }
            catch(IOException)
            {
                //Delete the attachment from the database attachments table, if it no longer exists in the attachments directory
                int id = int.Parse(attachmentId);
                T entity = this.attachmentService.ById(id).First();
                await this.attachmentService.Delete(entity.Id);
                return NotFound();
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }

        private string GetContentType(string path)
        {
            var types = FileFormatValidator.GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
    }
}
