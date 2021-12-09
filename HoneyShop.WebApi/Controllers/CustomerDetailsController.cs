using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShopWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly ICustomerDetailsService _service;

        public CustomerDetailsController(ICustomerDetailsService service)
        {
            _service = service ?? throw new InvalidDataException("CustomerDetails service can not be null");
        }
        
        [HttpPost]
        public ActionResult<CustomerDetails> CreateCustomerDetails([FromBody] CustomerDetails customerDetails)
        {
            if (string.IsNullOrEmpty(customerDetails.FirstName))
            {
                return BadRequest("Name is required");
            }
            var createCustomerDetails = _service.CreateCustomerDetails(customerDetails);
            if (createCustomerDetails)
            {
                return Ok("CustomerDetails was created");
            }

            return BadRequest("CustomerDetails was not created");
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<CustomerDetails>> GetAll()
        {
            return _service.GetAllCustomerDetails();
        }

        [HttpPut]
        public ActionResult<CustomerDetails> UpdateCustomerDetails([FromBody] CustomerDetails customerDetails)
        {
            var updateCustomerDetails = _service.UpdateCustomerDetails(customerDetails);
            if (updateCustomerDetails)
            {
                return Ok("CustomerDetails was updated");
            }

            return BadRequest("CustomerDetails couldn't be updated");
        }

        [HttpDelete("{id}")]
        public ActionResult<CustomerDetails> DeleteCustomerDetails(int id)
        {
            var deletedCustomerDetails = _service.DeleteCustomerDetails(id);
            if (deletedCustomerDetails)
            {
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpGet("retrieveById/{id}")]
        public ActionResult<CustomerDetails> GetCustomerDetailsById(int id)
        {
            var customerDetailsById = _service.GetCustomerDetailsById(id);
            if (customerDetailsById != null)
            {
                return customerDetailsById;
            }

            return BadRequest("CustomerDetails was not found");
        }
        
        [HttpGet("retrieveByUserId/{userId}")]
        public ActionResult<List<CustomerDetails>> GetCustomerDetailsByUserId(int userId)
        {
            var customerDetailsByUserId = _service.GetCustomerDetailsByUserId(userId);

            if (customerDetailsByUserId != null)
            {
                return customerDetailsByUserId;
            }

            return BadRequest("CustomerDetails was not found");
        }
    }
}