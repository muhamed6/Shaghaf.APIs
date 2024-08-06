namespace Shaghaf.Core.Entities.HomeEntities
{
    public class Advertisement:BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime EventDate { get; set; }
    
        public int HomeId { get; set; } 
        public Home Home { get; set; }

        

        public decimal Price { get; set; } 
    }
}