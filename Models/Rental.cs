using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace VideoShopRentalRevision.Models
{
    public class Rental
    {
        private decimal _totalCost;
        [Key]
        public int RentalId { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public required DateTime RentalDate { get; set; }
        public int Quantity { get; set; }

        public decimal TotalCost 
        {
            get => Movie?.Price * Quantity ?? 0;
            set =>_totalCost = value; 
        }
        public DateTime ReturnDate { get; set; }
        public int RentalDuration { get; set; }
        public Customer? Customer { get; set; }
        public Movie? Movie { get; set; }
    }
}
