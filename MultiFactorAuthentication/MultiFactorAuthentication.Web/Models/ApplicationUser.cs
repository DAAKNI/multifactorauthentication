using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fido2NetLib.Objects;
using Microsoft.AspNetCore.Identity;

namespace MultiFactorAuthentication.Web.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string TwoFactorMethod { get; set; }
    public PublicKeyCredentialDescriptor Descriptor { get; set; }
    public byte[] PublicKey { get; set; }
    public byte[] UserHandle { get; set; }
    public uint SignatureCounter { get; set; }
    public string CredType { get; set; }
    public DateTime RegDate { get; set; }
    public Guid AaGuid { get; set; }

  }
}
