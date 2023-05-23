using FaxVerification.Configuration;
using FaxVerification.Records;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Data;
using Volo.Abp.Users;
using static FaxVerification.Web.Pages.OCR.EditModalModel;

namespace FaxVerification.Web.Pages.Configuration
{
    public class IndexModel : FaxVerificationPageModel
    {
        [BindProperty]
        //public EditDetailsViewModel Data { get; set; }
        public ConfigurationSettViewModel Data { get; set; }

        private readonly IConfigAppService _configAppService;

        private readonly IConfiguration _configuration;
        private IWebHostEnvironment Environment;

        public IndexModel(IConfiguration configuration, IWebHostEnvironment _environment,IConfigAppService configAppService)
        {
            _configuration = configuration;
            Environment = _environment;
            _configAppService = configAppService;
            //Data = new EditDetailsViewModel();
            //Data.PersonDetails = new DynamicViewModel();
            //Data.FormConfiguration = new ConfigurationSettViewModel();

            Data = new ConfigurationSettViewModel();


        }
        public async void OnGet()
        {
            string Formconfigutration = _configuration.GetValue<string>("ConfigurationAtrributes");

            //var dataFile = Server.MapPath("~/App_Data/data.txt");
            //string[] filePaths = Directory.GetFiles(Path.Combine(Environment.WebRootPath, "Configuration/Config.json"));
            //CreateUpdateConfigurationSettingsDto s = new CreateUpdateConfigurationSettingsDto();
            //Guid id = new Guid("BE0F2722-6BB5-B833-D5A1-3A0B56FBC344");
            //var S =  _configAppService.GetAsync(id);


            var fileContents = System.IO.File.ReadAllText(Path.Combine(Environment.WebRootPath, "Configuration/Config.json"));
            //ConfigurationSettViewModel person = JsonSerializer.Deserialize<ConfigurationSettViewModel>(fileContents);
            Data = JsonSerializer.Deserialize<ConfigurationSettViewModel>(fileContents);
            //Data.FormConfiguration = person;

            //string[] ConfigArrray = Formconfigutration.Split('~');
            //if (ConfigArrray.Length >= 8)
            //    Data.PersonDetails.Attribute_8 = Convert.ToString(ConfigArrray[7]);
            //if (ConfigArrray.Length >= 7)
            //    Data.PersonDetails.Attribute_7 = Convert.ToString(ConfigArrray[6]);
            //if (ConfigArrray.Length >= 6)
            //    Data.PersonDetails.Attribute_6 = Convert.ToString(ConfigArrray[5]);
            //if (ConfigArrray.Length >= 5)
            //    Data.PersonDetails.Attribute_5 = Convert.ToString(ConfigArrray[4]);
            //if (ConfigArrray.Length >= 4)
            //    Data.PersonDetails.Attribute_4 = Convert.ToString(ConfigArrray[3]);
            //if (ConfigArrray.Length >= 3)
            //    Data.PersonDetails.Attribute_3 = Convert.ToString(ConfigArrray[2]);
            //if (ConfigArrray.Length >= 2)
            //    Data.PersonDetails.Attribute_2 = Convert.ToString(ConfigArrray[1]);
            //if (ConfigArrray.Length >= 1)
            //    Data.PersonDetails.Attribute_1 = Convert.ToString(ConfigArrray[0]);

        }

        public void OnPost()
        {
            var a = Data;
        }

        [HttpPost]
        public IActionResult OnSave()
        {
            //var fileContents = System.IO.File.ReadAllText(Path.Combine(Environment.WebRootPath, "Configuration/Config.json"));
            var filepath = Path.Combine(Environment.WebRootPath, "Configuration/Config.json");

            var userData = JsonSerializer.Serialize<ConfigurationSettViewModel>(Data);

            System.IO.File.WriteAllText(@filepath, userData);

            return new NoContentResult();
        }

        public async Task<IActionResult> OnPostSave()
        {
            //CreateWebName();
            //localStorage
            // all  done
            return Page();
        }
    }
}
