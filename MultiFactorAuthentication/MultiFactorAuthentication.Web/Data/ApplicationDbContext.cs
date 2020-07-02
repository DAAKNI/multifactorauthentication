using System;
using System.Collections.Generic;
using System.Text;
using Fido2NetLib.Objects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiFactorAuthentication.Web.Models;

namespace MultiFactorAuthentication.Web.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite("Filename=app.db");

    public DbSet<Fido2Credential> Fido2Credentials { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      
      base.OnModelCreating(builder);
    }
  }
}
