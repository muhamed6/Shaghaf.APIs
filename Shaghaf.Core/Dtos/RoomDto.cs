﻿using Shaghaf.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos
{
    public class RoomDto
    {
       public int Id { get; set; }
        public string Type { get; set; }
   
        public string Plan { get; set; }
        
        public decimal Offer { get; set; }
        
        public decimal Rate { get; set; }
    
        public string Name { get; set; } = null!;
      
        public int Seat { get; set; }
       
        public string Description { get; set; } = null!;
      
        public int LocationId { get; set; } 
       
        public DateTime Date { get; set; }
       

        public decimal Price { get; set; }

        public string BookingPhoneNumber { get; set; } = null!;
        public ICollection<RoomCategory> RoomCategories { get; set; } = new List<RoomCategory>();
    }
}
