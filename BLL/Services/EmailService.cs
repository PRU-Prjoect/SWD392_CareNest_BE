using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _config;
        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_config["Mail:Client"], _config.GetValue<int>("Mail:Port"))
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config["Mail:EMail"], _config["Mail:AppPassword"])
            };

            await client.SendMailAsync(
                new MailMessage(from: "nguyenngoctuananh3001@gmail.com", to: email, subject, message));
        }

    }
}

