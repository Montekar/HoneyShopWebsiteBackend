using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using HoneyShop.Core.IServices;

namespace HoneyShop.Domain.Service
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(string receiverEmail, string subject, string body)
        {
            if (receiverEmail == null || subject == null || body == null)
            {
                 throw new InvalidDataException("Receiver email, subject, body cannot be null");
            }

            var client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "honeyappreal@gmail.com",
                    Password = "mantas123"
                }
            };
            var fromEmail = new MailAddress("honeyappreal@gmail.com", "Honey WebShop");
            var toEmail = new MailAddress(receiverEmail, "Someone");
            var message = new MailMessage()
            {
                From = fromEmail,
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            message.To.Add(toEmail);

            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    }