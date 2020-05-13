﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Development;
using Fido2NetLib.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiFactorAuthentication.Web.Data;
using MultiFactorAuthentication.Web.Models;
using MultiFactorAuthentication.Web.Services;


namespace MultiFactorAuthentication.Web.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class Fido2Controller : Controller
    {
      
      private readonly IFido2 _fido2;
      private readonly IFido2CredentialService _fido2CredentialService;
      private readonly UserManager<ApplicationUser> _userManager;
      public static IMetadataService _mds;
      public static readonly DevelopmentInMemoryStore DemoStorage = new DevelopmentInMemoryStore();


    public Fido2Controller(
      UserManager<ApplicationUser> userManager,
      IFido2 fido2,
      IFido2CredentialService fido2CredentialService
      )
    {
      _fido2 = fido2;
      _fido2CredentialService = fido2CredentialService;
      _userManager = userManager;
    }

    private string FormatException(Exception e)
    {
      return string.Format("{0}{1}", e.Message, e != null ? "\n\n Verbose:\n (" + e + ")" : "");
    }

    [HttpPost]
    [Route("/makeCredentialOptions")]
    public async Task<JsonResult> MakeCredentialOptions([FromForm] string username,
                                      [FromForm] string displayName,
                                      [FromForm] string attType,
                                      [FromForm] string authType,
                                      [FromForm] bool requireResidentKey,
                                      [FromForm] string userVerification)
    {
      try
      {

        //if (string.IsNullOrEmpty(username))
        //{
        //  username = $"{displayName} (Usernameless user created at {DateTime.UtcNow})";
        //}

        
        var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

         // 1. Get user from DB by username (in our example, auto create missing users)
        var user = new Fido2User
        {
          Id = Encoding.UTF8.GetBytes(applicationUser.Id) // byte representation of userID is required
        };

        // 2. Get user existing keys by username
        // var existingKeys = DemoStorage.GetCredentialsByUser(user).Select(c => c.Descriptor).ToList();
        var existingKeys = new List<PublicKeyCredentialDescriptor>();
          //_fido2CredentialService.GetCredentialsByUser(applicationUser).Select(c => c.Descriptor).ToList();

        // 3. Create options
        var authenticatorSelection = new AuthenticatorSelection
        {
          RequireResidentKey = requireResidentKey,
          UserVerification = userVerification.ToEnum<UserVerificationRequirement>()
        };

        if (!string.IsNullOrEmpty(authType))
          authenticatorSelection.AuthenticatorAttachment = authType.ToEnum<AuthenticatorAttachment>();

        var exts = new AuthenticationExtensionsClientInputs()
        {
          Extensions = true,
          UserVerificationIndex = true,
          Location = true,
          UserVerificationMethod = true,
          BiometricAuthenticatorPerformanceBounds = new AuthenticatorBiometricPerfBounds
          {
            FAR = float.MaxValue,
            FRR = float.MaxValue
          }
        };

        var options = _fido2.RequestNewCredential(user, existingKeys, authenticatorSelection, attType.ToEnum<AttestationConveyancePreference>(), exts);

        // 4. Temporarily store options, session/in-memory cache/redis/db
        HttpContext.Session.SetString("fido2.attestationOptions", options.ToJson());

        // 5. return options to client
        return Json(options);
      }
      catch (Exception e)
      {
        return Json(new CredentialCreateOptions { Status = "error", ErrorMessage = FormatException(e)});
      }
    }


    [HttpPost]
    [Route("/register")]
    public async Task<JsonResult> MakeCredential([FromBody] AuthenticatorAttestationRawResponse attestationResponse)
    {
      try
      {
        // 1. get the options we sent the client
        var jsonOptions = HttpContext.Session.GetString("fido2.attestationOptions");
        var options = CredentialCreateOptions.FromJson(jsonOptions);

        // 2. Create callback so that lib can verify credential id is unique to this user
        // IsCredentialIdUniqueToUserAsyncDelegate callback = async (IsCredentialIdUniqueToUserParams args) =>
        // {
        //   // var users = await DemoStorage.GetUsersByCredentialIdAsync(args.CredentialId);
        //   var users = await _fido2CredentialService.GetUsersByCredentialIdAsync(args.CredentialId);
        //   if (users.Count > 0)
        //     return false;
        //
        //   return true;
        // };
        IsCredentialIdUniqueToUserAsyncDelegate callback = async (IsCredentialIdUniqueToUserParams args) =>
        {
          // #TODO Check if credentials are unique
          return true;
        };

        // 2. Verify and make the credentials
        var success = await _fido2.MakeNewCredentialAsync(attestationResponse, options, callback);

        
        var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

        // Schreibe die Credentials in die Datenbank
        await _fido2CredentialService.AddCredentialToUser(new Fido2Credential()
        {
           UserId = applicationUser.Id,
           Descriptor = new PublicKeyCredentialDescriptor(success.Result.CredentialId),
           PublicKey = success.Result.PublicKey,
           UserHandle = success.Result.User.Id,
           SignatureCounter = success.Result.Counter,
           CredType = success.Result.CredType,
           RegDate = DateTime.Now,
           AaGuid = success.Result.Aaguid
        });

        
        // #TODO Return Databse Entry not just JSON


        // 4. return "ok" to the client
        return new JsonResult(success);
      }
      catch (Exception e)
      {
        return new JsonResult(new Fido2.CredentialMakeResult { Status = "error", ErrorMessage =  FormatException(e) });
      }
    }
  }
}