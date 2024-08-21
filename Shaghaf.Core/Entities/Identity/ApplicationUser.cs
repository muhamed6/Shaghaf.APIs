using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Entities.Identity
{
    public class ApplicationUser :IdentityUser
    {
        [NotMapped]
        public virtual string? ConfirmPasswordHash { get; set; }
    }
}
