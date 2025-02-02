using System.ComponentModel.DataAnnotations.Schema;

namespace VideoShopRentalRevision.Models
{
    public class RentalDetail
    {
        public int RentalDetailsId { get; set; }
        public int RentalId { get; set; }
        public Rental? Rental { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int Quantity { get; set; }
    }
}
