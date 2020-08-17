using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corregidor2020Back.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Corregidor2020Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        string superSecretAddress = "sbogucki@mail.usf.edu"; 

        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is working");           
        }

        [Route("SendEmail")]
        public async Task<IActionResult> PostMessage(Email email, [FromHeader] bool captcha)
        {
            var apiKey = _configuration.GetValue<string>("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            

            if (ModelState.IsValid && captcha)
            {
                var from = new EmailAddress(email.from);
                var to = new EmailAddress(superSecretAddress);
                var subject = email.subject;
                var content = email.content;
                var htmlContent = "";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);
                var response = await client.SendEmailAsync(msg);
                return Ok("Email Sent!");
            }

            else if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            else
            {
                return BadRequest("Didn't Submit Captcha");
            }            
        }
    }
}