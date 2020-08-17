using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corregidor2020Back.models
{
    public class Email
    {
        public string to { get; set; }        
        [Required]
        public string from { get; set; }
        [Required]
        public string content { get; set; }       
        [Required]
        public string subject { get; set; }
        
        public string htmlContent { get; set; }
    }
}
