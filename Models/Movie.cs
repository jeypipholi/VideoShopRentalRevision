using System.Text.Json.Serialization;

namespace VideoShopRentalRevision.Models
{
    public class Movie
    {

        public int MovieId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<Rental>? Rentals { get; set; }
        public ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
