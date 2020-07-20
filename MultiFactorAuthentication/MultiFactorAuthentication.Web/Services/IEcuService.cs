using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiFactorAuthentication.Web.Models;

namespace MultiFactorAuthentication.Web.Services
{
  /*
   * Interface the defines the functionality of the EcuService
   */
  public interface IEcuService
  {
    Ecu Create(Ecu newEcu);
    IEnumerable<Ecu> GetAll();
    Ecu GetById(int id);
    Ecu Update(Ecu updatedEcu);
    Ecu Delete(int id);

  }
}
