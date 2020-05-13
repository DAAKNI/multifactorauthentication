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
  public class Fido2Credential : StoredCredential
  {
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Foo { get; set; }
    [NotMapped]
    public PublicKeyCredentialDescriptor Descriptor { get ; set; }

  }
}
