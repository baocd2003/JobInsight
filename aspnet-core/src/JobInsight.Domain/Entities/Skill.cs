using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Represents a technical or soft skill
    /// </summary>
    public class Skill : CreationAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Display name of the skill (e.g., "React", "C#")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Normalized name for search and comparison (e.g., "REACT", "CSHARP")
        /// </summary>
        public string NormalizedName { get; set; }

        /// <summary>
        /// Category classification (e.g., "Frontend", "Backend", "DevOps", "Database", "Mobile", "Data", "Soft Skills")
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Sub-category for more granular classification (e.g., "JavaScript Framework", "Programming Language")
        /// </summary>
        public string SubCategory { get; set; }

        /// <summary>
        /// Aliases for the skill stored as JSON (e.g., ["React.js", "ReactJS"])
        /// </summary>
        public string Aliases { get; set; }

        /// <summary>
        /// Related skills stored as JSON
        /// </summary>
        public string RelatedSkills { get; set; }

        /// <summary>
        /// Total number of job mentions for this skill
        /// </summary>
        public int TotalJobMentions { get; set; }

        /// <summary>
        /// Trending score to indicate skill popularity
        /// </summary>
        public decimal TrendingScore { get; set; }

        /// <summary>
        /// Icon URL for visual representation
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// Color hex code for UI representation
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Soft delete flag
        /// </summary>
        public bool IsDeleted { get; set; }

        public Skill()
        {
        }

        public Skill(string name, string normalizedName, string category)
        {
            Name = name;
            NormalizedName = normalizedName;
            Category = category;
        }
    }
}
