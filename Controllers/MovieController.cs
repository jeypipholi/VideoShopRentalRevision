using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoShopRentalRevision.Data;
using VideoShopRentalRevision.Models;

namespace VideoShopRentalRevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public MovieController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovie()
        {
            var movies = await dbContext.Movies.ToListAsync();
            return movies;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await dbContext.Movies.FindAsync(id);

            if (movie is null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie([FromBody] Movie movie)
        {
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetMovieById), new {id = movie.MovieId },movie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }
            dbContext.Entry(movie).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await dbContext.Movies.FindAsync(id);

            if (movie is null)
            {
                return NotFound();
            }

            dbContext.Movies.Remove(movie);
            dbContext.SaveChanges();
            return Ok();

        }
    }
}
