using BasicDesk.Common.Constants.Validation;
using BasicDesk.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Requests
{
    public class ReplyAttachment : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MinLength(RequestAttachmentConstants.FileNameMinLength)]
        [MaxLength(RequestAttachmentConstants.FileNameMaxLength)]
        public string FileName { get; set; }

        [Required]
        [MinLength(RequestAttachmentConstants.PathToFileMinLength)]
        [MaxLength(RequestAttachmentConstants.PathToFileMaxLength)]
        public string PathToFile { get; set; }

        public int ReplyId { get; set; }
        public RequestReply Reply { get; set; }
    }
}
