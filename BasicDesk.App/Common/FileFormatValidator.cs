using System.Collections.Generic;
using System.Linq;

namespace BasicDesk.App.Common
{
    public class FileFormatValidator
    {
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public static bool IsValidFormat(string extension)
        {
            var isAllowedFileType = GetMimeTypes().Any(f => f.Key == $".{extension}");
            if (!isAllowedFileType)
            {
                return false;
            }

            return true;
        }
    }
}
