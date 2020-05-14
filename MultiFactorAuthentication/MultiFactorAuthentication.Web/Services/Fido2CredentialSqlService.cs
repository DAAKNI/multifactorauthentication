using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using Fido2NetLib.Development;
using Fido2NetLib.Objects;
using Microsoft.EntityFrameworkCore;
using MultiFactorAuthentication.Web.Data;
using MultiFactorAuthentication.Web.Models;

namespace MultiFactorAuthentication.Web.Services
{
  public class Fido2CredentialSqlService : IFido2CredentialService
  {
    private readonly ApplicationDbContext _db;

    public Fido2CredentialSqlService(ApplicationDbContext db)
    {
      _db = db;
    }
    
    public Fido2Credential GetCredentialByUser(ApplicationUser user)
    {
      return _db.Fido2Credentials.FirstOrDefault(c => c.UserId == user.Id);
    }

    public void AddCredential(Fido2Credential cred)
    {
      _db.Add(cred);
    }

    public async Task<int> UpdateCounter(byte[] credentialId, uint counter)
    {
      var cred = _db.Fido2Credentials.FirstOrDefault(c => new PublicKeyCredentialDescriptor(c.Descriptor).Id.SequenceEqual(credentialId));
      cred.SignatureCounter = counter;
      await _db.SaveChangesAsync();
      return 0;

    }

    // public List<Fido2Credential> GetCredentialsByUser(ApplicationUser user)
    // {
    //   return _db.Fido2Credentials.Where(c => c.UserId.SequenceEqual(user.Id)).ToList();
    // }
   public async Task<List<Fido2Credential>> GetCredentialsByUser(ApplicationUser user)
   {
      //  var query = from r in _db.Fido2Credentials
      //  where r.UserId.StartsWith(user.Id) || string.IsNullOrEmpty(user.Id)
      //  select r;
      // return query;
      return await _db.Fido2Credentials.Where(c => c.UserId == user.Id).ToListAsync();
    }

    public List<ApplicationUser> GetUsersByCredentialIdAsync(byte[] argsCredentialId)
    {
      throw new NotImplementedException();
    }

    public async Task<Fido2Credential> AddCredentialToUser(Fido2Credential newFido2Credential)
    {
      _db.Fido2Credentials.Add(newFido2Credential);
      await _db.SaveChangesAsync();
      return newFido2Credential;
    }

    public int Commit()
    {
      return _db.SaveChanges();
    }

    public async Task<List<Fido2Credential>> GetCredentialsByUserHandleAsync(byte[] userHandle)
    {
      // return Task.FromResult(_db.Fido2Credentials.Where(c => c.UserHandle.SequenceEqual(userHandle)).ToList());
      return await _db.Fido2Credentials.Where(c => c.UserHandle == userHandle).ToListAsync();
    }

    // public List<ApplicationUser> GetUsersByCredentialIdAsync(byte[] argsCredentialId)
    // {
    //   // our in-mem storage does not allow storing multiple users for a given credentialId. Yours shouldn't either.
    //   var cred = _db.Fido2Credentials.Where(c => c.Descriptor.Id.SequenceEqual(credentialId)).FirstOrDefault();
    //
    //   if (cred == null)
    //     return Task.FromResult(new List<ApplicationUser>());
    //
    //   return Task.FromResult(_db.Fido2Credentials.Where(u => u.Value.Id.SequenceEqual(cred.UserId)).Select(u => u.Value).ToList());
    // }
  }
}
