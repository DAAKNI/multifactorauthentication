using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiFactorAuthentication.Web.Models;

namespace MultiFactorAuthentication.Web.Services
{
  public interface IUserService
  {
    User Create(User newUser);
    IEnumerable<User> GetAll();
    User GetById(int id);
    User Update(User updatedUser);
    User Delete(int id);
  }
}
