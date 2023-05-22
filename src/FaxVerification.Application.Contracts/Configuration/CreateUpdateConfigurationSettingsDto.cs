using System;
using System.Collections.Generic;
using System.Text;

namespace FaxVerification.Configuration
{
    public class CreateUpdateConfigurationSettingsDto
    {
        public string TemplateName { get; set; }
        public List<FieldConfigDto> Fields { get; set; }
    }
}
