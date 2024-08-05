using Shaghaf.Core.Entities.BookingEntities;
using System.ComponentModel.DataAnnotations;

namespace Shaghaf.Core.Dtos
{
    public class BookingDto
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public int SeatCount { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public decimal Discount { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string SessionId { get; set; }
    }
}
