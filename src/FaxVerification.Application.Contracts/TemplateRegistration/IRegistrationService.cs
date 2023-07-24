using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaxVerification.TemplateRegistration
{
    public interface IRegistrationService : ICrudAppService< 
            RegistrationDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateRegistrationDto>
    {
    }
}
