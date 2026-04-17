using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Represents a job benefit/perk
    /// </summary>
    public class Benefit : CreationAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Display name of the benefit (e.g., "13th Month Salary", "Health Insurance")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Normalized name for search and comparison
        /// </summary>
        public string NormalizedName { get; set; }

        /// <summary>
        /// Category classification (e.g., "Insurance", "Leave", "Perks", "Learning", "Equipment", "Compensation")
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Icon name/identifier for UI representation
        /// </summary>
        public string IconName { get; set; }

        /// <summary>
        /// Description of the benefit
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Total number of companies offering this benefit
        /// </summary>
        public int TotalCompaniesOffering { get; set; }

        public Benefit()
        {
        }

        public Benefit(string name, string normalizedName, string category)
        {
            Name = name;
            NormalizedName = normalizedName;
            Category = category;
        }
    }
}
