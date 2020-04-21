using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiFactorAuthentication.API.Models;

namespace MultiFactorAuthentication.API.Services
{
  public interface IEcuData
  {
    Ecu Create(Ecu newEcu);
    IEnumerable<Ecu> GetAll();
    Ecu GetById(int id);
    Ecu Update(Ecu updatedEcu);
    Ecu Delete(int id);

  }
}
