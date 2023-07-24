using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace FaxVerification.TemplateRegistration
{
    public class Registration : FullAuditedAggregateRoot<Guid>
    {
        public string TemplateName { get; set; }
        public string VendorNo { get; set; }
        public string FilePath { get; set; }
        public string OCRConfig { get; set; }
    }
}
