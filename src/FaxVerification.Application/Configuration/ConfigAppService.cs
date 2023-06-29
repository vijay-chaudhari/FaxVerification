using FaxVerification.Permissions;
using FaxVerification.Records;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;

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

        public async Task AssignDocument(AssignDocumentRequest request)
        {
            Guid? UserID = CurrentUser.Id;
            Guid DocumentID = new Guid(request.DocumentID);

            var ImageOcrDto = await _ocrAppService.GetAsync(DocumentID);

            if (ImageOcrDto.AssignedTo != null)
            {

            }
            else
            {

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



                string[] filename = ImageOcrDto.OutputPath.Split("\\");
                string Filenam = filename[filename.Count() - 1];
                string[] G = Filenam.Split("_");
                string a = G[0];
                int emailSerial = Convert.ToInt32(a);


                await CyrtAPIAsync(Convert.ToString(emailSerial), "07");

            }

        }

        public async Task UnAssignDocument(AssignDocumentRequest request)
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
                   AssignedTo = null,

               });



            string[] filename = ImageOcrDto.OutputPath.Split("\\");
            string Filenam = filename[filename.Count()-1];
            string[] G = Filenam.Split("_");
            string a = G[0];
            int emailSerial = Convert.ToInt32(a);


            await CyrtAPIAsync(Convert.ToString(emailSerial), "05");



        }


        private async Task CyrtAPIAsync(string emailSerial,string value)
        {
            string Token = await _ocrAppService.GetTokenAsync();

            string url = "http://123.201.35.66:9192/jderest/v2/bsfnservice";

            var Data = @"{
                         ""token"": ""{Token}"",
                         ""name"": ""UpdateInvoiceStatus"",
                          ""isAsync"": false,
                          ""inParams"": [
                            {
                              ""id"": 1,
                              ""value"": ""{EmailValue}""
                            },
                            {
                              ""id"": 2,
                              ""value"": ""{Value}""
                            }
                          ],
                          ""outputIds"": [
                            1,
                            2
                          ]
                        }";

            Data = Data.Replace("{Token}", Token);
            Data = Data.Replace("{EmailValue}", emailSerial);
            Data = Data.Replace("{Value}", value);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

                var content = new StringContent(Data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();


                    //return Convert.ToString(item);
                }
                else
                {
                    //return "Error";
                }
            }


        }

        public async Task SaveConfigFile(AssignDocumentRequest request)
        {
            string FileContent = request.UserID;
            string FileRoot = request.DocumentID;
            var filepath = Path.Combine(FileRoot, "Configuration/Config.json");

            var userData = FileContent;// JsonSerializer.Serialize<string>(FileContent);

            System.IO.File.WriteAllText(@filepath, userData);
        }
    }

    public class AssignDocumentRequest
    {
        public string DocumentID { get; set; }
        public string UserID { get; set; }
    }
}
