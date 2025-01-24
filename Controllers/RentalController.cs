using Microsoft.AspNetCore.Http;
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
        private readonly AppDbContext dbContext;

        public RentalController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetAllRental()
        {
            var rentals = await dbContext.Rentals.ToListAsync();
            return rentals;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRentalById(int id)
        {
            var rental = await dbContext.Rentals.FindAsync(id);

            if (rental is null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        [HttpPost]
        public async Task<ActionResult> AddRental([FromBody] Rental rental)
        {
            dbContext.Rentals.Add(rental);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetRentalById), new { id = rental.RentalId }, rental);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRental(int id, Rental rental)
        {
            if (id != rental.RentalId)
            {
                return BadRequest();
            }
            dbContext.Entry(rental).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var rental = await dbContext.Rentals.FindAsync(id);

            if (rental is null)
            {
                return NotFound();
            }

            dbContext.Rentals.Remove(rental);
            dbContext.SaveChanges();
            return NoContent();

        }
    }
}
