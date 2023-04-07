using System.Threading.Tasks;

namespace FaxVerification.Data;

public interface IFaxVerificationDbSchemaMigrator
{
    Task MigrateAsync();
}
