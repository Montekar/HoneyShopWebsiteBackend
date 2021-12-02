using HoneyShop.Core.Models;
using HoneyShop.Security.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserAuthenticator _userAuthenticator;

        public LoginController(IUserAuthenticator userAuthenticator)
        {
            _userAuthenticator = userAuthenticator;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] LoginInput loginInput)
        {
            string userToken;
            if (_userAuthenticator.Login(loginInput.Email, loginInput.Password, out userToken))
            {
                //Authentication successful
                return Ok(new
                {
                    username = loginInput.Email,
                    token = userToken
                });
            }
            return Unauthorized("Unknown username and password combination");
        }
    }
}