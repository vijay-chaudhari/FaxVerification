using System;
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

        public FieldConfig(Guid id) : base(id) { }
        
    }
}
