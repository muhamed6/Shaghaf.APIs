namespace Shaghaf.Core.Entities.HomeEntities
{
    public class Category:BaseEntity
    {
      

        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int HomeId { get; set; } 
        public Home Home { get; set; }
    }
}