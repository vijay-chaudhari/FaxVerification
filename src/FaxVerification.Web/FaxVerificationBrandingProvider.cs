using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace FaxVerification.Web;

[Dependency(ReplaceServices = true)]
public class FaxVerificationBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "FaxVerification";
}
