namespace Shaghaf.Core.Entities.HomeEntities
{
    public class PhotoSession:BaseEntity
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } 
        public decimal Price { get; set; } 
        public int HomeId { get; set; } 
        public Home Home { get; set; }
    }
}