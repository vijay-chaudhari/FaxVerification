using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaxVerification.Configuration
{
    public class ConfigurationSettings
    {
        public int TemplateId { get; set; }
        public List<FieldsModel> Fields { get; set; }
        public List<FieldsValueModel> FieldValue { get; set; }
    }


    public class FieldsModel
    {
        public string FieldName { get; set; }
        public string RegExpression { get; set; }
        public string CoOrdinates { get; set; }
    }

    public class FieldsValueModel
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
        public string CoOrdinates { get; set; }
    }



}
