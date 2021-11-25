using System.Collections.Generic;
using System.IO;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;

namespace HoneyShop.Domain.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new InvalidDataException("UserRepository Cannot Be Null");
        }
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public User CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        public User EditUser(User user)
        {
            return _userRepository.EditUser(user);
        }

        public User DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }
    }
}