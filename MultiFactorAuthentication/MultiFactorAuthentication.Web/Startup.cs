using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Development;
using Fido2NetLib.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiFactorAuthentication.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MultiFactorAuthentication.Web.Models;
using MultiFactorAuthentication.Web.Services;
namespace MultiFactorAuthentication.Web
{
  public class Startup
  {

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }



    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {


      services.AddDistributedMemoryCache();
      services.AddSession(options =>
      {
        // Set a short timeout for easy testing.
        options.IdleTimeout = TimeSpan.FromMinutes(2);
        options.Cookie.HttpOnly = true;
        // Strict SameSite mode is required because the default mode used
        // by ASP.NET Core 3 isn't understood by the Conformance Tool
        // and breaks conformance testing
        //options.Cookie.SameSite = SameSiteMode.Strict;
      });



      // Add SQLITE as DB for persisting ApplicationUsers and Fido2Credentials
      services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>();

      // Setting up the IdentityFramework with ApplicationUser as default Usertype
      services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedEmail = false)
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

      // Setting up Authentication and JSON Web Token fun
      services
        .AddAuthentication()
        .AddCookie()
        .AddJwtBearer(cfg =>
        {
          cfg.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidIssuer = Configuration["Tokens:Issuer"],
            ValidAudience = Configuration["Tokens:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
          };
        });

      
      services.AddControllersWithViews();
      services.AddControllers()
        .AddNewtonsoftJson();
      services.AddHttpContextAccessor();


      services.AddRazorPages(opts =>
      {
        opts.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
      }).AddNewtonsoftJson();


      // Add an in Memory Database for the EcuService (api/ecus/)
      services.AddSingleton<IEcuService, InMemoryEcuService>();

      // Setup the Fido2CredentialService to persist Credentials to the Database
      services.AddScoped<IFido2CredentialService, Fido2CredentialSqlService>();

      // Setup Fido2
      services.AddFido2(options =>
      {
        options.ServerDomain = Configuration["fido2:serverDomain"];
        options.ServerName = "FIDO2 Test";
        options.Origin = Configuration["fido2:origin"];
        options.TimestampDriftTolerance = Configuration.GetValue<int>("fido2:timestampDriftTolerance");
      });





    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseSession();
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllers();
        endpoints.MapRazorPages();
      });
    }
  }
}
