using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Tracks job crawling/scraping history from source websites
    /// </summary>
    public class CrawlHistory : CreationAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Source website (e.g., "ITviec", "TopCV", "VietnamWorks")
        /// </summary>
        public string SourceWebsite { get; set; }

        /// <summary>
        /// When the crawl session started
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// When the crawl session ended
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Total number of jobs found during crawl
        /// </summary>
        public int JobsFound { get; set; }

        /// <summary>
        /// Number of new jobs created
        /// </summary>
        public int JobsCreated { get; set; }

        /// <summary>
        /// Number of existing jobs updated
        /// </summary>
        public int JobsUpdated { get; set; }

        /// <summary>
        /// Number of jobs skipped
        /// </summary>
        public int JobsSkipped { get; set; }

        /// <summary>
        /// Number of errors encountered
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// JSON details of errors that occurred
        /// </summary>
        public string ErrorDetails { get; set; }

        /// <summary>
        /// Status of the crawl (e.g., "Running", "Completed", "Failed", "PartialSuccess")
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Number of pages processed
        /// </summary>
        public int PagesProcessed { get; set; }

        /// <summary>
        /// Duration of the crawl in seconds
        /// </summary>
        public int? DurationSeconds { get; set; }

        public CrawlHistory()
        {
        }

        public CrawlHistory(string sourceWebsite)
        {
            SourceWebsite = sourceWebsite;
            StartTime = DateTime.UtcNow;
            Status = "Running";
        }
    }
}
