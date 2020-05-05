using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiFactorAuthentication.Web.Models;
using MultiFactorAuthentication.Web.Services;

namespace MultiFactorAuthentication.Web.Services
{
  public class InMemoryUserService : IUserService
  {
    List<User> users;
    public InMemoryUserService()
    {
      {
        users = new List<User>()
        {
          new User() {Id = 1, Name = "Dan", Email = "daakni@gmail.com", Password = "secret"},
        };
      }
    }
    public User Create(User newUser)
    {
      newUser.Id = users.Max(r => r.Id) + 1;
      users.Add(newUser);
      return newUser; ;
    }

    public IEnumerable<User> GetAll()
    {
      return users;
    }

    public User GetById(int id)
    {
      return users.SingleOrDefault(u => u.Id == id);
    }

    public User Update(User updatedUser)
    {
      var user = users.FirstOrDefault(e => e.Id == updatedUser.Id);
      if (user != null)
      {
        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
      }

      return user;
    }

    public User Delete(int id)
    {
      var user = users.FirstOrDefault(e => e.Id == id);
      if (user != null)
      {
        users.Remove(user);
      }

      return user;
    }
  }
}
