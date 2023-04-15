using FaxVerification.Permissions;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.PortableExecutable;
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

        public async Task Highlight(Request request)
        {

            HighliightPdf(request);
        }

        public class Request
        {
            public string FileName { get; set; }
            public float X1 { get; set; }
            public float Y1 { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public int PageNumber { get; set; }
        }

        private void HighliightPdf(Request request)
        {

            float constant = 4.166666666666667f;

            PdfReader reader = new PdfReader(request.FileName);
            using FileStream fs = new FileStream(request.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            using PdfStamper stamper = new(reader, fs);

            try
            {
                iTextSharp.text.Rectangle rectangle = reader.GetPageSize(request.PageNumber);
                float newX1 = request.X1 / constant;
                float newY1 = rectangle.Height - (request.Y1 / constant);
                float newX2 = request.Width / constant;
                float newY2 = rectangle.Height - (request.Height / constant);
                //Create an array of quad points based on that rectangle. NOTE: The order below doesn't appear to match the actual spec but is what Acrobat produces
                iTextSharp.text.Rectangle newRect = new iTextSharp.text.Rectangle(newX1, newY1, newX2, newY2);
                float[] quad = { newRect.Right, newRect.Bottom, newRect.Left, newRect.Bottom, newRect.Right, newRect.Top, newRect.Left, newRect.Top };

                //Create our hightlight
                PdfAnnotation highlight = PdfAnnotation.CreateMarkup(stamper.Writer, newRect, null, PdfAnnotation.MARKUP_HIGHLIGHT, quad);

                //Set the color
                highlight.Color = iTextSharp.text.BaseColor.YELLOW;

                //Add the annotation
                stamper.AddAnnotation(highlight, request.PageNumber);
            }
            catch (Exception ex)
            {

                //_logger.LogError("Error in HighliightPdf {ex}", ex);
            }

            #region Pixel to Point concept
            // W:612 H:792 points, where 1 point contains = 1/72 = 0.0138888888888889 pixel here 72 is standard number for pdf documents
            // W:8.5 H: 11 inch
            // 1 inch = 72 pixel
            // Tiff image DPI = 300 means 300 pixel in 1 inch
            // 300 / 72 = 4.166666666666667 pixel
            // So pdf pixel size will be
            // W: 612 x 4.166666666666667 H:792 x 4.166666666666667
            // W : 2550 H: 3300 pixel

            // Now we have rectangle Coords as Pixel so to highlight it on pdf convert them to points
            // x1 = 1526 pixel / 4.166666666666667 = 326.24 points
            // y1 = 552 pixel / 4.166666666666667 = 132.48 points
            // x2 = 2308 pixel / 4.166666666666667 = 553.92 points
            //y2 = 603 pixel / 4.166666666666667 = 144.72 points

            //float w = (rectangle.Width * (300 / 72));
            //float x_scale = rectangle.Width / w;
            //float h = (rectangle.Height * (300 / 72));
            //float y_scale = rectangle.Height / h; 
            #endregion

            GC.Collect();

            //File.Delete(Path.Combine(_inputPath, documents.FileName));
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
