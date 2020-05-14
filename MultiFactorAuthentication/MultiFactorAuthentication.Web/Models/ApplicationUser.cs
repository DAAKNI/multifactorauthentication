using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Fido2NetLib.Objects;
using Microsoft.AspNetCore.Identity;

namespace MultiFactorAuthentication.Web.Models
{
  public class ApplicationUser : IdentityUser
  {
    public TwoFactorType TwoFactorMethod { get; set; }
    public ICollection<Fido2Credential> Fido2Credentials { get; set; }

  }
}
