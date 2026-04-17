using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Represents a geographic location for job listings
    /// </summary>
    public class Location : CreationAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// City name (e.g., "Ho Chi Minh City", "Hanoi")
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// District/Quarter within the city
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Country (default: Vietnam)
        /// </summary>
        public string Country { get; set; } = "Vietnam";

        /// <summary>
        /// Display name for the location
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// URL-friendly slug for the location
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Latitude coordinate
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Longitude coordinate
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Total number of jobs for this location
        /// </summary>
        public int TotalJobs { get; set; }

        /// <summary>
        /// Soft delete flag
        /// </summary>
        public bool IsDeleted { get; set; }

        public Location()
        {
        }

        public Location(string city, string displayName, string slug)
        {
            City = city;
            DisplayName = displayName;
            Slug = slug;
            Country = "Vietnam";
        }
    }
}
