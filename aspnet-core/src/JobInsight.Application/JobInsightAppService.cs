using System;
using System.Collections.Generic;
using System.Text;
using JobInsight.Localization;
using Volo.Abp.Application.Services;

namespace JobInsight;

/* Inherit your application services from this class.
 */
public abstract class JobInsightAppService : ApplicationService
{
    protected JobInsightAppService()
    {
        LocalizationResource = typeof(JobInsightResource);
    }
}
