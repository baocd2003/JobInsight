using Microsoft.Extensions.Localization;
using JobInsight.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace JobInsight.Blazor.WebApp.Tiered.Client;

[Dependency(ReplaceServices = true)]
public class JobInsightBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<JobInsightResource> _localizer;

    public JobInsightBrandingProvider(IStringLocalizer<JobInsightResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
