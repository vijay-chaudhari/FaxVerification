using System;
using System.Collections.Generic;
using System.Text;
using FaxVerification.Localization;
using Volo.Abp.Application.Services;

namespace FaxVerification;

/* Inherit your application services from this class.
 */
public abstract class FaxVerificationAppService : ApplicationService
{
    protected FaxVerificationAppService()
    {
        LocalizationResource = typeof(FaxVerificationResource);
    }


}
