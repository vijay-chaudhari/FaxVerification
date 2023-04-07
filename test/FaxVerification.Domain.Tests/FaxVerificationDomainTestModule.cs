using FaxVerification.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace FaxVerification;

[DependsOn(
    typeof(FaxVerificationEntityFrameworkCoreTestModule)
    )]
public class FaxVerificationDomainTestModule : AbpModule
{

}
