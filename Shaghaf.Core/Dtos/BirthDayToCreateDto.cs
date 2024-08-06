using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos
{
    public class BirthDayToCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int HomeId { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }


     
    }
}
