using System;
//using Volo.Abp.Events.Distributed;

namespace JobInsight.Events
{
    /// <summary>
    /// Event published when a job is created - for testing RabbitMQ
    /// </summary>
    public class JobCreatedEvent 
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
