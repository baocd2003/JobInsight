using JobInsight.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace JobInsight.Permissions;

public class JobInsightPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(JobInsightPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(JobInsightPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<JobInsightResource>(name);
    }
}
