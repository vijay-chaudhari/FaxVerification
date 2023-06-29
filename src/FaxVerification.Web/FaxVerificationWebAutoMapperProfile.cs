using AutoMapper;
using FaxVerification.Configuration;
using FaxVerification.Records;
using Volo.Abp.AutoMapper;
using static FaxVerification.Web.Pages.OCR.EditModalModel;

namespace FaxVerification.Web;

public class FaxVerificationWebAutoMapperProfile : Profile
{
    public FaxVerificationWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<OcrDto, Pages.OCR.EditModalModel.EditDetailsViewModel>()
            .ForMember(x=>x.PersonDetails, opt=>opt.Ignore())
            .ForMember(x=>x.FormConfiguration, opt=>opt.Ignore())
            .ForMember(x=>x.CurrentUserID, opt=>opt.Ignore())
            .ForMember(x=>x.FilePath, opt=>opt.Ignore());

        CreateMap<ConfigurationSettings, ConfigurationSettViewModel>();
    }
}
