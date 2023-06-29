using FaxVerification.Records;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace FaxVerification;

[DependsOn(
    typeof(FaxVerificationDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(FaxVerificationApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class FaxVerificationApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<FaxVerificationApplicationModule>(validate: true);
        });

        // Register your custom application service
        context.Services.AddTransient<OCRWorkBenchIntegration>();
    }
}
