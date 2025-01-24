namespace VideoShopRentalRevision.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public required string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public ICollection<Rental>? Rentals { get; set; }
    }
}
