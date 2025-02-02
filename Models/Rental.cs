using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace VideoShopRentalRevision.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        [JsonIgnore]
        public Customer? Customer { get; set; }
        public ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
