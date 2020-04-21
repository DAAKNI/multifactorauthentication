using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiFactorAuthentication.API.Models;

namespace MultiFactorAuthentication.API.Services
{

  public class InMemoryEcuData : IEcuData
  {
    List<Ecu> ecus;

    public InMemoryEcuData()
    {
      ecus = new List<Ecu>()
            {
                new Ecu() {Id = 1, Type = "Door control unit"},
                new Ecu() {Id = 2, Type = "Engine control unit"},
                new Ecu() {Id = 3, Type = "Electric power steering control unit"},
                new Ecu() {Id = 4, Type = "Human-machine interface"}
            };
    }

    public Ecu Create(Ecu newEcu)
    {
      newEcu.Id = ecus.Max(r => r.Id) + 1;
      ecus.Add(newEcu);
      return newEcu;
    }

    public IEnumerable<Ecu> GetAll()
    {
      return ecus;
    }

    public Ecu GetById(int id)
    {
      return ecus.SingleOrDefault(e => e.Id == id);
    }

    public Ecu Update(Ecu updatedEcu)
    {
      var ecu = ecus.FirstOrDefault(e => e.Id == updatedEcu.Id);
      if (ecu != null)
      {
        ecu.Type = updatedEcu.Type;
        ecu.Description = updatedEcu.Description;
      }

      return ecu;

    }

    public Ecu Delete(int id)
    {
      var ecu = ecus.FirstOrDefault(e => e.Id == id);
      if (ecu != null)
      {
        ecus.Remove(ecu);
      }

      return ecu;
    }
  }
}
