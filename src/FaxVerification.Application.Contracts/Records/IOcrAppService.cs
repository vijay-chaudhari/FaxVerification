using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaxVerification.Records
{
    public interface IOcrAppService : 
        ICrudAppService< //Defines CRUD methods
        OcrDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateImageOcrDto> //Used to create/update a book
    {
       // Task<List<OcrDto>> GetListAsync();
    }
}
