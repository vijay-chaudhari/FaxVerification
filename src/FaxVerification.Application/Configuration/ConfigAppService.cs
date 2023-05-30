using FaxVerification.Permissions;
using FaxVerification.Records;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FaxVerification.Configuration
{
    [Authorize(FaxVerificationPermissions.Documents.Default)]
    public class ConfigAppService : CrudAppService<
        ConfigurationSettings, //The Book entity
        ConfigurationSettingsDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto,
        CreateUpdateConfigurationSettingsDto>,//Used for paging/sorting
        IConfigAppService
    {
        private readonly IOcrAppService _ocrAppService;
        public ConfigAppService(IRepository<ConfigurationSettings, Guid> repository , IOcrAppService ocrAppService) : base(repository)
        {
            _ocrAppService = ocrAppService;
        }

        public override async Task<PagedResultDto<ConfigurationSettingsDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var a = ObjectMapper.Map<List<ConfigurationSettings>, List<ConfigurationSettingsDto>>(await Repository.GetListAsync(true));
            return new PagedResultDto<ConfigurationSettingsDto> { TotalCount = a.Count, Items = a };
        }

        public async Task AssignDocument (AssignDocumentRequest request)
        {
            Guid? UserID = CurrentUser.Id;
            Guid DocumentID = new Guid(request.DocumentID);

            var ImageOcrDto = await _ocrAppService.GetAsync(DocumentID);

            ImageOcrDto.AssignedTo = UserID;

            await _ocrAppService.UpdateAsync(
               ImageOcrDto.Id,
               new CreateUpdateImageOcrDto
               {
                   OutputPath = ImageOcrDto.OutputPath,
                   OCRText = ImageOcrDto.OCRText,
                   Confidence = ImageOcrDto.Confidence,
                   Output = ImageOcrDto.Output,
                   InputPath = ImageOcrDto.InputPath,
                   AssignedTo = UserID,
                   
               });


        }

    }

    public class AssignDocumentRequest
    {
        public string DocumentID { get; set; }
        public string UserID { get; set; }
    }
}
