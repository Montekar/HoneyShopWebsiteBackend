using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using HoneyShop.Core.IServices;
using HoneyShopWebsiteBackend.Dto.UserDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        
        private readonly IEmailService _service;

        public EmailController(IEmailService service)
        {
            _service = service ?? throw new InvalidDataException("Email service can not be null");
        }
        
        [HttpPost]
        public ActionResult<bool> CreateCustomerDetails(EmailDto emailDto)
        {
            if (emailDto.ReceiverEmail.IsNullOrEmpty() || emailDto.Subject.IsNullOrEmpty() || emailDto.Body.IsNullOrEmpty())
            {
                return BadRequest("Null or Empty");
            }
            return _service.SendEmail(emailDto.ReceiverEmail, emailDto.Subject, emailDto.Body);
        }
    }
}