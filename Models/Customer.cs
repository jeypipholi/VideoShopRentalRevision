namespace VideoShopRentalRevision.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public ICollection<Rental>? Rentals { get; set; }
    }
}
