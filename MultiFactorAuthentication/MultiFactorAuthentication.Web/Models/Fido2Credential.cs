using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Fido2NetLib.Development;
using Fido2NetLib.Objects;
using Newtonsoft.Json;

namespace MultiFactorAuthentication.Web.Models
{
  public class Fido2Credential 
  {
    public int Id { get; set; }
    public string UserId { get; set; }

   public byte[] Descriptor { get ; set; }
   public byte[] PublicKey { get; set; }
   public byte[] UserHandle { get; set; }
   public uint SignatureCounter { get; set; }
   public string CredType { get; set; }
   public DateTime RegDate { get; set; }
   public Guid AaGuid { get; set; }

  }
}
