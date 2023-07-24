using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FaxVerification.TemplateRegistration
{
    public class CreateUpdateRegistrationDto : AuditedEntityDto<Guid>
    {
        public string TemplateName { get; set; }
        public string VendorNo { get; set; }
        public string FilePath { get; set; }
        public string OCRConfig { get; set; }
    }
}
