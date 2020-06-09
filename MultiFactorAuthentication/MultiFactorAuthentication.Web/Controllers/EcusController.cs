using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiFactorAuthentication.Web.Models;
using MultiFactorAuthentication.Web.Services;

namespace MultiFactorAuthentication.Web.Controllers
{
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  //[Authorize]
  [Route("api/ecus")]
  public class EcusController : ControllerBase
  {
    private readonly IEcuService ecuData;

    public EcusController(IEcuService ecuData)
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
