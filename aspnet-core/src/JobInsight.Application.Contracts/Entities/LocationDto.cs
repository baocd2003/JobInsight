using System;
using Volo.Abp.Application.Dtos;

namespace JobInsight.Entities.Dtos
{
    /// <summary>
    /// DTO for creating/updating a location
    /// </summary>
    public class CreateUpdateLocationDto
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; } = "Vietnam";
        public string DisplayName { get; set; }
        public string Slug { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

    /// <summary>
    /// DTO for reading location data
    /// </summary>
    public class LocationDto : EntityDto<Guid>
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string DisplayName { get; set; }
        public string Slug { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int TotalJobs { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
