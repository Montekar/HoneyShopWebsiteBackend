using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoneyShop.Core.Models;
using HoneyShop.Security.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserAuthenticator _userAuthenticator;

        public RegisterController(IUserAuthenticator userAuthenticator)
        {
            _userAuthenticator = userAuthenticator;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegisterInput registerInput)
        {
            string username = registerInput.Username;
            string password = registerInput.Password;

            if (_userAuthenticator.CreateUser(username, password))
            {
                //Authentication succesful
                return Ok();
            }

            return Problem("Could not create user with name: " + username);
        }
    }
}