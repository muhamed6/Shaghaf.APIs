using Shaghaf.Core.Entities.RoomEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shaghaf.Core.Entities.HomeEntities
{
    public class PhotoSession:BaseEntity
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } 
        public decimal Price { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}