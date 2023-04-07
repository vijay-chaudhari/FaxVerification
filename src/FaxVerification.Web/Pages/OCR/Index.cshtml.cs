using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Domain.Repositories;
using FaxVerification.Records;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace FaxVerification.Web.Pages.OCR
{
    
    public class IndexModel : AbpPageModel
    {
        public List<ImageOcr> Data { get; set; }
        private readonly IRepository<ImageOcr, Guid> _imageOCRApp;

        public PersonDetails personDetails { get; set; }
        //public IndexModel(IRepository<ImageOcr, Guid> repository)
        //{
        //    _imageOCRApp = repository;
        //}

        public async void OnGet()
        {
            try
            {
                //Data = (await _imageOCRApp.GetListAsync());
            }
            catch (Exception)
            {

                throw;
            }
        }
        public class PersonDetails
        {
            [Required]
            [Placeholder("Enter your name...")]
            [InputInfoText("What is your name?")]
            public string Name { get; set; }

            [Required]
            [Placeholder("Enter your DOB...")]
            [InputInfoText("What is your DOB?")]
            public DateOnly BirthDate { get; set; }
        }
    }
}
