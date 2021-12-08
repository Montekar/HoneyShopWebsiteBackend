using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess.Entities;
using HoneyShop.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HoneyShop.DataAccess.Repositories
{
    public class CustomerDetailsRepository: ICustomerDetailsRepository
    {
        private readonly HoneyContext _honeyContext;

        public CustomerDetailsRepository(HoneyContext honeyContext)
        {
            _honeyContext = honeyContext ?? throw new InvalidDataException("CustomerDetails Repository must have a DB context in constructor");
        }

        public List<CustomerDetails> GetAllCustomerDetails()
        {
            return _honeyContext.CustomerDetails.Select(pe => new CustomerDetails()
            {
                Id = pe.Id, 
                UserId = pe.UserId,
                FirstName = pe.FirstName,
                LastName = pe.LastName,
                PhoneNumber = pe.PhoneNumber,
                    
                AddressCountry = pe.AddressCountry,
                AddressCity = pe.AddressCity,
                AddressPostCode = pe.AddressPostCode,
                AddressStreet = pe.AddressStreet,
                AddressNumber = pe.AddressNumber
            }).ToList();
        }
        
        public bool CreateCustomerDetails(CustomerDetails customerDetails)
        {
            var customerDetailsEntity = new CustomerDetailsEntity()
            {
                Id = customerDetails.Id,
                FirstName = customerDetails.FirstName,
                LastName = customerDetails.LastName,
                PhoneNumber = customerDetails.PhoneNumber,

                AddressCountry = customerDetails.AddressCountry,
                AddressCity = customerDetails.AddressCity,
                AddressPostCode = customerDetails.AddressPostCode,
                AddressStreet = customerDetails.AddressStreet,
                AddressNumber = customerDetails.AddressNumber
            };
            _honeyContext.CustomerDetails.Attach(customerDetailsEntity).State = EntityState.Added;
            _honeyContext.SaveChanges();
            return true;
        }

        public bool UpdateCustomerDetails(CustomerDetails customerDetails)
        {
            var customerDetailsEntity = new CustomerDetailsEntity()
            {
                Id = customerDetails.Id,
                UserId = customerDetails.UserId,
                FirstName = customerDetails.FirstName,
                LastName = customerDetails.LastName,
                PhoneNumber = customerDetails.PhoneNumber,

                AddressCountry = customerDetails.AddressCountry,
                AddressCity = customerDetails.AddressCity,
                AddressPostCode = customerDetails.AddressPostCode,
                AddressStreet = customerDetails.AddressStreet,
                AddressNumber = customerDetails.AddressNumber
            };
            if (customerDetailsEntity != null)
            {
                _honeyContext.Attach(customerDetailsEntity).State = EntityState.Modified;
                _honeyContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteCustomerDetails(int id)
        {
            var customerDetailsToRemove = _honeyContext.CustomerDetails.Where(p => p.Id == id);
            if (customerDetailsToRemove != null)
            {
                _honeyContext.RemoveRange(customerDetailsToRemove);
                _honeyContext.SaveChanges();
                return true;
            }
            return false;
        }

        public CustomerDetails GetCustomerDetailsById(int id)
        {
            var customerDetailsById = _honeyContext.CustomerDetails.FirstOrDefault(customerDetails => id.Equals(customerDetails.Id));
            if (customerDetailsById != null)
            {
                return new CustomerDetails()
                {
                    Id = customerDetailsById.Id,
                    UserId = customerDetailsById.UserId,
                    FirstName = customerDetailsById.FirstName,
                    LastName = customerDetailsById.LastName,
                    PhoneNumber = customerDetailsById.PhoneNumber,

                    AddressCountry = customerDetailsById.AddressCountry,
                    AddressCity = customerDetailsById.AddressCity,
                    AddressPostCode = customerDetailsById.AddressPostCode,
                    AddressStreet = customerDetailsById.AddressStreet,
                    AddressNumber = customerDetailsById.AddressNumber
                };
            }

            return null;
        }
        
        public List<CustomerDetails> GetCustomerDetailsByUserId(int userId)
        {
            var customerDetailsById =
                _honeyContext.CustomerDetails.Where(entity => userId.Equals(entity.UserId)).Select(entity => new CustomerDetails
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    PhoneNumber = entity.PhoneNumber,

                    AddressCountry = entity.AddressCountry,
                    AddressCity = entity.AddressCity,
                    AddressPostCode = entity.AddressPostCode,
                    AddressStreet = entity.AddressStreet,
                    AddressNumber = entity.AddressNumber
                }).ToList();
            return customerDetailsById;
        }
    }
}