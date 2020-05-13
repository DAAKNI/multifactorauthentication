using System;
using System.Collections.Generic;
using System.Text;
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

    public DbSet<Fido2Credential> Fido2Credentials { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      //builder.Entity<Fido2Credential>().HasKey(m => m.Id);
      base.OnModelCreating(builder);
    }
  }
}
