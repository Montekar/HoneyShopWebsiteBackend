using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntityFrameworkCore.Testing.Moq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess;
using HoneyShop.DataAccess.Repositories;
using HoneyShop.Domain;
using HoneyShop.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HoneyShop.Infrastructure.Test
{
    public class UserRepositoryTest
    {
        [Fact]
        public void UserRepository_IsIUserRepository()
        {
            var fakeContext = Create.MockedDbContextFor<HoneyContext>();
            var repository = new UserRepository(fakeContext);
            Assert.IsAssignableFrom<IUserRepository>(repository);
        }

        [Fact]
        public void UserRepository_WithNullDbContext_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new UserRepository(null));
        }

        [Fact]
        public void UserRepository_WithNullDbContext_ThrowsExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() => new UserRepository(null));
            Assert.Equal("User Repository Must Have a HoneyContext", exception.Message);
        }

        [Fact]
        public void GetAll_GetAllUserEntitiesFromDbContext_ReturnListOfUsers()
        {
            var fakeContext = Create.MockedDbContextFor<HoneyContext>();
            var repository = new UserRepository(fakeContext);
            var list = new List<UserEntity>
            {
                new UserEntity
                {
                    Id = 1,
                    Username = "ExpectedUsername1",
                    Role = "ExpectedRole1"
                },
                new UserEntity
                {
                    Id = 2,
                    Username = "ExpectedUsername2",
                    Role = "ExpectedRole2"
                },
                new UserEntity
                {
                    Id = 3,
                    Username = "ExpectedUsername3",
                    Role = "ExpectedRole3"
                }
            };
            fakeContext.Set<UserEntity>().AddRange(list);
            fakeContext.SaveChanges();
            var expectedList = list.Select(ue => new User
            {
                Id = ue.Id,
                Role = ue.Role,
                Email = ue.Username,
                PasswordHash = ue.PasswordHash,
                PasswordSalt = ue.PasswordSalt
            }).ToList();

            var actualResult = repository.GetAllUsers();
            
            Assert.Equal(expectedList,actualResult,new Comparer());
        }

        [Fact]
        public void GetUser_GetUserEntityByIdFromDbContext_ReturnUser()
        {
            var fakeContext = Create.MockedDbContextFor<HoneyContext>();
            var repository = new UserRepository(fakeContext);
            var userEntity = new UserEntity
            {
                Id = 1,
                Username = "ExpectedUsername1",
                Role = "ExpectedRole1"
            };
            fakeContext.Set<UserEntity>().Add(userEntity);
            fakeContext.SaveChanges();
            var expectedUser = new User
            {
                Id = 1,
                Email = "ExpectedUsername1",
                Role = "ExpectedRole1"
            };
            var actualUser = repository.GetUser(expectedUser.Id);
            Assert.Equal(expectedUser,actualUser,new Comparer());
        }
        [Fact]
        public void CreateUser_CreateUserInDbContext_ReturnUser()
        {
            var fakeContext = Create.MockedDbContextFor<HoneyContext>();
            var repository = new UserRepository(fakeContext);
            
            var expectedUser = new User
            {
                Id = 1,
                Email = "ExpectedUsername1",
                Role = "ExpectedRole1"
            };
            var actualUser = repository.CreateUser(expectedUser);
            Assert.Equal(expectedUser,actualUser,new Comparer());
        }
        
        [Fact]
        public void DeleteUser_DeleteUserInDbContext_ReturnUser()
        {
            var fakeContext = Create.MockedDbContextFor<HoneyContext>();
            var repository = new UserRepository(fakeContext);
            var fakeId = 1;
            var userEntity = new UserEntity
            {
                Id = fakeId,
                Username = "ExpectedUsername1",
                Role = "ExpectedRole1"
            };
            fakeContext.Set<UserEntity>().Add(userEntity);
            fakeContext.SaveChanges();
            
            var expectedUser = new User
            {
                Id = fakeId,
                Email = "ExpectedUsername1",
                Role = "ExpectedRole1"
            };
            
            var actualUser = repository.DeleteUser(fakeId);
            Assert.Equal(expectedUser,actualUser,new Comparer());
        }
        
        [Fact]
        public void EditUser_EditUserInDbContext_ReturnUser()
        {
            var fakeContext = Create.MockedDbContextFor<HoneyContext>();
            var repository = new UserRepository(fakeContext);
            var fakeId = 1;
            var userEntity = new UserEntity
            {
                Id = fakeId,
                Username = "ExpectedUsername1",
                Role = "ExpectedRole1"
            };
            fakeContext.Set<UserEntity>().Add(userEntity);
            fakeContext.SaveChanges();
            
            var expectedUser = new User
            {
                Id = userEntity.Id,
                Email = userEntity.Username,
                Role = userEntity.Role
            };
            var actualUser = repository.EditUser(expectedUser);
            Assert.Equal(expectedUser,actualUser,new Comparer());
        }
        
        public class Comparer:IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Email == y.Email && Equals(x.PasswordHash, y.PasswordHash) && Equals(x.PasswordSalt, y.PasswordSalt) && x.Role == y.Role;
            }

            public int GetHashCode(User obj)
            {
                return HashCode.Combine(obj.Id, obj.Email, obj.PasswordHash, obj.PasswordSalt, obj.Role);
            }
        }
    }
}