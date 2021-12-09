using EntityFrameworkCore.Testing.Moq;
using HoneyShop.Core.Models;
using HoneyShop.DataAccess;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HoneyShop.Infrastructure.Test
{
    public class HoneyContextTest
    {
        [Fact]
        public void UserRepository_IsIUserRepository()
        {
            var mockedDbContext = Create.MockedDbContextFor<HoneyDbContext>();
            Assert.NotNull(mockedDbContext);
        }

        [Fact]
        public void HoneyContext_DbSets_MustHaveDbSetWithTypeUser()
        {
            var mockedDbContext = Create.MockedDbContextFor<HoneyDbContext>();
            Assert.True(mockedDbContext.Users is DbSet<UserEntity>);
        }
    }
}