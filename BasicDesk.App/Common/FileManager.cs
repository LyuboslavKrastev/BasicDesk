//using System.Threading.Tasks;

//public class FileManager
//{
//    public async Task<IActionResult> DownloadAttachmentsAsync(string fileName, string filePath, string requestId)
//    {
//        try
//        {
//            if (fileName == null)
//                return Content("filename not present");

//            var memory = new MemoryStream();
//            using (var stream = new FileStream(filePath, FileMode.Open))
//            {
//                await stream.CopyToAsync(memory);
//            }
//            memory.Position = 0;
//            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
//        }
//        catch (IOException)
//        {
//            this.TempData.Put("__Message", new MessageModel()
//            {
//                Type = MessageType.Danger,
//                Message = "File not available"
//            });

//            return this.RedirectToAction("Details", new { id = requestId });
//        }
//    }

//    public async Task CreateAttachmentsAsync(RequestCreationBindingModel model, string requestId)
//    {
//        string path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Requests", model.Attachment.FileName);
//        var fileStream = new FileStream(path, FileMode.Create);
//        using (fileStream)
//        {
//            await model.Attachment.CopyToAsync(fileStream);
//        }
//        this.dbContext.RequestAttachments.Add(new RequestAttachment
//        {
//            FileName = model.Attachment.FileName,
//            PathToFile = path,
//            RequestId = request.Id
//        });
//    }

//    private string GetContentType(string path)
//    {
//        var types = FileFormatValidator.GetMimeTypes();
//        var ext = Path.GetExtension(path).ToLowerInvariant();
//        return types[ext];
//    }

//}

