using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorAppointmentSystem.Server.Data;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbCustomerController : ControllerBase
    {
        private readonly AppointmentDbContext _context;

        public TbCustomerController(AppointmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TbCustomer>>> GetAll()
        {
            return await _context.TbCustomers.ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TbCustomer>> GetById(string id)
        {
            var customer = await _context.TbCustomers.FindAsync(id);
            if (customer == null)
                return NotFound();
            return customer;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<TbCustomer>> Create(TbCustomer customer)
        {
            _context.TbCustomers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(string id, TbCustomer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _context.TbCustomers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.TbCustomers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
