using FaxVerification.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FaxVerification.Records
{
    [Authorize(FaxVerificationPermissions.Documents.Default)]
    public class OcrAppService :
        CrudAppService<
        ImageOcr, //The Book entity
        OcrDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto,
        CreateUpdateImageOcrDto>,//Used for paging/sorting
        IOcrAppService
    {
        public IRepository<ImageOcr, Guid> Repository { get; }

        public OcrAppService(IRepository<ImageOcr, Guid> repository) : base(repository)
        {
            Repository = repository;
            GetPolicyName = FaxVerificationPermissions.Documents.Default;
            GetListPolicyName = FaxVerificationPermissions.Documents.Default;
            CreatePolicyName = FaxVerificationPermissions.Documents.Create;
            UpdatePolicyName = FaxVerificationPermissions.Documents.Edit;
            DeletePolicyName = FaxVerificationPermissions.Documents.Create;
        }

        public override async Task<PagedResultDto<OcrDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Task<PagedResultDto<OcrDto>> result = Repository.GetListAsync(input);
            Guid? UserID = CurrentUser.Id;
            List<ImageOcr> resultlist = await Repository.GetListAsync(true);
            var result = ObjectMapper.Map<List<ImageOcr>, List<OcrDto>>(resultlist).ToList();//.Where(a=>a.AssignedTo == UserID || a.AssignedTo == null).ToList();
            result.ForEach(a => a.CurrentUserID = UserID);
            return new PagedResultDto<OcrDto> { TotalCount = result.Count, Items = result }; ; //base.GetListAsync(input);
        }
    }
}
