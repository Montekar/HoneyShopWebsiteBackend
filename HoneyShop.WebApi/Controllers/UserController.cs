using System.Collections.Generic;
using System.IO;
using System.Linq;
using Castle.Core.Internal;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using HoneyShopWebsiteBackend.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            if (userService == null)
            {
                throw new InvalidDataException("UserService Cannot Be Null");
            }

            _userService = userService;
        }
        
        [HttpGet]
        public ActionResult<AllUsersDto> GetAllUsers()
        {
            var list = _userService.GetAllUsers();
            if (list.IsNullOrEmpty())
            {
                return BadRequest("List is null or empty");
            }

            return Ok(new AllUsersDto
            {
                List = list.Select(user => new UserDto
                {
                    Id = user.Id,
                    Role = user.Role,
                    Username = user.Username
                }).ToList()
            });
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID has to be 1 or above");
            }

            var user = _userService.GetUser(id);
            
            if (user == null)
            {
                return BadRequest("User not found");
            }
            
            return Ok(new UserDto {Id = user.Id, Role = user.Role, Username = user.Username});
        }
        [HttpPost]
        public ActionResult<UserDto> CreateUser(UserDto user)
        {
            var createdUser = _userService.CreateUser(new User{Id = user.Id,Role = user.Role,Username = user.Username});
            return Ok(new UserDto {Id = createdUser.Id, Role = createdUser.Role, Username = createdUser.Username});
        }
        
        [HttpPut("{id}")]
        public ActionResult<UserDto> EditUser(int id, [FromBody] UserDto userDto)
        {
            if (id < 1)
            {
                return BadRequest("ID has to be 1 or above");
            }
            if(id != userDto.Id)
            {
                return BadRequest("Pet ID must be the same or ID");
            }

            var editedUser = _userService.EditUser(new User
            {
                Id = userDto.Id,
                Role = userDto.Role,
                Username = userDto.Username
            });
            
            if (editedUser == null)
            {
                return BadRequest("User not found");
            }
            
            
            return Ok(new UserDto {Id = editedUser.Id, Role = editedUser.Role, Username = editedUser.Username});
        }
        
        [HttpDelete("{id}")]
        public ActionResult<UserDto> DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID has to be 1 or above");
            }
            User deletedUser = _userService.DeleteUser(id);
            if (deletedUser == null)
            {
                return BadRequest("User not found");
            }
            return Ok(new UserDto {Id = deletedUser.Id,Role = deletedUser.Role,Username = deletedUser.Username});
        }
    }
}