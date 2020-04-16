using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MultiFactorAuthentication.API.Services;

namespace MultiFactorAuthentication.API.Controllers
{
    [ApiController]
    [Route("api/ecus")]
    public class EcusController : ControllerBase
    {
        private readonly IEcuData ecuData;

        public EcusController(IEcuData ecuData)
        {
            this.ecuData = ecuData;

        }

        [HttpGet()]
        public IActionResult GetEcus()
        {
            var ecus = ecuData.GetAll();
            return new JsonResult(ecus);
        }
    }
}
