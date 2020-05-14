using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiFactorAuthentication.Web.Models
{
  public enum TwoFactorType
  {
    None,
    Fido2,
    Authenticator,
    SMS
  }
}
