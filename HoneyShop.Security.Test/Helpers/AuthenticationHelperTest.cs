using HoneyShop.Security.Helpers;
using Moq;
using Xunit;

namespace HoneyShop.Security.Test.Helpers
{
    public class AuthenticationHelperTest
    {
        private readonly AuthenticationHelper _service;

        public AuthenticationHelperTest()
        {
            _service = new AuthenticationHelper();
        }
        
        [Fact]
        public void AuthenticationHelper_IsIAuthenticationHelper()
        {
            Assert.True(_service is IAuthenticationHelper);
        }
    }
}