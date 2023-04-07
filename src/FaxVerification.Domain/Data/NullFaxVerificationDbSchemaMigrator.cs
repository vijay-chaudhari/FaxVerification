using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace FaxVerification.Data;

/* This is used if database provider does't define
 * IFaxVerificationDbSchemaMigrator implementation.
 */
public class NullFaxVerificationDbSchemaMigrator : IFaxVerificationDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
