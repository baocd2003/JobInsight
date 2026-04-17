using JobInsight.Localization;
using Volo.Abp.AspNetCore.Components;

namespace JobInsight.Blazor.WebApp.Tiered;

public abstract class JobInsightComponentBase : AbpComponentBase
{
    protected JobInsightComponentBase()
    {
        LocalizationResource = typeof(JobInsightResource);
    }
}
