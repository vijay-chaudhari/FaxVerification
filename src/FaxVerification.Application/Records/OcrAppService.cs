using FaxVerification.Configuration;
using FaxVerification.Permissions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Scriban.Runtime.Accessors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict;
using Volo.Abp.UI.Navigation;
using static OpenIddict.Abstractions.OpenIddictConstants;

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

        //public IConfigAppService _configAppService;

        public OcrAppService(IRepository<ImageOcr, Guid> repository) : base(repository)
        {
            Repository = repository;
            GetPolicyName = FaxVerificationPermissions.Documents.Default;
            GetListPolicyName = FaxVerificationPermissions.Documents.Default;
            CreatePolicyName = FaxVerificationPermissions.Documents.Create;
            UpdatePolicyName = FaxVerificationPermissions.Documents.Edit;
            DeletePolicyName = FaxVerificationPermissions.Documents.Create;

            // _configAppService = configAppService;

        }

        public override async Task<PagedResultDto<OcrDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            PagedResultDto<OcrDto> result1 = await base.GetListAsync(input);
            Guid? UserID = CurrentUser.Id;
            List<ImageOcr> resultlist = await Repository.GetListAsync(true);
            List<OcrDto> ocrDtos = result1.Items.ToList();
            ocrDtos.ForEach(a => a.CurrentUserID = UserID);
            return result1;

        }



        public async Task<string> UPDATEOCRDATA(ApiRequest request)
        {
            HttpClient client = new HttpClient();

            string Token = await GetTokenAsync();

            request.token = Token;

            string FileName = "";
            foreach (var i in request.inParams)
            {
                if (i.id == 6)
                {
                    FileName = i.value;
                }
            }

            string ISFileAvailable = "2"; //await CheckFileName(FileName,Token);

            if (ISFileAvailable != "0")
            {


                string[] G = FileName.Split("_");
                string a = G[0];
                int emailSerial = Convert.ToInt32(a);


                await CyrtAPIAsync(Convert.ToString(emailSerial), "10");


                foreach (var i in request.inParams.ToList())
                {
                    if (i.id < 6)
                    {
                        request.inParams.Remove(i);
                    }

                    if (i.id == 6)
                    {
                        i.value = Convert.ToString(emailSerial);
                    }
                }

                foreach (var i in request.outputIds.ToList())
                {
                    if (i < 6)
                    {
                        request.outputIds.Remove(i);
                    }
                }


                string ocrDataUrl = "http://123.201.35.66:9192/jderest/v2/bsfnservice";


                string ocrDataRequestBody = System.Text.Json.JsonSerializer.Serialize(request);

                //"{\r\n    \"token\" : \"04419go4yFvF83UuDpEdmz21xHx94BY3fZqaA7/RT9makE=MDE4MDE0Mjk2MzgwOTk1OTE0OTk3ODA5MTAzLjE4MS4xNy4yMTExNjg1ODUxODcyMjcx\",\r\n    \"name\": \"UpdateOCRDataInWorkBenchInvoice\",\r\n    \"isAsync\" : false,\r\n    \"inParams\" : [\r\n        {\r\n            \"id\": 6,\r\n            \"value\": \"7\"\r\n        },\r\n        {\r\n            \"id\": 8,\r\n            \"value\":\"INV123\"\r\n        },\r\n        {\r\n            \"id\":9,\r\n            \"value\": \"2023/04/26\"\r\n        },\r\n        {\r\n            \"id\":10,\r\n            \"value\":\"1234.32\"\r\n        },\r\n        {\r\n            \"id\":11,\r\n            \"value\":\"2023/04/26\"\r\n        },\r\n        {\r\n            \"id\":12,\r\n            \"value\":\"ABCD Corporation\"\r\n        }\r\n    ],\r\n    \"outputIds\" : [\r\n        8,9,10,11,12\r\n    ]\r\n}\r\n";

                HttpResponseMessage response = await client.PostAsync(ocrDataUrl, new StringContent(ocrDataRequestBody, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    // Successful response
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    UpdateResponse userInfo = System.Text.Json.JsonSerializer.Deserialize<UpdateResponse>(jsonResponse);
                    return userInfo.template;

                }
                else
                {
                    return "Error";
                }
            }
            else
            {

                request.name = "CreateInvoiceInWorkBench";
                try
                {
                    foreach (var i in request.inParams.ToList())
                    {
                        if (i.id > 6)
                        {
                            request.inParams.Remove(i);
                        }
                    }

                    foreach (var i in request.outputIds.ToList())
                    {
                        if (i > 6)
                        {
                            request.outputIds.Remove(i);
                        }
                    }
                }
                catch (Exception ex)
                {

                }


                string ocrDataUrl = "http://123.201.35.66:9192/jderest/v2/bsfnservice";

                string ocrDataRequestBody = System.Text.Json.JsonSerializer.Serialize(request);

                //"{\r\n    \"token\" : \"04419go4yFvF83UuDpEdmz21xHx94BY3fZqaA7/RT9makE=MDE4MDE0Mjk2MzgwOTk1OTE0OTk3ODA5MTAzLjE4MS4xNy4yMTExNjg1ODUxODcyMjcx\",\r\n    \"name\": \"UpdateOCRDataInWorkBenchInvoice\",\r\n    \"isAsync\" : false,\r\n    \"inParams\" : [\r\n        {\r\n            \"id\": 6,\r\n            \"value\": \"7\"\r\n        },\r\n        {\r\n            \"id\": 8,\r\n            \"value\":\"INV123\"\r\n        },\r\n        {\r\n            \"id\":9,\r\n            \"value\": \"2023/04/26\"\r\n        },\r\n        {\r\n            \"id\":10,\r\n            \"value\":\"1234.32\"\r\n        },\r\n        {\r\n            \"id\":11,\r\n            \"value\":\"2023/04/26\"\r\n        },\r\n        {\r\n            \"id\":12,\r\n            \"value\":\"ABCD Corporation\"\r\n        }\r\n    ],\r\n    \"outputIds\" : [\r\n        8,9,10,11,12\r\n    ]\r\n}\r\n";

                HttpResponseMessage response = await client.PostAsync(ocrDataUrl, new StringContent(ocrDataRequestBody, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    // Successful response
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    UpdateResponse userInfo = System.Text.Json.JsonSerializer.Deserialize<UpdateResponse>(jsonResponse);
                    return userInfo.template;

                }
                else
                {
                    return "Error";
                }

            }



        }

        private async Task CyrtAPIAsync(string emailSerial, string value)
        {
            string Token = await GetTokenAsync();

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


        public async Task<string> GetTokenAsync()
        {
            string token = string.Empty;

            token = await Login();

            return token;
        }


        static async Task<string> Login()
        {
            HttpClient client = new HttpClient();
            string loginUrl = "http://123.201.35.66:9192/jderest/v2/tokenrequest";

            UserAccount user = new UserAccount();
            user.username = "harishp";
            user.password = "harishp";

            string loginRequestBody = System.Text.Json.JsonSerializer.Serialize(user);

            //"{\r\n    \"username\":\"harishp\",\r\n    \"password\":\"harishp\"\r\n}";

            HttpResponseMessage response = await client.PostAsync(loginUrl, new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                // Successful response
                string jsonResponse = await response.Content.ReadAsStringAsync();
                OCRWorkbench userInfo = System.Text.Json.JsonSerializer.Deserialize<OCRWorkbench>(jsonResponse);
                return userInfo.userInfo.token;

            }
            else
            {
                return "Error";
            }
            // Handle the login response as needed

        }
        static async Task ValidateToken(HttpClient client)
        {
            string tokenValidationUrl = "http://123.201.35.66:9192/jderest/v2/tokenrequest/validate";
            string tokenValidationRequestBody = "{\r\n    \"token\": \"04419go4yFvF83UuDpEdmz21xHx94BY3fZqaA7/RT9makE=MDE4MDE0Mjk2MzgwOTk1OTE0OTk3ODA5MTAzLjE4MS4xNy4yMTExNjg1ODUxODcyMjcx\"\r\n}";

            HttpResponseMessage response = await client.PostAsync(tokenValidationUrl, new StringContent(tokenValidationRequestBody));

            // Handle the token validation response as needed
        }



        public async Task<string> CheckFileName(string FileName, string Token)
        {
            string url = "http://123.201.35.66:9192/jderest/v2/dataservice";
            //string username = "harishp";
            //string password = "harishp";
            //string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            string data = @"{
            ""token"": ""{ToKen}"",
            ""targetName"": ""V5643001"",
            ""targetType"": ""view"",
            ""dataServiceType"": ""BROWSE"",
            ""returnControlIDs"": ""F5643001.Y56FLNM|F5643001.WFEMAILNO|F5643001.Y56EMID"",
            ""query"": {
                ""autoFind"": true,
                ""condition"": [
                    {
                        ""value"": [
                            {
                                ""content"": ""{FILENAMe}"",
                                ""specialValueId"": ""LITERAL""
                            }
                        ],
                        ""controlId"": ""F5643001.Y56FLNM"",
                        ""operator"": ""EQUAL""
                    }
                ]
            }
        }";

            data = data.Replace("{FILENAMe}", FileName);
            data = data.Replace("{ToKen}", Token);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    RootObject obj = System.Text.Json.JsonSerializer.Deserialize<RootObject>(result);

                    dynamic ass = JsonConvert.DeserializeObject(result);
                    var item = ass.fs_DATABROWSE_V5643001.data.gridData.summary.records;

                    string itmcount = Convert.ToString(item);

                    if (Convert.ToInt32(itmcount) >= 1)
                    {
                        item = ass.fs_DATABROWSE_V5643001.data.gridData.rowset[0].F5643001_WFEMAILNO;
                    }
                    return Convert.ToString(item);
                }
                else
                {
                    return "Error";
                }
            }
        }



        public async Task<string> GetVendorName(string EmailSr)
        {
            string url = "http://123.201.35.66:9192/jderest/v2/dataservice";
            //string username = "harishp";
            string Token = await GetTokenAsync();
            //string password = "harishp";
            //string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            string data = @"{
        ""token"": ""{ToKen}"",
        ""targetName"": ""V550102"",
        ""targetType"": ""view"",
        ""dataServiceType"": ""BROWSE"",
        ""maxPageSize"": ""3000"",
        ""returnControlIDs"": ""F5643001.AN8V|F5643001.VDNM|F5643001.Y56EMID|F5643001.Y56DTAMT|F5643001.Y56EMDT|F5643001.Y56EMSUB|F5643001.S74IVD|F5643001.Y56VINVA|F5643001.Y56PSTS|F5643001.KCOO|F5643001.DOCO|F5643001.DCTO|F5643001.Y56INVDT|F5643002.UORG|F5643002.ITM|F5643002.LITM|F5643002.CITM|F5643002.UNCS|F5643002.AEXP|F5643002.CRCD|F5643001.VINV|F5643001.Y56TAXA|F5643001.Y56TAMT|F5643001.EXA"",
        ""query"": {
            ""autoFind"": true,
            ""condition"": [
                {
                    ""value"": [
                        {
                            ""content"": ""{EmailSr}"",
                            ""specialValueId"": ""LITERAL""
                        }
                    ],
                    ""controlId"": ""F5643002.WFEMAILNO"",
                    ""operator"": ""EQUAl""
                }
            ]
        }
    }";

            data = data.Replace("{EmailSr}", EmailSr);
            data = data.Replace("{ToKen}", Token);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    //Root obj = System.Text.Json.JsonSerializer.Deserialize<Root>(result);
                  
                        Root root = JsonConvert.DeserializeObject<Root>(result);
                  
                    

                    dynamic ass = JsonConvert.DeserializeObject(result);
                    string reurnReslut = "";
                    try
                    {
                        var itemRow = root.Fs_DATABROWSE_V550102.Data.GridData.Rowset.Count;
                        if(itemRow > 0)
                        {
                            var item = root.Fs_DATABROWSE_V550102.Data.GridData.Rowset[0].F5643001_AN8V;
                            var itemName = root.Fs_DATABROWSE_V550102.Data.GridData.Rowset[0].F5643001_VDNM;
                            reurnReslut = item +"_"+itemName;
                        }
                        else
                        {
                            return "NoRecodFound";
                        }
                      
                    }
                    catch(Exception ex)
                    {

                    }
                    
                    return reurnReslut;
                }
                else
                {
                    return "Error";
                }
            }
        }

    }

    public class ApiRequest
    {
        public string token { get; set; }
        public string name { get; set; }
        public bool isAsync { get; set; }
        public List<InParam> inParams { get; set; }
        public List<int> outputIds { get; set; }
    }

    public class InParam
    {
        public int id { get; set; }
        public string value { get; set; }
    }

    public class UserAccount
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
