using FaxVerification.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FaxVerification.TemplateRegistration
{
    public class RegistrationService : CrudAppService<
        Registration, 
        RegistrationDto,
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateUpdateRegistrationDto>,
        IRegistrationService
    {
        public RegistrationService(IRepository<Registration, Guid> repository) : base(repository)
        {
        }
    }
}
