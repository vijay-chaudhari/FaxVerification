using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FaxVerification.Configuration
{
    [Serializable]
    public class ConfigurationSettingsDto : AuditedEntityDto<Guid>
    {
        public string TemplateName { get; set; }
        public List<FieldConfigDto> Fields { get; set; }
    }

    [Serializable]
    public class FieldConfigDto
    {
        public Guid TemplateId { get; set; }
        public string FieldName { get; set; }
        public string RegExpression { get; set; }
        public string CoOrdinates { get; set; }
    }
}
