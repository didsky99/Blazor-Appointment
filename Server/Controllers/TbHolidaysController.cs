using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorAppointmentSystem.Server.Data;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Server.Controllers
{
    [Route("api/TbHolidays")]
    [ApiController]
    public class TbHolidaysController : ControllerBase
    {
        private readonly AppointmentDbContext _context;

        public TbHolidaysController(AppointmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TbHolidays>>> GetAll()
        {
            return await _context.TbHolidays.ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TbHolidays>> GetById(byte id)
        {
            var holiday = await _context.TbHolidays.FindAsync(id);
            if (holiday == null)
                return NotFound();
            return holiday;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<TbHolidays>> Create(TbHolidays holiday)
        {
            _context.TbHolidays.Add(holiday);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = holiday.Id }, holiday);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(byte id, TbHolidays holiday)
        {
            if (id != holiday.Id)
                return BadRequest();

            _context.Entry(holiday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TbHolidays.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var holiday = await _context.TbHolidays.FindAsync(id);
            if (holiday == null)
                return NotFound();

            _context.TbHolidays.Remove(holiday);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
