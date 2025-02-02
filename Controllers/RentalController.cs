using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoShopRentalRevision.Data;
using VideoShopRentalRevision.Models;

namespace VideoShopRentalRevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RentalController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Rental
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetAllRentals()
        {
            var rentals = await _dbContext.Rentals
                .Include(r => r.Customer) // Eager loading of related Customer
                .Include(r => r.RentalDetails) // Eager loading RentalDetails if needed
                    .ThenInclude(d => d.Movie) // Include related Movie
                .ToListAsync();

            return Ok(rentals);
        }

        // GET: api/Rental/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRentalById(int id)
        {
            var rental = await _dbContext.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(d => d.Movie)
                .FirstOrDefaultAsync(r => r.RentalId == id);

            if (rental == null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        /*
        // POST: api/Rental
        [HttpPost]
        public async Task<ActionResult> AddRental([FromBody] Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Rentals.Add(rental);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRentalById), new { id = rental.RentalId }, rental);
        }*/
        [HttpPost]
        public async Task<IActionResult> CreateRental(int customerId, List<int> movieIds)
        {
            if (movieIds == null || movieIds.Count == 0)
                return BadRequest("You must provide at least one movie to rent.");

            var movies = await _dbContext.Movies.Where(m => movieIds.Contains(m.MovieId)).ToListAsync();

            if (movies.Count != movieIds.Count)
                return NotFound("Some movies are not found.");

            var rental = new Rental
            {
                CustomerId = customerId,
                RentalDate = DateTime.UtcNow,
                Movies = movies
            };

            _dbContext.Rentals.Add(rental);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRentalById), new { id = rental.RentalId }, rental);
        }


        // PUT: api/Rental/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRental(int id, [FromBody] Rental rental)
        {
            if (id != rental.RentalId)
            {
                return BadRequest("Rental ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Entry(rental).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Rental/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRental(int id)
        {
            var rental = await _dbContext.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            _dbContext.Rentals.Remove(rental);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalExists(int id)
        {
            return _dbContext.Rentals.Any(r => r.RentalId == id);
        }
    }
}
