﻿using Shaghaf.Core.Entities.RoomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Dtos
{
    public class RoomToCreateOrUpdateDto
    {
        public decimal Offer { get; set; }
        public decimal Rate { get; set; }
        public string Name { get; set; } = null!;
        public int Seat { get; set; }
        public string Description { get; set; } = null!;
        public int LocationId { get; set; } 
        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public string Plan { get; set; }

        public List<int> SelectedCategories { get; set; } = default!;
    }
}
