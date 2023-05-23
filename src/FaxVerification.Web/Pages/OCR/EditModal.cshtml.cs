using FaxVerification.Configuration;
using FaxVerification.Records;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static FaxVerification.Web.Pages.OCR.EditModalModel;
//using Microsoft.Extensions.Configuration;
//using Castle.Core.Configuration;



namespace FaxVerification.Web.Pages.OCR
{
    public class EditModalModel : PageModel
    {
        [BindProperty]
        public EditDetailsViewModel Data { get; set; }
        //[BindProperty]
        //public ConfigurationSettViewModel Configu { get; set; }
        //IConfiguration Configuration;
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment Environment;
        private readonly IOcrAppService _ocrAppService;
        public EditModalModel(IOcrAppService ocrAppService, IConfiguration configuration, IWebHostEnvironment _environment)
        {
            _ocrAppService = ocrAppService;
            _configuration = configuration;
            Environment = _environment;
            Data = new EditDetailsViewModel();
            Data.FormConfiguration = new ConfigurationSettViewModel();

            //Configu = new ConfigurationSettViewModel();
        }
        public async Task OnGetAsync(Guid id)
        {
            var ImageOcrDto = await _ocrAppService.GetAsync(id);
            Data.Id = ImageOcrDto.Id;
            Data.InputPath = ImageOcrDto.InputPath;
            Data.FilePath = Path.GetFileName(ImageOcrDto.OutputPath);
            Data.OutputPath = ImageOcrDto.OutputPath;
            Data.OCRText = ImageOcrDto.OCRText;
            Data.Confidence = ImageOcrDto.Confidence;
            Data.Output = ImageOcrDto.Output;
            try
            {

                Data.PersonDetails = JsonSerializer.Deserialize<TextExtractionFields>(ImageOcrDto.Output);
            }
            catch(Exception ex)
            {

            }
            //Data.PersonDetails = JsonSerializer.Deserialize<EditDetailsViewModel>(ImageOcrDto.Output);
            //string Formconfigutration = _configuration.GetValue<string>("ConfigurationAtrributes");
            var fileContents = System.IO.File.ReadAllText(Path.Combine(Environment.WebRootPath, "Configuration/Config.json"));


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

            for(var i = 0; i < person.Fields.Count; i++)
            {
                var field = Data.PersonDetails.AdditionalFields[i];
                //person.Fields[i].FieldName = field.FieldName;
                person.Fields[i].RegExpression = field.Text;
                person.Fields[i].CoOrdinates = field.Rectangle + "," + field.PageNumber;

            }


            Data.FormConfiguration = person;
            //Configu = person;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //var a = Configu;
            await _ocrAppService.UpdateAsync(
                Data.Id,
                new CreateUpdateImageOcrDto
                {
                    OutputPath = Data.OutputPath,
                    OCRText= Data.OCRText,
                    Confidence= Data.Confidence,
                    Output = JsonSerializer.Serialize(Data.PersonDetails),
                    InputPath = Data.InputPath
                });

            return new NoContentResult();
        }

        public async Task<IActionResult> OnPostHighlight(Request request)
        {

            return new NoContentResult();
        }

        public class EditDetailsViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }
            public TextExtractionFields PersonDetails { get; set; }

            //public DynamicViewModel PersonDetails { get; set; }

            [HiddenInput]
            public string InputPath { get; set; }
            [HiddenInput]
            public string OutputPath { get; set; }
            [HiddenInput]
            public string OCRText { get; set; }
            [HiddenInput]
            public string Confidence { get; set; }
            [HiddenInput]
            public string Output { get; set; }
            [HiddenInput]
            public string FilePath { get; set; }
            [HiddenInput]
            public ConfigurationSettViewModel FormConfiguration { get; set; }
        }

        public class ConfigurationSettViewModel
        {
            public string TemplateName { get; set; }
            public List<FieldConfig> Fields { get; set; }
           // public List<FieldsValueModel> FieldValue { get; set; }
        }

        public class ConfigFieldViewModel
        {
            public string TemplateName { get; set; }
            //public List<FieldConfig> Fields { get; set; }
             public List<ExtraFields> FieldValue { get; set; }
        }


        public class TextExtractionFields
        {
            public Patient Patient { get; set; }
            public Invoice Invoice { get; set; }
            public List<ExtraFields> AdditionalFields { get; set; }
            public TextExtractionFields()
            {
                Invoice = new();
                Patient = new();
                AdditionalFields = new List<ExtraFields>();
            }




        }
        public class Patient
        {
            public PatientName Name { get; set; }
            public PatientBirthDate BirthDate { get; set; }

            //[HiddenInput]
            //public string Name { get; set; }
            //[HiddenInput]
            //public string BirthDate { get; set; }
        }
        public class Invoice
        {
            public InvoiceNumber InvNum { get; set; }
            public InvoiceDate InvDate { get; set; }
            public PurchaseOrder OrderNum { get; set; }
            public PurchaseOrderDate OrderDate { get; set; }
            public VendorName VendorName { get; set; }
            public Tax Tax { get; set; }
            public GrossAmount Total { get; set; }

            public Invoice()
            {
                InvNum = new();
                InvDate = new();
                OrderNum = new();
                OrderDate = new();
                VendorName = new();
                Tax = new();
                Total = new();
            }



            //[Display(Name = "Invoice Number")]
            //public string Number { get; set; }
            //[HiddenInput]
            //public string InvNumCords { get; set; }


            //[Display(Name = "Invoice Date")]
            //public string Date { get; set; }
            //[HiddenInput]
            //public string InvDateCords { get; set; }


            //[Display(Name = "Purchase Order")]
            //public string PurchaseOrderNumber { get; set; }
            //[HiddenInput]
            //public string PurchaseOrderNumCords { get; set; }


            //[Display(Name = "Purchase Order Date")]
            //public string OrderDate { get; set; }
            //[HiddenInput]
            //public string OrderDateCords { get; set; }

            //[HiddenInput]
            //public string Currency { get; set; }
            //public Supplier Supplier { get; set; }
            //public Customer Customer { get; set; }
            //public Payment Payment { get; set; }
            //public Invoice()
            //{
            //    Supplier = new();
            //    Customer = new();
            //    Payment = new();
            //}
        }

        public class Supplier
        {
            [Display(Name = "Vendor Name")]
            public string CompanyName { get; set; }
            [HiddenInput]
            public string VendorNameCord { get; set; }

            [HiddenInput]
            [Display(Name = "Supplier Address")]
            public string Address { get; set; }

            [HiddenInput]
            [Display(Name = "Supplier Number")]
            public string BusinessNumber { get; set; }

            [HiddenInput]
            public string PhoneNumber { get; set; }

            [HiddenInput]
            [Display(Name = "Supplier Fax")]
            public string Fax { get; set; }

            [HiddenInput]
            [Display(Name = "Supplier Email")]
            public string Email { get; set; }

            [HiddenInput]
            [Display(Name = "Supplier WebSite")]
            public string Website { get; set; }
        }

        public class Customer
        {
            [HiddenInput]
            [Display(Name = "Customer ID")]
            public string Number { get; set; }

            [HiddenInput]
            [Display(Name = "Customer Name")]
            public string Name { get; set; }

            [HiddenInput]
            [Display(Name = "Customer Company Name")]
            public string CompanyName { get; set; }

            [HiddenInput]
            [Display(Name = "Customer Biling Address")]
            public string BillingAddress { get; set; }

            [HiddenInput]
            [Display(Name = "Customer Delivery Address")]
            public string DeliveryAddress { get; set; }

            [HiddenInput]
            [Display(Name = "Customer Number")]
            public string BusinessNumber { get; set; }

            [HiddenInput]
            public string PhoneNumber { get; set; }

            [HiddenInput]
            [Display(Name = "Customer Fax")]
            public string Fax { get; set; }

            [HiddenInput]
            [Display(Name = "Customer Email")]
            public string Email { get; set; }
        }

        public class Payment
        {
            [HiddenInput]
            [Display(Name = "Payment Due Date")]
            public string DueDate { get; set; }

            [HiddenInput]
            [Display(Name = "Base Amount")]
            public string BaseAmount { get; set; }
            [Display(Name = "Tax")]
            public string Tax { get; set; }
            [HiddenInput]
            public string TaxCord { get; set; }

            [Display(Name = "Gross Amount")]
            public string Total { get; set; }
            [HiddenInput]
            public string TotalCord { get; set; }

            [HiddenInput]
            [Display(Name = "Paid Amount")]
            public string Paid { get; set; }

            [HiddenInput]
            [Display(Name = "Due Amount")]
            public string DueAmount { get; set; }

            [HiddenInput]
            [Display(Name = "Payment Reference Number")]
            public string PaymentReference { get; set; }
        }

        public class Request 
        {
            public string FileName { get; set; }
            public Rectangle rect { get; set; }
            public int PageNumber { get; set; }
        }

        public class PatientName
        {
            
            [Display(Name = "Patient Name")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }

        }

        public class PatientBirthDate
        {
           
            [Display(Name = "Birth Date")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }

        }
        public class InvoiceNumber
        {
            [Display(Name = "Invoice Number")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }

        }
        public class InvoiceDate
        {
            [Display(Name = "Invoice Date")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }

        }
        public class PurchaseOrder
        {
            [Display(Name = "Purchase Order")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }

        }
        public class PurchaseOrderDate
        {
            [Display(Name = "Purchase Order Date")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }
        }
        public class VendorName
        {
            [Display(Name = "Vendor Name")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }
        }
        public class Tax
        {
            [Display(Name = "Tax")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }
        }
        public class GrossAmount
        {
            [Display(Name = "Gross Amount")]
            public string Text { get; set; }
            [HiddenInput]
            public int PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }

        }
        
        public class DynamicViewModel
        {
            [HiddenInput]
            public string AttributeCords_1 { get; set; }
            public string Attribute_1 { get; set; }
            [HiddenInput]
            public string AttributeCords_2 { get; set; }
            public string Attribute_2 { get; set; }
            [HiddenInput]
            public string AttributeCords_3 { get; set; }
            public string Attribute_3 { get; set; }
            [HiddenInput]
            public string AttributeCords_4 { get; set; }
            public string Attribute_4 { get; set; }
            [HiddenInput]
            public string AttributeCords_5 { get; set; }
            public string Attribute_5 { get; set; }
            [HiddenInput]
            public string AttributeCords_6 { get; set; }
            public string Attribute_6 { get; set; }
            [HiddenInput]
            public string AttributeCords_7 { get; set; }
            public string Attribute_7 { get; set; }
            [HiddenInput]
            public string AttributeCords_8 { get; set; }
            public string Attribute_8 { get; set; }

        }

        public class ExtraFields
        {
            [HiddenInput]
            public string Text { get; set; }
            [HiddenInput]
            public string FieldName { get; set; }
            [HiddenInput]
            public int? PageNumber { get; set; }
            [HiddenInput]
            public string Rectangle { get; set; }
        }

    }
}
