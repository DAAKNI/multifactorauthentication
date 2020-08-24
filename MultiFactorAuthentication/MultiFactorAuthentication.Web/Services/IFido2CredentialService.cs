using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Development;
using MultiFactorAuthentication.Web.Models;

namespace MultiFactorAuthentication.Web.Services
{
/*
 * This interface defines the functionality of the service which adds and gets Fido2 Credential to a from a user.
 * It may be implemented by a in memory service or a database service.
 */
  public interface IFido2CredentialService
  {
    
    void AddCredential(Fido2Credential cred);
    Task<int> UpdateCounter(byte[] credentialId, uint counter);
    Fido2Credential GetCredentialByUser(ApplicationUser user);
    Task<List<Fido2Credential>> GetCredentialsByUser(ApplicationUser user);
    List<ApplicationUser> GetUsersByCredentialIdAsync(byte[] argsCredentialId);
    Task<Fido2Credential> AddCredentialToUser(Fido2Credential storedCredential);

    int Commit();
      Task<List<Fido2Credential>> GetCredentialsByUserHandleAsync(byte[] argsUserHandle);
  }
}
