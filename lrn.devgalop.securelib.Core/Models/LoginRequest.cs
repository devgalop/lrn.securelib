using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lrn.devgalop.securelib.Core.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="Username field is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; } = string.Empty;
    }
}