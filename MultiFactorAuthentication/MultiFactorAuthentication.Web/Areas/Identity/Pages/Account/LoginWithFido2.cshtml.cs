using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MultiFactorAuthentication.Web.Areas.Identity.Pages.Account
{
    public class LoginWithFido2Model : PageModel
    {
    // private readonly SignInManager<IdentityUser> _signInManager;
    // private readonly ILogger<LoginWith2faModel> _logger;
    //
    // public LoginWithFido2Model(SignInManager<IdentityUser> signInManager, ILogger<LoginWith2faModel> logger)
    // {
    //   _signInManager = signInManager;
    //   _logger = logger;
    // }

    [BindProperty]
    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; }



    public void OnGet()
        {
        }
    }
}
