using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace FaxVerification.Records
{
    public class ImageOcr : FullAuditedAggregateRoot<Guid>
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public string OCRText { get; set; }
        public string Confidence { get; set; }
        public string Output { get; set; }
        public Guid? AssignedTo { get; set; }
    }
}
