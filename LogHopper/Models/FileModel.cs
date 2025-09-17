using System;

namespace LogHopper.Models
{
    public enum FileType
    {
        Unknown,
        Text,
        Json,
        Pdf
    }

    public class FileModel
    {
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public FileType FileType { get; set; } = FileType.Unknown;
        public string? ContentPreview { get; set; } = null;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
