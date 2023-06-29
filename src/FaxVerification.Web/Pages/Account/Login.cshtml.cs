using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Account.Settings;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Volo.Abp.Account.Web;
using Volo.Abp.Account.Web.Pages.Account;

namespace FaxVerification.Web.Pages.Account
{
    public class CustomLoginModel : LoginModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public CustomLoginModel(
        Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemeProvider,
        Microsoft.Extensions.Options.IOptions<Volo.Abp.Account.Web.AbpAccountOptions> accountOptions,
        Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> identityOpetions,
        SignInManager<IdentityUser> signInManager)
            : base(schemeProvider, accountOptions, identityOpetions)
        {
            _signInManager = signInManager;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                //var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                //if (result.Succeeded)
                //{
                //    // User authentication successful, redirect to the desired page
                //    return LocalRedirect(returnUrl ?? "/");
                //}
                //if (result.IsLockedOut)
                //{
                //    // Handle account lockout
                //}
                //else
                //{
                //    // Handle invalid login attempt
                //}
            }

            // If we reach here, the login failed, show the login page again with error messages
            return Page();
        }



    }
}
