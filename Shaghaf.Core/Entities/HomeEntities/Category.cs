﻿namespace Shaghaf.Core.Entities.HomeEntities
{
    public class Category:BaseEntity
    {
      

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<RoomCategory> RoomCategories { get; set; }

    }
}