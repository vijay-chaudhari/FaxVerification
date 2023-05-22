using FaxVerification.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FaxVerification.Configuration
{
    [Authorize(FaxVerificationPermissions.Documents.Default)]
    public class ConfigAppService : CrudAppService<
        ConfigurationSettings, //The Book entity
        ConfigurationSettingsDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto,
        CreateUpdateConfigurationSettingsDto>,//Used for paging/sorting
        IConfigAppService
    {
        public ConfigAppService(IRepository<ConfigurationSettings, Guid> repository) : base(repository)
        {
        }
    }
}
