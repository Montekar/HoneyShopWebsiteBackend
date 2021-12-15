using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HoneyShop.Core.IServices;
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
        public ActionResult<bool> CreateCustomerDetails(string receiverEmail, string subject, string body)
        {
            return _service.SendEmail(receiverEmail, subject, body);
        }
    }
}