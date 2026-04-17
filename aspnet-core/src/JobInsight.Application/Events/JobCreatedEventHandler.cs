using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace JobInsight.Events
{
    /// <summary>
    /// Simple handler for RabbitMQ test
    /// </summary>
    public class JobCreatedEventHandler : IDistributedEventHandler<JobCreatedEventData>, ITransientDependency
    {
        public async Task HandleEventAsync(JobCreatedEventData eventData)
        {
            await Task.Run(() =>
            {
                System.Console.WriteLine($"✓ JobCreatedEvent received from RabbitMQ!");
                System.Console.WriteLine($"  Job: {eventData.JobTitle} (ID: {eventData.JobId})");
                System.Console.WriteLine($"  Company ID: {eventData.CompanyId}");
            });
        }
    }
}
