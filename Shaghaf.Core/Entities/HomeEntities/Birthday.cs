namespace Shaghaf.Core.Entities.HomeEntities
{
    public class Birthday:BaseEntity
    {
        public string Name { get; set; } 
        public DateTime Date { get; set; }
        public string Description { get; set; } 
        public ICollection<Cake> Cakes { get; set; }
        public ICollection<Decoration> Decorations { get; set; }
        public int HomeId { get; set; } 
        public Home Home { get; set; }
    }
}