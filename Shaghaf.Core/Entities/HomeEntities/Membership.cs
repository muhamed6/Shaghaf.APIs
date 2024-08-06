namespace Shaghaf.Core.Entities.HomeEntities
{
  
    public class Membership:BaseEntity
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int HomeId { get; set; } 
        public Home Home { get; set; }
    }
}