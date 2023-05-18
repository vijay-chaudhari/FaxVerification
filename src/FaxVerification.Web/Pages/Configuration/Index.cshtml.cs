using FaxVerification.Configuration;
using FaxVerification.Records;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Users;
using static FaxVerification.Web.Pages.OCR.EditModalModel;

namespace FaxVerification.Web.Pages.Configuration
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public EditDetailsViewModel Data { get; set; }
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment Environment;

        public IndexModel(IConfiguration configuration, IWebHostEnvironment _environment)
        {
            _configuration = configuration;
            Environment = _environment;
            Data = new EditDetailsViewModel();
            Data.PersonDetails = new DynamicViewModel();

           
        }
        public void OnGet()
        {
            string Formconfigutration = _configuration.GetValue<string>("ConfigurationAtrributes");

            //var dataFile = Server.MapPath("~/App_Data/data.txt");
            //string[] filePaths = Directory.GetFiles(Path.Combine(Environment.WebRootPath, "Configuration/Config.json"));

            var fileContents = System.IO.File.ReadAllText(Path.Combine(Environment.WebRootPath, "Configuration/Config.json"));
            var person = JsonSerializer.Deserialize<ConfigurationSettings>(fileContents);
            Data.FormConfiguration = person;

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

        public async Task<IActionResult> OnPostAsync()
        {
            //var fileContents = System.IO.File.ReadAllText(Path.Combine(Environment.WebRootPath, "Configuration/Config.json"));
            var filepath = Path.Combine(Environment.WebRootPath, "Configuration/Config.json");

            var userData = JsonSerializer.Serialize<ConfigurationSettings>(Data.FormConfiguration);

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
