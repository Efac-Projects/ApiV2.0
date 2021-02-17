using System;
using System.Collections.Generic;
using System.Text;




namespace App.Shared
{
    public class UserManagerResponse
    {
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }    

    }
}
