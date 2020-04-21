using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiFactorAuthentication.API.Models;

namespace MultiFactorAuthentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

      [HttpPost]
      public IActionResult CreateToken(User user )
    }
}