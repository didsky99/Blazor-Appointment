using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorAppointmentSystem.Server.Data;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Server.Controllers
{
    [Route("api/MGender")]
    [ApiController]
    public class MGenderController : ControllerBase
    {
        private readonly AppointmentDbContext _context;

        public MGenderController(AppointmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MGender>>> GetAll()
        {
            var genders = await _context.MGenders.ToListAsync();
            return Ok(genders);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MGender>> GetById(byte id)
        {
            var gender = await _context.MGenders.FindAsync(id);
            if (gender == null)
                return NotFound();

            return gender;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<MGender>> Create(MGender gender)
        {
            _context.MGenders.Add(gender);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = gender.Id }, gender);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(byte id, MGender gender)
        {
            if (id != gender.Id)
                return BadRequest();

            _context.Entry(gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MGenders.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var gender = await _context.MGenders.FindAsync(id);
            if (gender == null)
                return NotFound();

            _context.MGenders.Remove(gender);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
