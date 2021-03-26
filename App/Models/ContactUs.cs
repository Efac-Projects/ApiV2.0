using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class ContactUs
    {
        [Key]
        public int ContactId { get; set; }
        public string FullName { get; set; }
        public Guid BusinessId { get; set; }
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
