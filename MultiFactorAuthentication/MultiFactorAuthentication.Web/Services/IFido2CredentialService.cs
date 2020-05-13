using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Development;
using MultiFactorAuthentication.Web.Models;

namespace MultiFactorAuthentication.Web.Services
{
  public interface IFido2CredentialService
  {
    Fido2Credential GetCredentialByUserId(int id);
    void AddCredential(Fido2Credential cred);
    void UpdateCounter(byte[] credentialId, uint counter);
    IEnumerable<Fido2Credential> GetCredentialsByUser(ApplicationUser user);
    List<ApplicationUser> GetUsersByCredentialIdAsync(byte[] argsCredentialId);
    Task AddCredentialToUser(Fido2Credential storedCredential);

    int Commit();
  }
}
