using HoneyShop.Security.Helpers;
using Moq;
using Xunit;

namespace HoneyShop.Security.Test.Helpers
{
    public class InterfaceAuthenticationHelper
    {
        private readonly Mock<IAuthenticationHelper> _service;

        public InterfaceAuthenticationHelper()
        {
            _service = new Mock<IAuthenticationHelper>();
        }
        
        [Fact]
        public void IAuthenticationHelper_Exists()
        {
            Assert.NotNull(_service.Object);
        }

        //Needs fix
        [Fact]
        public void IAuthenticationHelper_CreatePasswordHash()
        {
            var password = "password";
            byte[] passwordHash;
            byte[] passwordSalt;
            _service.Setup(ah =>
                ah.CreatePasswordHash( password, out passwordHash, out passwordSalt));
        }

        [Fact]
        public void IAuthenticationHelper_VerifyPasswordHash_ReturnBoolean()
        {
            var password = "password";
            byte[] passwordHash = null;
            byte[] passwordSalt = null;
            var expected = true;
            _service.Setup(ah =>
                ah.VerifyPasswordHash(password,  passwordHash,  passwordSalt))
                .Returns(expected);
            Assert.Equal(expected, _service.Object.VerifyPasswordHash(password, passwordHash,passwordSalt));
        }
        

        //Needs adjustment when we have users
        [Fact]
        public void IAuthenticationHelper_GenerateToken_ReturnString()
        {
            var expected = "token";
            _service.Setup(ah => ah.GenerateToken(null))
                .Returns(expected);
            Assert.Equal(expected, _service.Object.GenerateToken(null));

        }
    }
}