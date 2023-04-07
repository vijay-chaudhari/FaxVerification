using FaxVerification.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace FaxVerification.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FaxVerificationEntityFrameworkCoreModule),
    typeof(FaxVerificationApplicationContractsModule)
    )]
public class FaxVerificationDbMigratorModule : AbpModule
{

}
