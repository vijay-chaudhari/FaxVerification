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

namespace FaxVerification.Records
{
    [Authorize(FaxVerificationPermissions.Documents.Default)]
    public class OCRWorkBenchIntegration : ApplicationService, IOCRWorkBenchIntegration
    {
        public OCRWorkBenchIntegration() 
        {

        }
    }
}
