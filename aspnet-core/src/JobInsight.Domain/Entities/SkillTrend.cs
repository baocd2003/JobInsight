using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Tracks skill trends over time (monthly snapshot)
    /// </summary>
    public class SkillTrend : CreationAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Skill ID
        /// </summary>
        public Guid SkillId { get; set; }

        /// <summary>
        /// Year of the trend data
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month of the trend data (1-12)
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Number of jobs mentioning this skill in the period
        /// </summary>
        public int JobMentions { get; set; }

        /// <summary>
        /// Average salary for jobs requiring this skill
        /// </summary>
        public decimal? AverageSalary { get; set; }

        /// <summary>
        /// Median salary for jobs requiring this skill
        /// </summary>
        public decimal? MedianSalary { get; set; }

        /// <summary>
        /// Growth rate compared to previous period (percentage)
        /// </summary>
        public decimal? GrowthRate { get; set; }

        /// <summary>
        /// Number of junior-level jobs requiring this skill
        /// </summary>
        public int JuniorJobsCount { get; set; }

        /// <summary>
        /// Number of mid-level jobs requiring this skill
        /// </summary>
        public int MidJobsCount { get; set; }

        /// <summary>
        /// Number of senior-level jobs requiring this skill
        /// </summary>
        public int SeniorJobsCount { get; set; }

        // Navigation properties
        public virtual Skill Skill { get; set; }

        public SkillTrend()
        {
        }

        public SkillTrend(Guid skillId, int year, int month)
        {
            SkillId = skillId;
            Year = year;
            Month = month;
        }
    }
}
