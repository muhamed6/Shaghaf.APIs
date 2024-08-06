namespace Shaghaf.Core.Entities.HomeEntities
{
    public class Cake:BaseEntity
    {
        

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ServingSize { get; set; } 
        public int BirthdayId { get; set; } 
        public Birthday Birthday { get; set; }
    }

}