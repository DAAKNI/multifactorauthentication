using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MultiFactorAuthentication.API.Services;

namespace MultiFactorAuthentication.API
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
      //services.AddIdentity<StoreUser, IdentityRole>(config => { config.User.RequireUniqueEmail = true; })
      // .AddEntityFrameWorkStores<DutchContext>();


      // Conf
      services.AddControllersWithViews();
      services.AddRazorPages();

      services.AddAuthentication()
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
      services.AddSingleton<IEcuService, InMemoryEcuService>();
      services.AddSingleton<IUserService, InMemoryUserService>();
      //services.AddScoped<IEcuData, InMemoryEcuData>();
      services.AddControllers(setupAction =>
      {
        setupAction.ReturnHttpNotAcceptable = true;

      }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // Conf
      app.UseHttpsRedirection();
      app.UseStaticFiles();


      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        //endpoints.MapControllers();
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });
    }
  }
}
