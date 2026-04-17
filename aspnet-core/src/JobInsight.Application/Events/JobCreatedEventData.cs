using System;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;
namespace JobInsight.Events
{
    /// <summary>
    /// Simple event DTO for testing RabbitMQ
    /// </summary>
    public class JobCreatedEventData
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    [EventName("jobinsight.test")]
    public class TestMessageEto
    {
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
