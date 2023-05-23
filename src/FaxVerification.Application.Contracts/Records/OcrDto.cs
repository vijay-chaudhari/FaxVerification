using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace FaxVerification.Records
{
    public class OcrDto : AuditedEntityDto<Guid>
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public string OCRText { get; set; }
        public string Confidence { get; set; }
        public string Output { get; set; }

        public Guid? AssignedTo { get; set; }
        //public string FormConfiguration { get; set; }
    }
}
