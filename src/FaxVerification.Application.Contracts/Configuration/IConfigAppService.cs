using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaxVerification.Configuration
{
    public interface IConfigAppService :  ICrudAppService<ConfigurationSettingsDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateConfigurationSettingsDto>
    {
    }
}
        