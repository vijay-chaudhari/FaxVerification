using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace FaxVerification.Configuration
{
    public class FieldConfig : AuditedAggregateRoot<Guid>
    {
        public Guid TemplateId { get; set; }
        public string FieldName { get; set; }
        public string RegExpression { get; set; }
        public string CoOrdinates { get; set; }

        public ConfigurationSettings ConfigurationSetting { get; set; } //Navigation Property

        public FieldConfig()
        {
            Id = Guid.NewGuid();
        }

    }
}
