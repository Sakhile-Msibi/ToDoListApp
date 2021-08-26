using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }  
    }
}
