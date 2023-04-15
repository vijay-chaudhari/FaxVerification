using FaxVerification.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FaxVerification.Web.Pages.OCR
{
    public class EditModalModel : PageModel
    {
        [BindProperty]
        public EditDetailsViewModel Data { get; set; }

        private readonly IOcrAppService _ocrAppService;
        public EditModalModel(IOcrAppService ocrAppService)
        {
            _ocrAppService = ocrAppService;
            Data = new EditDetailsViewModel(); 
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
            Data.PersonDetails = JsonSerializer.Deserialize<TextExtractionFields>(ImageOcrDto.Output);
        }

        public async Task<IActionResult> OnPostAsync()
        {
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
        }

        public class TextExtractionFields
        {
            public Invoice Invoice { get; set; }
            public TextExtractionFields()
            {
                Invoice = new();
            }
        }

        public class Invoice
        {
            [Display(Name = "Invoice Number")]
            public string Number { get; set; }

            [Display(Name = "Invoice Date")]
            public string Date { get; set; }

            [Display(Name = "Purchase Order")]
            public string PurchaseOrderNumber { get; set; }


            [Display(Name = "Purchase Order Date")]
            public string OrderDate { get; set; }



            [HiddenInput]
            public string Currency { get; set; }
            public Supplier Supplier { get; set; }
            public Customer Customer { get; set; }
            public Payment Payment { get; set; }
            public Invoice()
            {
                Supplier = new();
                Customer = new();
                Payment = new();
            }
        }

        public class Supplier
        {
            [Display(Name = "Vendor Name")]
            public string CompanyName { get; set; }

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
            [Display(Name = "Gross Amount")]
            public string Total { get; set; }

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
    }
}
