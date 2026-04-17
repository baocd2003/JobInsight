using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Daily snapshot of job market metrics
    /// </summary>
    public class MarketSnapshot : CreationAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Date of the snapshot
        /// </summary>
        public DateTime SnapshotDate { get; set; }

        /// <summary>
        /// Total number of active jobs on this date
        /// </summary>
        public int TotalActiveJobs { get; set; }

        /// <summary>
        /// Number of new jobs posted today
        /// </summary>
        public int NewJobsToday { get; set; }

        /// <summary>
        /// Number of jobs expired today
        /// </summary>
        public int ExpiredJobsToday { get; set; }

        /// <summary>
        /// Average salary in USD
        /// </summary>
        public decimal? AverageSalaryUSD { get; set; }

        /// <summary>
        /// Median salary in USD
        /// </summary>
        public decimal? MedianSalaryUSD { get; set; }

        /// <summary>
        /// JSON array of top skills on this date
        /// </summary>
        public string TopSkills { get; set; }

        /// <summary>
        /// JSON array of top companies on this date
        /// </summary>
        public string TopCompanies { get; set; }

        /// <summary>
        /// JSON array of top locations on this date
        /// </summary>
        public string TopLocations { get; set; }

        /// <summary>
        /// Percentage of remote jobs
        /// </summary>
        public decimal? RemoteJobsPercentage { get; set; }

        public MarketSnapshot()
        {
        }

        public MarketSnapshot(DateTime snapshotDate)
        {
            SnapshotDate = snapshotDate.Date;
        }
    }
}
