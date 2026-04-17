using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JobInsight.Entities
{
    /// <summary>
    /// Standalone application user entity (separate table from identity users).
    /// Add custom properties here as needed.
    /// </summary>
    public class AppUser : FullAuditedAggregateRoot<Guid>
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; } = true;
        public string CurrentJobTitle { get; set; }  
        //public string Skills { get; set; } 
        public int? YearsOfExperience { get; set; } 
        public AppUser()
        {
        }

        public AppUser(Guid id, string userName, string email, string displayName = null)
        {
            Id = id;
            UserName = userName;
            EmailAddress = email;
            DisplayName = displayName;
            IsActive = true;
        }
    }
}
