using Shaghaf.Core.Entities.RoomEntities;

namespace Shaghaf.Core.Entities.HomeEntities
{
    public class Birthday:BaseEntity
    {
        public string Name { get; set; } 
        public DateTime Date { get; set; }
        public string Description { get; set; } 
        public ICollection<Cake> Cakes { get; set; }
        public ICollection<Decoration> Decorations { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}