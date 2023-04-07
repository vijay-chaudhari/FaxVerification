using Volo.Abp.Modularity;

namespace FaxVerification;

[DependsOn(
    typeof(FaxVerificationApplicationModule),
    typeof(FaxVerificationDomainTestModule)
    )]
public class FaxVerificationApplicationTestModule : AbpModule
{

}
