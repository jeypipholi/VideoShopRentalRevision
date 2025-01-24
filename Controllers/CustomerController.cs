using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoShopRentalRevision.Data;
using VideoShopRentalRevision.Models;

namespace VideoShopRentalRevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public CustomerController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomer()
        {
            var customers = await dbContext.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await dbContext.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer([FromBody] Customer customer)
        {
            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new {id = customer.CustomerId}, customer);
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id,Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            dbContext.Entry(customer).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await dbContext.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
