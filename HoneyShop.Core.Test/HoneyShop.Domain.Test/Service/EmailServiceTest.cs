using System.IO;
using HoneyShop.Core.IServices;
using HoneyShop.Domain.Service;
using Moq;
using Xunit;

namespace HoneyShop.Domain.Test.Service
{
    public class EmailServiceTest
    {
        private readonly EmailService _service;
        private readonly Mock<IEmailService> _mock;

        public EmailServiceTest()
        {
            _mock = new Mock<IEmailService>();
            _service = new EmailService();
        }
        
        [Fact]
        public void EmailService_Exists()
        {
            Assert.True(_service is not null);
        }
        
        [Fact]
        public void SendEmail_WithNullParameters_ThrowsInvalidDataException()
        {
            var exception = Assert.Throws<InvalidDataException>(
                () => _service.SendEmail(null,null,null));
            Assert.Equal("Receiver email, subject, body cannot be null", exception.Message);
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
            bool actual = _service.SendEmail(email, subject, body);
            Assert.Equal(expected, actual);
        }
    }
}