using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class ConfirmAppoinment
    {
        public string Message { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
