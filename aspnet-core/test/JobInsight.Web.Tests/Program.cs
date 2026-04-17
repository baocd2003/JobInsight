using Microsoft.AspNetCore.Builder;
using JobInsight;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("JobInsight.Web.csproj");
await builder.RunAbpModuleAsync<JobInsightWebTestModule>(applicationName: "JobInsight.Web" );

public partial class Program
{
}
