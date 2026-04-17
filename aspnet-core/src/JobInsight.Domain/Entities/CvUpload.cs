using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Represents a CV/resume uploaded by a user
    /// </summary>
    public class CvUpload : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// ID of the user who uploaded the CV
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Original file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File storage path or blob URL
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// File extension (e.g., "pdf", "docx")
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long FileSizeBytes { get; set; }

        /// <summary>
        /// Raw text extracted from the CV file
        /// </summary>
        public string RawText { get; set; }

        /// <summary>
        /// Job title parsed by AI from the CV
        /// </summary>
        public string ParsedJobTitle { get; set; }

        /// <summary>
        /// Years of experience parsed by AI from the CV
        /// </summary>
        public int? ParsedYearsOfExperience { get; set; }

        /// <summary>
        /// Processing status (e.g., "Pending", "Analyzing", "Done", "Failed")
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Error message if processing failed
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// When the file was uploaded
        /// </summary>
        public DateTime UploadedAt { get; set; }

        /// <summary>
        /// When AI finished processing the CV
        /// </summary>
        public DateTime? ProcessedAt { get; set; }

        // Navigation properties
        public virtual AppUser User { get; set; }

        public CvUpload()
        {
        }

        public CvUpload(Guid userId, string fileName, string fileUrl, string fileType)
        {
            UserId = userId;
            FileName = fileName;
            FileUrl = fileUrl;
            FileType = fileType;
            UploadedAt = DateTime.UtcNow;
            Status = "Pending";
        }
    }
}
