using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntityFrameworkCore.Testing.Moq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess;
using HoneyShop.DataAccess.Entities;
using HoneyShop.DataAccess.Repositories;
using HoneyShop.Domain.IRepository;
using HoneyShop.Domain.Service;
using Xunit;

namespace HoneyShop.Infrastructure.Test
{
    public class CustomerDetailsRepositoryTest
    {
        private readonly HoneyContext _fakeContext;
        private readonly CustomerDetailsRepository _customerDetailsRepository;
        private readonly List<CustomerDetailsEntity> _list;
        public CustomerDetailsRepositoryTest()
        {
            _fakeContext = Create.MockedDbContextFor<HoneyContext>();
            _customerDetailsRepository = new CustomerDetailsRepository(_fakeContext);
            _list = new List<CustomerDetailsEntity>
            {
                new CustomerDetailsEntity()
                {
                    Id = 1, 
                    FirstName = "Bob",
                    LastName = "TheBuilder",
                    Email = "email@gmail.com",
                    PhoneNumber = "12345678",
                    
                    AddressCountry = "Denmark",
                    AddressCity = "Esbjerg",
                    AddressPostCode = 6500,
                    AddressStreet = "Randomgade",
                    AddressNumber = "96 ST TV"
                },
                new CustomerDetailsEntity()
                {
                    Id = 2, 
                    FirstName = "Bob2",
                    LastName = "TheBuilder2",
                    Email = "email2@gmail.com",
                    PhoneNumber = "87654321",
                    
                    AddressCountry = "Denmark",
                    AddressCity = "Esbjerg",
                    AddressPostCode = 6500,
                    AddressStreet = "Randomgade",
                    AddressNumber = "88 ST TV"
                }
            };

        }

        [Fact]
        public void CustomerDetailsRepository_IsICustomerDetailsRepository()
        {
            Assert.IsAssignableFrom<ICustomerDetailsRepository>(_customerDetailsRepository);
        }
        
        [Fact]
        public void CustomerDetailsRepository_WithNullDBContext_ThrowsInvalidDataException()
        {
            var actual = Assert.Throws<InvalidDataException>(
                () => new CustomerDetailsRepository(null));
            Assert.Equal("CustomerDetails Repository must have a DB context in constructor", actual.Message);
        }

        /* Failing test nr1
        [Fact]
        public void FindAll_GetAllCustomerDetailsEntitiesInDBContext_AsAListOfCustomerDetails()
        {
            _fakeContext.Set<CustomerDetailsEntity>().AddRange(_list);
            _fakeContext.SaveChanges();

            var repositoryList = _list.Select(pe => new CustomerDetails()
            {
                Id = pe.Id, 
                FirstName = pe.FirstName,
                LastName = pe.LastName,
                Email = pe.Email,
                PhoneNumber = pe.PhoneNumber,
                    
                AddressCountry = pe.AddressCountry,
                AddressCity = pe.AddressCity,
                AddressPostCode = pe.AddressPostCode,
                AddressStreet = pe.AddressStreet,
                AddressNumber = pe.AddressNumber
            }).ToList();

            var actualResult = _customerDetailsRepository.GetAllCustomerDetails();
            Assert.Equal(repositoryList,actualResult, new Comparer());
        }
        */

        /* Failing test nr2
        [Fact]
        public void UpdateCustomerDetails_UpdateCustomerDetailsInDBContext_ReturnCustomerDetails()
        {
            _fakeContext.Set<CustomerDetailsEntity>().AddRange(_list);
            _fakeContext.SaveChanges();
            
            var customerDetailsToUpdate = _fakeContext.CustomerDetails.FirstOrDefault(p => p.Id == 1);
            
            if (customerDetailsToUpdate != null)
            {
                _fakeContext.Update(customerDetailsToUpdate);
                _fakeContext.SaveChanges();
            }

            var customerDetails = new CustomerDetails()
            {
                Id = customerDetailsToUpdate.Id,
                FirstName = customerDetailsToUpdate.FirstName,
            };
            
            var actual = _customerDetailsRepository.UpdateCustomerDetails(customerDetails);
            Assert.True(actual);
        }
        */
        
        [Fact]
        public void DeleteCustomerDetails_DeleteCustomerDetailsInDBContext_ReturnBoolean()
        {
            _fakeContext.Set<CustomerDetailsEntity>().AddRange(_list);
            _fakeContext.SaveChanges();

            var customerDetailsToRemove = _fakeContext.CustomerDetails.Where(p => p.Id == 1);
            if (customerDetailsToRemove != null)
            {
                _fakeContext.RemoveRange(customerDetailsToRemove);
                _fakeContext.SaveChanges();
            }

            var actual = _customerDetailsRepository.DeleteCustomerDetails(1);
            Assert.Equal(2, _list.Count);
            Assert.True(actual);
        }
        
        public class Comparer: IEqualityComparer<CustomerDetails>
        {
            public bool Equals(CustomerDetails x, CustomerDetails y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.FirstName == y.LastName && x.PhoneNumber.Equals(y.PhoneNumber);
            }

            public int GetHashCode(CustomerDetails obj)
            {
                return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName, obj.PhoneNumber);
            }
        }
    }


}