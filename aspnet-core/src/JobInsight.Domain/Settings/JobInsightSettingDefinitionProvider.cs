using Volo.Abp.Settings;

namespace JobInsight.Settings;

public class JobInsightSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(JobInsightSettings.MySetting1));
    }
}
