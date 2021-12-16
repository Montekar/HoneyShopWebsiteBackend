using System;
using System.Net;
using System.Net.Mail;
using HoneyShop.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShop.Domain.Service
{
    public class EmailService : IEmailService
    {
        public bool SendEmail([FromBody]string receiverEmail, string subject, string body)
        {
            Console.WriteLine(receiverEmail);
            Console.WriteLine(subject);
            Console.WriteLine(body);
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