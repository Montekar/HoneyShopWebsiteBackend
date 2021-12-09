using System;
using System.Security.Authentication;
using HoneyShop.Core.Models;
using HoneyShop.Security.IServices;
using HoneyShop.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public AuthController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<TokenDto> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = _securityService.GenerateJwtToken(dto.Email, dto.Password);

                return Ok(new TokenDto
                {
                    Jwt = token.Jwt,
                    Message = token.Message
                });
            }
            catch (AuthenticationException ae)
            {
                return Unauthorized(ae.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Please contact Admin");
            }
        }
        
        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public ActionResult<RegisteredDto> Register([FromBody] RegisterDto dto)
        {
            var user = _securityService.RegisterUser(dto.Email, dto.Password);
            if (user == null)
            {
                return Problem("Could not create user with email: " + dto.Email);;
            }
            return new RegisteredDto
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.HashedPassword
            };
        }

        public class TokenDto
        {
            public string Jwt { get; set; }
            public string Message { get; set; }
        }

        public class LoginDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class RegisterDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisteredDto
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}