using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.RabbitMQ;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace JobInsight;

[DependsOn(
    typeof(JobInsightDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(JobInsightApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpEventBusRabbitMqModule)
    )]
public class JobInsightApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<JobInsightApplicationModule>();
        });

        context.Services.AddHttpClient();
        context.Services.AddHttpClient("AiService", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(60);
        });

        // Configure RabbitMQ for distributed events
        context.Services.Configure<AbpRabbitMqOptions>(options =>
        {
            options.Connections.Default.HostName = "localhost"; // Change to your RabbitMQ server
            options.Connections.Default.Port = 5672;
            // Optional: Add credentials if RabbitMQ requires authentication
            // options.Connections.Default.UserName = "guest";
            // options.Connections.Default.Password = "guest";
        });
        Configure<AbpRabbitMqEventBusOptions>(options =>
        {
            options.ExchangeName = "jobinsight.exchange";
        });
    }
}
