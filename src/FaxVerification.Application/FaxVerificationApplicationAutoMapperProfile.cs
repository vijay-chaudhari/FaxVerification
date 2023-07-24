﻿using AutoMapper;
using FaxVerification.Configuration;
using FaxVerification.Records;
using FaxVerification.TemplateRegistration;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using Volo.Abp.AutoMapper;
using Volo.Abp.Guids;

namespace FaxVerification;

public class FaxVerificationApplicationAutoMapperProfile : Profile
{
    public FaxVerificationApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ImageOcr, OcrDto>().ForMember(a=>a.CurrentUserID, opt => opt.Ignore());
        CreateMap<CreateUpdateImageOcrDto, ImageOcr>()
            .ForMember(x=>x.Id, opt=>opt.Ignore())
            .ForMember(x=>x.ConcurrencyStamp, opt=>opt.Ignore())//.ForMember(x=>x.)
            .IgnoreFullAuditedObjectProperties().IgnoreAllPropertiesWithAnInaccessibleSetter();


        CreateMap<ConfigurationSettings, ConfigurationSettingsDto>().ReverseMap();
        CreateMap<CreateUpdateConfigurationSettingsDto, ConfigurationSettings>()
            .ForMember(destinationMember: d => d.Fields, opt => opt.MapFrom(s => s.Fields))
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore())
            .IgnoreFullAuditedObjectProperties().IgnoreAllPropertiesWithAnInaccessibleSetter();

        CreateMap<FieldConfig, FieldConfigDto>()
            .DisableCtorValidation().ReverseMap();


        CreateMap<Registration, RegistrationDto>()
            .DisableCtorValidation().ReverseMap();

        CreateMap<CreateUpdateRegistrationDto, Registration>()
       .ForMember(x => x.Id, opt => opt.Ignore())
       .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore())
       .IgnoreFullAuditedObjectProperties().IgnoreAllPropertiesWithAnInaccessibleSetter();


        //CreateMap<FieldConfigDto, FieldConfig>().DisableCtorValidation()
        //    .ForMember(x => x.Id, opt => new SimpleGuidGenerator().Create())
        //    .ForMember(x=>x.TemplateId, opt=>opt.Ignore())
        //    .ForMember(x=>x.ConfigurationSetting, opt=>opt.Ignore());
    }
}
