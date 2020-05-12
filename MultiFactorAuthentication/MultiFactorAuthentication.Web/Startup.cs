using System;
using System.Collections.Generic;
using System.Linq;
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
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));
      services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
      services.AddControllersWithViews();
      services.AddControllers()
        .AddNewtonsoftJson();



      services.AddRazorPages(opts =>
      {
        // we don't care about antiforgery in the demo
        opts.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
      }).AddNewtonsoftJson();



      //services.AddSingleton<IEcuService, InMemoryEcuService>();
      //services.AddSingleton<IUserService, InMemoryUserService>();
      //services.AddFido2(options =>
      //{
      //  options.ServerDomain = Configuration["fido2:serverDomain"];
      //  options.ServerName = Configuration["fido2:serverName"];
      //  options.Origin = Configuration["fido2:origin"];
      //  options.TimestampDriftTolerance = Configuration.GetValue<int>("fido2:timestampDriftTolerance");
      //});

      services.AddFido2(options =>
        {
          options.ServerDomain = Configuration["fido2:serverDomain"];
          options.ServerName = "FIDO2 Test";
          options.Origin = Configuration["fido2:origin"];
          options.TimestampDriftTolerance = Configuration.GetValue<int>("fido2:timestampDriftTolerance");
          options.MDSAccessKey = Configuration["fido2:MDSAccessKey"];
          options.MDSCacheDirPath = Configuration["fido2:MDSCacheDirPath"];
        })
        .AddCachedMetadataService(config =>
        {
          //They'll be used in a "first match wins" way in the order registered
          config.AddStaticMetadataRepository();
          if (!string.IsNullOrWhiteSpace(Configuration["fido2:MDSAccessKey"]))
          {
            config.AddFidoMetadataRepository(Configuration["fido2:MDSAccessKey"]);
          }
        });

      services.AddFido2(options =>
        {
          options.ServerDomain = Configuration["fido2:serverDomain"];
          options.ServerName = "FIDO2 Test";
          options.Origin = Configuration["fido2:origin"];
          options.TimestampDriftTolerance = Configuration.GetValue<int>("fido2:timestampDriftTolerance");
        })
        .AddCachedMetadataService(config =>
        {
          //They'll be used in a "first match wins" way in the order registered
          config.AddStaticMetadataRepository();
          if (!string.IsNullOrWhiteSpace(Configuration["fido2:MDSAccessKey"]))
          {
            config.AddFidoMetadataRepository(Configuration["fido2:MDSAccessKey"]);
          }
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
