using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MultiFactorAuthentication.Web.Models;

namespace MultiFactorAuthentication.Web.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger,
      SignInManager<ApplicationUser> signInManager,
      UserManager<ApplicationUser> userManager,
      IConfiguration config,
      IHttpContextAccessor httpContextAccessor)
    {
      _logger = logger;
      _signInManager = signInManager;
      _userManager = userManager;
      _config = config;
      _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
      return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpPost("CreateToken")]
    [Route("api/token")]
    public async Task<IActionResult> CreateToken()
    {



      if (_signInManager.IsSignedIn(User))


      {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = await _userManager.FindByIdAsync(userId);
        var claims = new[]
        {
          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          _config["Tokens:Issuer"],
          _config["Tokens:Audience"],
          claims,
          expires: DateTime.UtcNow.AddMinutes(15),
          signingCredentials: credentials
        );
        var results = new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token),
          expiration = token.ValidTo
        };
        return Created("", results);
      }
      else
      {
        return BadRequest();
      }

      return BadRequest();
    }
  }
}
