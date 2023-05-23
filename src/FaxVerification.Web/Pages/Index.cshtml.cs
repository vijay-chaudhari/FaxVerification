using Microsoft.AspNetCore.Mvc;
using System;
using Volo.Abp.Users;
using static FaxVerification.Web.Pages.OCR.EditModalModel;

namespace FaxVerification.Web.Pages;

public class IndexModel : FaxVerificationPageModel
{

    [BindProperty]
    public IndexPageModel Data { get; set; }

    private readonly ICurrentUser _currentUser;
    public IndexModel(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
        Data = new IndexPageModel();
    }
    public void OnGet()
    {
        Guid? userId = _currentUser.Id;
        Data.UserID = Convert.ToString(userId);
    }


    public class IndexPageModel
    {
        public string UserID { get; set; }
    }
}