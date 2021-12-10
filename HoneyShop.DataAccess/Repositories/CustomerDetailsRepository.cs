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
        private readonly HoneyDbContext _honeyDbContext;

        public CustomerDetailsRepository(HoneyDbContext honeyDbContext)
        {
            _honeyDbContext = honeyDbContext ?? throw new InvalidDataException("CustomerDetails Repository must have a DB context in constructor");
        }

        public List<CustomerDetails> GetAllCustomerDetails()
        {
            return _honeyDbContext.CustomerDetails.Select(pe => new CustomerDetails()
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
        
        public CustomerDetails CreateCustomerDetails(CustomerDetails customerDetails)
        {
            var customerDetailsEntity = new CustomerDetailsEntity()
            {
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
            var savedEntity = _honeyDbContext.CustomerDetails.Add(customerDetailsEntity).Entity;
            _honeyDbContext.SaveChanges();
            return new CustomerDetails
            {
                Id = savedEntity.Id,
                UserId = savedEntity.UserId,
                FirstName = savedEntity.FirstName,
                LastName = savedEntity.LastName,
                PhoneNumber = savedEntity.PhoneNumber,

                AddressCountry = savedEntity.AddressCountry,
                AddressCity = savedEntity.AddressCity,
                AddressPostCode = savedEntity.AddressPostCode,
                AddressStreet = savedEntity.AddressStreet,
                AddressNumber = savedEntity.AddressNumber
            };
        }

        public CustomerDetails UpdateCustomerDetails(CustomerDetails customerDetails)
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
            _honeyDbContext.Attach(customerDetailsEntity).State = EntityState.Modified;
                _honeyDbContext.SaveChanges();

                return new CustomerDetails
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
        }

        public CustomerDetails DeleteCustomerDetails(int id)
        {
            var entity = _honeyDbContext.CustomerDetails.FirstOrDefault(detailsEntity => detailsEntity.Id == id);
            if (entity == null)
            {
                return null;
            }
            var deletedEntity = _honeyDbContext.Remove(entity).Entity;
            _honeyDbContext.SaveChanges();
            return new CustomerDetails
            {
                Id = deletedEntity.Id,
                UserId = deletedEntity.UserId,
                FirstName = deletedEntity.FirstName,
                LastName = deletedEntity.LastName,
                PhoneNumber = deletedEntity.PhoneNumber,

                AddressCountry = deletedEntity.AddressCountry,
                AddressCity = deletedEntity.AddressCity,
                AddressPostCode = deletedEntity.AddressPostCode,
                AddressStreet = deletedEntity.AddressStreet,
                AddressNumber = deletedEntity.AddressNumber
            };
        }

        public CustomerDetails GetCustomerDetailsById(int id)
        {
            var customerDetailsById = _honeyDbContext.CustomerDetails.FirstOrDefault(customerDetails => id.Equals(customerDetails.Id));
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
                _honeyDbContext.CustomerDetails.Where(entity => userId.Equals(entity.UserId)).Select(entity => new CustomerDetails
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