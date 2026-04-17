using JobInsight.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JobInsight.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class JobInsightPageModel : AbpPageModel
{
    protected JobInsightPageModel()
    {
        LocalizationResourceType = typeof(JobInsightResource);
    }
}
