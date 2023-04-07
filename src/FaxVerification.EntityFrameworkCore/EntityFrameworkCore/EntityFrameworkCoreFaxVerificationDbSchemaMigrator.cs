using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FaxVerification.Data;
using Volo.Abp.DependencyInjection;

namespace FaxVerification.EntityFrameworkCore;

public class EntityFrameworkCoreFaxVerificationDbSchemaMigrator
    : IFaxVerificationDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreFaxVerificationDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the FaxVerificationDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<FaxVerificationDbContext>()
            .Database
            .MigrateAsync();
    }
}
