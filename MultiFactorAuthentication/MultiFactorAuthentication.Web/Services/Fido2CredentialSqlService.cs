using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
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
    
    public Fido2Credential GetCredentialByUserId(int id)
    {
      return _db.Fido2Credentials.Find(id);
    }

    public void AddCredential(Fido2Credential cred)
    {
      _db.Add(cred);
    }

    public void UpdateCounter(byte[] credentialId, uint counter)
    {
      throw new NotImplementedException();
    }

    // public List<Fido2Credential> GetCredentialsByUser(ApplicationUser user)
    // {
    //   return _db.Fido2Credentials.Where(c => c.UserId.SequenceEqual(user.Id)).ToList();
    // }
   public IEnumerable<Fido2Credential> GetCredentialsByUser(ApplicationUser user)
   {
      var query = from r in _db.Fido2Credentials
      where r.UserId.StartsWith(user.Id) || string.IsNullOrEmpty(user.Id)
      select r;
     return query;
     
    }

    public List<ApplicationUser> GetUsersByCredentialIdAsync(byte[] argsCredentialId)
    {
      throw new NotImplementedException();
    }

    public async Task AddCredentialToUser(Fido2Credential newFido2Credential)
    {
      _db.Fido2Credentials.Add(newFido2Credential);
      await _db.SaveChangesAsync();
    }

    public int Commit()
    {
      return _db.SaveChanges();
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
