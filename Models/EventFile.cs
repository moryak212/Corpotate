using System;

namespace Corporate.Models
{
    public class EventFile
    {
        public int Id { get; set; }

        public int CorporateEventId { get; set; }
        public CorporateEvent? CorporateEvent { get; set; }

        /// <summary>Оригинальное имя файла (как у пользователя)</summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>Имя файла на сервере (случайное)</summary>
        public string StoredFileName { get; set; } = string.Empty;

        /// <summary>Относительный путь от корня сайта, например /uploads/eventfiles/abc123.pdf</summary>
        public string FilePath { get; set; } = string.Empty;

        public string? ContentType { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
