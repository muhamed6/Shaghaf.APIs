using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos
{
    public class RegisterDto
    {

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$%^&+=])(?!.*\\s).{8,16}$"
            , ErrorMessage = "Password Must Have 1 Uppercase, 1 Lowercase, 1 number ")]
        public string Password { get; set; } = null!;
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$%^&+=])(?!.*\\s).{8,16}$"
            , ErrorMessage = "Password Must Have 1 Uppercase, 1 Lowercase, 1 number ")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
