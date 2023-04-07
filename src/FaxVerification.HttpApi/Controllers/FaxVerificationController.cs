using FaxVerification.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace FaxVerification.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class FaxVerificationController : AbpControllerBase
{
    protected FaxVerificationController()
    {
        LocalizationResource = typeof(FaxVerificationResource);
    }
}
