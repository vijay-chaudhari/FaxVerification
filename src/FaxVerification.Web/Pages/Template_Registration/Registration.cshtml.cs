using FaxVerification.Records;
using FaxVerification.TemplateRegistration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Users;
using static FaxVerification.Web.Pages.OCR.EditModalModel;

namespace FaxVerification.Web.Pages.Template_Registration
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public RegisterDetailsViewModel Data { get; set; }
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment Environment;
        private readonly IRegistrationService _registrationService;
        private readonly ICurrentUser _currentUser;

        public RegistrationModel(IRegistrationService registrationService, IConfiguration configuration, IWebHostEnvironment _environment, ICurrentUser currentUser)
        {
            _registrationService = registrationService;
            _configuration = configuration;
            Environment = _environment;
            _currentUser = currentUser;
            Data = new RegisterDetailsViewModel();


        }
        public async Task OnGetAsync(Guid id)
        {
            var ImageOcrDto = await _registrationService.GetAsync(id);
            Data.Id = ImageOcrDto.Id;
            //Data.InputPath = ImageOcrDto.InputPath;
            Data.FilePath = Path.GetFileName(ImageOcrDto.FilePath);
            //Data.OutputPath = ImageOcrDto.OutputPath;
            Data.OCRConfig = ImageOcrDto.OCRConfig;
            //Data.Confidence = ImageOcrDto.Confidence;
            //Data.Output = ImageOcrDto.Output;
            try
            {

                Data.PersonDetails = JsonSerializer.Deserialize<TextExtractionFields>(ImageOcrDto.OCRConfig);
            }
            catch (Exception ex)
            {

            }
            //Data.PersonDetails = JsonSerializer.Deserialize<EditDetailsViewModel>(ImageOcrDto.Output);
            //string Formconfigutration = _configuration.GetValue<string>("ConfigurationAtrributes");
            var fileContents = System.IO.File.ReadAllText(Path.Combine(Environment.WebRootPath, "Configuration/Config.json"));

            Data.CurrentUserID = Convert.ToString(_currentUser.Id);

            var person = JsonSerializer.Deserialize<ConfigurationSettViewModel>(fileContents);


            //string Formconfigutration = "";
            //if (person.Attribute_1 != null)
            //    Formconfigutration += person.Attribute_1;
            //if (person.Attribute_2 != null)
            //    Formconfigutration += "~" + person.Attribute_2;
            //if (person.Attribute_3 != null)
            //    Formconfigutration += "~" + person.Attribute_3;
            //if (person.Attribute_4 != null)
            //    Formconfigutration += "~" + person.Attribute_4;
            //if (person.Attribute_5 != null)
            //    Formconfigutration += "~" + person.Attribute_5;
            //if (person.Attribute_6 != null)
            //    Formconfigutration += "~" + person.Attribute_6;
            //if (person.Attribute_7 != null)
            //    Formconfigutration += "~" + person.Attribute_7;
            //if (person.Attribute_8 != null)
            //    Formconfigutration += "~" + person.Attribute_8;



            for (var i = 0; i < person.Fields.Count; i++)
            {
                if (Data.PersonDetails != null && i < Data.PersonDetails.AdditionalFields.Count)
                {
                    //var field = Data.PersonDetails.Invoice.AdditionalFields[i];
                    ////person.Fields[i].FieldName = field.FieldName;
                    //person.Fields[i].RegExpression = field.Text;
                    //person.Fields[i].CoOrdinates = field.Rectangle + "," + field.PageNumber;

                    person.Fields[i].RegExpression = "";
                    person.Fields[i].CoOrdinates = "";
                }
                else
                {
                    person.Fields[i].RegExpression = "";
                    person.Fields[i].CoOrdinates = "";
                }
            }

            if(Data.PersonDetails != null)
            {
                for (var i = 0; i < Data.PersonDetails.AdditionalFields.Count; i++)
                {
                    for (var j = 0; j < person.Fields.Count; j++)
                    {
                        if (person.Fields[j].FieldName == Data.PersonDetails.AdditionalFields[i].FieldName)
                        {
                            var field = Data.PersonDetails.AdditionalFields[i];
                            person.Fields[j].RegExpression = field.Text;
                            person.Fields[j].CoOrdinates = field.Rectangle + "," + field.PageNumber;

                        }


                        if (Data.PersonDetails.AdditionalFields[i].Text != "" && Data.PersonDetails.AdditionalFields[i].Text != null)
                        {

                        }



                    }

                }
            }
        

            Data.FormConfiguration = person;



        }

        public async Task<IActionResult> OnPostAsync()
        {
            //var a = Configu;
            await _registrationService.UpdateAsync(
                Data.Id,
                new CreateUpdateRegistrationDto
                {
                    VendorNo = Data.vendorNo,
                    TemplateName = Data.TemplateName,
                    FilePath = Data.FilePath,
                    OCRConfig = JsonSerializer.Serialize(Data.PersonDetails)
                });

            return new NoContentResult();
        }

        public async Task<IActionResult> OnPostHighlight(Request request)
        {

            return new NoContentResult();
        }

    }

    public class RegisterDetailsViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }
        public TextExtractionFields PersonDetails { get; set; }

        public TableContent TableCordinates { get; set; }

        [HiddenInput]
        public string FilePath { get; set; }
        [HiddenInput]
        public string OCRConfig { get; set; }
        [HiddenInput]
        public string TemplateName   { get; set; }
        [HiddenInput]
        public string vendorNo { get; set; }
        [HiddenInput]
        public ConfigurationSettViewModel FormConfiguration { get; set; }

        [HiddenInput]
        public string CurrentUserID { get; set; }

        //public RegisterDetailsViewModel()
        //{
        //    TableCordinates = new();
        //}
    }

    public class TableContent
    {
        [Display(Name = "Table Content")]
        [HiddenInput]
        public string Text { get; set; }
        [HiddenInput]
        public int PageNumber { get; set; }
        [HiddenInput]
        public string Rectangle { get; set; }

    }
}
