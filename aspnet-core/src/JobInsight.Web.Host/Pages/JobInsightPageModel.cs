using JobInsight.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JobInsight.Web.Pages;

public abstract class JobInsightPageModel : AbpPageModel
{
    protected JobInsightPageModel()
    {
        LocalizationResourceType = typeof(JobInsightResource);
    }
}
