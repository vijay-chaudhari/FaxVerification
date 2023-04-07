using FaxVerification.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FaxVerification.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class FaxVerificationPageModel : AbpPageModel
{
    protected FaxVerificationPageModel()
    {
        LocalizationResourceType = typeof(FaxVerificationResource);
    }
}
