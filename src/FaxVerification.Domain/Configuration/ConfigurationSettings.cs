using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace FaxVerification.Configuration
{
    public class ConfigurationSettings : FullAuditedAggregateRoot<Guid>
    {
        public string TemplateName { get; set; }
        public List<FieldConfig> Fields { get; set; }

        public ConfigurationSettings()
        {
            Fields = new List<FieldConfig>();
        }
        
    }
}
