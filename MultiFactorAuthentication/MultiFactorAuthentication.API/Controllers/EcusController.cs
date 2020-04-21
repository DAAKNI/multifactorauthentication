using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiFactorAuthentication.API.Models;
using MultiFactorAuthentication.API.Services;

namespace MultiFactorAuthentication.API.Controllers
{
  [ApiController]
  [Route("api/ecus")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class EcusController : ControllerBase
  {
    private readonly IEcuData ecuData;

    public EcusController(IEcuData ecuData)
    {
      this.ecuData = ecuData;

    }

    [HttpPost()]
    public IActionResult CreateEcu(Ecu newEcu)
    {
      var ecu = ecuData.Create(newEcu);
      return Ok(ecu);
    }

    [HttpGet()]
    public IActionResult GetEcus()
    {
      var ecus = ecuData.GetAll();
      return Ok(ecus);
    }

    [HttpGet("{ecuId}")]
    public IActionResult GetEcu(int ecuId)
    {
      var ecu = ecuData.GetById(ecuId);
      if (ecu == null)
      {
        return NotFound();
      }
      return Ok(ecu);
    }


    [HttpPut()]
    public ActionResult UpdateEcu(Ecu updatedEcu)
    {
      var ecu = ecuData.Update(updatedEcu);
      if (ecu == null)
      {
        return NotFound();
      }

      return Ok(ecu);
    }

    [HttpDelete("{ecuId}")]
    public ActionResult DeleteEcu(int ecuId)
    {
      var ecu = ecuData.Delete(ecuId);
      if (ecu == null)
      {
        return NotFound();
      }

      return Ok(ecu);
    }


  }
}
