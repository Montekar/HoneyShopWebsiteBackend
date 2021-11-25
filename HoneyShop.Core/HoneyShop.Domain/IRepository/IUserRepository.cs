using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Domain
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        User CreateUser(User user);
        User EditUser(User user);
        User DeleteUser(int id);
    }
}