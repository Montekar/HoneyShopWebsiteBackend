using System.IO;
using HoneyShop.Core.IServices;
using HoneyShop.Domain.Service;
using Moq;
using Xunit;

namespace HoneyShop.Core.Test.IServices
{
    public class IEmailServiceTest
    {
        private readonly Mock<IEmailService> _mock;

        public IEmailServiceTest()
        {
            _mock = new Mock<IEmailService>();
        }
        
        [Fact]
        public void IEmailService_Exists()
        {
            Assert.NotNull(_mock.Object);
        }

        [Fact]
        public void SendEmail_Returns_Bool()
        {
            bool expected = true;
            string email = "user@gmail.com";
            string subject = "subject";
            string body = "body";
            _mock.Setup(r => r.SendEmail(email,subject,body))
                .Returns(expected);
            bool actual = _mock.Object.SendEmail(email, subject, body);
            Assert.Equal(expected, actual);
        }
    }
}