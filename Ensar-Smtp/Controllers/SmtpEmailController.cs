using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Ensar_Smtp.Models;
using System.Text.RegularExpressions;

namespace Ensar_Smtp.Controllers
{
    [Route("api/Smtp")]
    [ApiController]
    [EnableCors]
    public class SmtpEmailController : ControllerBase
    {
        private readonly SmtpConfiguration _configValue;
        public SmtpEmailController(SmtpConfiguration configValue)
        {
            _configValue= configValue;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("SmtpEmail")]
        public IActionResult SendEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest(new { message = "Email is Required." });
                }
                else if(!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    return BadRequest(new { message = "Invalid email format, please provide a valid email address." });
                }
                else
                {
                    string subject = "Test Email";
                    string body = "Test mail is sent.";
                    MailMessage mailMessage = new MailMessage(_configValue.fromEmail, email, subject, body);
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient(_configValue.host, _configValue.port);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = _configValue.enableSsl;
                    smtpClient.Credentials = new NetworkCredential(_configValue.username, _configValue.password);
                    smtpClient.Send(mailMessage);
                    return Ok(new { message = "Email sent successfully." });
                }
                
            }catch(Exception ex)
            {
                return BadRequest(new { message = "An exception has occurred:" +ex.Message });
                
            }
        }
    }
}
