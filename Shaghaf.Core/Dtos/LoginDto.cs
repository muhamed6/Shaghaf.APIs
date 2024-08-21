using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos
{
    public class LoginDto
    {
        [Required]

        public string PhoneNumber { get; set; } = null!;

        [Required]

        public string Password { get; set; } = null!;
    }
}
