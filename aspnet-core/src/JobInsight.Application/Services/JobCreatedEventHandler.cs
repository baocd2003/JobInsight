using System.Threading.Tasks;
using JobInsight.Events;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace JobInsight.Services
{
    /// <summary>
    /// Event handler for JobCreatedEvent - for testing RabbitMQ
    /// </summary>
    public class JobCreatedEventHandler : IDistributedEventHandler<JobCreatedEvent>, ITransientDependency
    {
        public async Task HandleEventAsync(JobCreatedEvent eventData)
        {
            // Simple test handler - just logs/processes the event
            await Task.Run(() =>
            {
                System.Console.WriteLine($"[JobCreatedEventHandler] Event received!");
                System.Console.WriteLine($"  Job ID: {eventData.JobId}");
                System.Console.WriteLine($"  Job Title: {eventData.JobTitle}");
                System.Console.WriteLine($"  Company ID: {eventData.CompanyId}");
                System.Console.WriteLine($"  Created At: {eventData.CreatedAt}");
            });
        }
    }
}
