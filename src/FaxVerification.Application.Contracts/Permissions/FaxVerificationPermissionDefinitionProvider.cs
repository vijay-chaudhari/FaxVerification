using FaxVerification.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FaxVerification.Permissions;

public class FaxVerificationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FaxVerificationPermissions.GroupName);
        //Define your own permissions here. Example:
        var permission = myGroup.AddPermission(FaxVerificationPermissions.Documents.Default);
        permission.AddChild(FaxVerificationPermissions.Documents.Create);
        permission.AddChild(FaxVerificationPermissions.Documents.Edit);
        permission.AddChild(FaxVerificationPermissions.Documents.Delete);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FaxVerificationResource>(name);
    }
}
