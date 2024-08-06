﻿using System.ComponentModel.DataAnnotations;

namespace Shaghaf.Core.Dtos
{
    public class AdvertisementDto
    {
  

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
      
    }
}