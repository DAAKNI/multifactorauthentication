using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MultiFactorAuthentication.API.Models;
using MultiFactorAuthentication.API.Services;

namespace MultiFactorAuthentication.API.Controllers
{
  [Route("api/user")]
  [ApiController]
  public class UserController : ControllerBase
  {
    //private readonly UserManager<User> _userManager;
    //private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _config;
    private readonly IUserService _userService;
      
    public UserController(IUserService userService,
      //UserManager<User> userManager,
      //SignInManager<User> signInManager,
      IConfiguration config)
    {
      //_userManager = userManager;
      //_signInManager = signInManager;
      _config = config;
      _userService = userService;
    }
    [HttpPost("CreateToken")]
    public async Task<IActionResult> CreateToken(User _user)
    {
      //
      //var user = await _userManager.FindByNameAsync(_user.Name);

      var user = _userService.GetById(_user.Id);

      if (user == null)
      {
        return BadRequest();
      }

      if (user.Password == _user.Password)
      {
        var claims = new[]
        {
          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          _config["Tokens:Issuer"],
          _config["Tokens:Audience"],
          claims,
          expires: DateTime.UtcNow.AddMinutes(30),
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