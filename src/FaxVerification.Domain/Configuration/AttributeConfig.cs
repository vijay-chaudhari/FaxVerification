using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace FaxVerification.Configuration
{
    public class AttributeConfig : FullAuditedAggregateRoot<Guid>
    {
        public string Attribute_1 { get; set; }
        public string Attribute_2 { get; set; }
        public string Attribute_3 { get; set; }
        public string Attribute_4 { get; set; }
        public string Attribute_5 { get; set; }
        public string Attribute_6 { get; set; }
        public string Attribute_7 { get; set; }
        public string Attribute_8 { get; set; }



    }
}
