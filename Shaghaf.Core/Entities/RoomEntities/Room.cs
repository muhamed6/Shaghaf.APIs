

using Shaghaf.Core.Entities.HomeEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shaghaf.Core.Entities.RoomEntities
{
    public class Room :BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Offer { get; set; }
        public decimal Rate { get; set; }
        public int Seat { get; set; }
        public string Description { get; set; } = null!;




        public int LocationId { get; set; }
        public Location Location { get; set; } 
        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public RoomPlan Plan { get; set; } = RoomPlan.Hour; // default is per hour


        public RoomType Type { get; set; } = RoomType.FunnyRoom; // default is funny

    }
}
