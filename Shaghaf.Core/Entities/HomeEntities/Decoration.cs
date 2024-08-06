namespace Shaghaf.Core.Entities.HomeEntities
{
    public class Decoration:BaseEntity
    {

        public string Description { get; set; }  
        public decimal Price { get; set; }  
        public int BirthdayId { get; set; }  
        public Birthday Birthday { get; set; }
    }
}