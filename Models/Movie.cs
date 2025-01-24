namespace VideoShopRentalRevision.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public ICollection<Rental>? Rentals { get; set; }
    }
}
