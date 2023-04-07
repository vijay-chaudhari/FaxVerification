using FaxVerification.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
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

        public override Task<PagedResultDto<OcrDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }

        //public async Task<List<OcrDto>> GetListAsync()
        //{
        //    var querayble = await Repository.GetQueryableAsync();
        //    var query = querayble.Select(x => x).ToList();
        //    List<OcrDto> result = new List<OcrDto>();
        //    foreach (var item in query)
        //    {
        //        result.Add(new OcrDto
        //        {
        //            Id = item.Id,
        //            InputPath = item.InputPath,
        //            OutputPath = item.OutputPath,
        //            Output = item.Output
        //        });
        //    }
        //    return result;
        //}
    }
}
