using AutoMapper;
using FaxVerification.Records;
using System.Security.Cryptography.X509Certificates;
using Volo.Abp.AutoMapper;

namespace FaxVerification;

public class FaxVerificationApplicationAutoMapperProfile : Profile
{
    public FaxVerificationApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ImageOcr, OcrDto>();
        CreateMap<CreateUpdateImageOcrDto, ImageOcr>()
            .ForMember(x=>x.Id, opt=>opt.Ignore())
            .ForMember(x=>x.ConcurrencyStamp, opt=>opt.Ignore())
            .IgnoreFullAuditedObjectProperties().IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}
