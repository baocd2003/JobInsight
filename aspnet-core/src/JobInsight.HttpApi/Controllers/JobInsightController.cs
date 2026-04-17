using JobInsight.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace JobInsight.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class JobInsightController : AbpControllerBase
{
    protected JobInsightController()
    {
        LocalizationResource = typeof(JobInsightResource);
    }
}
