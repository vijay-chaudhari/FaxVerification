using System.Threading.Tasks;
using FaxVerification.Localization;
using FaxVerification.MultiTenancy;
using FaxVerification.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace FaxVerification.Web.Menus;

public class FaxVerificationMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<FaxVerificationResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                FaxVerificationMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Documents",
                l["Menu:Documents"],
                icon: "fa fa-file-text-o"
            ).AddItem(
                new ApplicationMenuItem(
                "OCR.Validate",
                l["Menu:Validate"],
                url: "/OCR"
                ).RequirePermissions(FaxVerificationPermissions.Documents.Default)
            ).AddItem(
                new ApplicationMenuItem(
                "Configuration.Validate",
                l["Configuration"],
                url: "/Configuration"
                ).RequirePermissions(FaxVerificationPermissions.Documents.Default)
            ).AddItem(
                new ApplicationMenuItem(
                "Template_Registration.Validate",
                l["Registration"],
                url: "/Template_Registration"
                ).RequirePermissions(FaxVerificationPermissions.Documents.Default)
            )
         );
    }
}
