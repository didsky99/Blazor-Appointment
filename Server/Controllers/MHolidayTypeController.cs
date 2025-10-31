using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorAppointmentSystem.Server.Data;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Server.Controllers
{
    [Route("api/MHolidayType")]
    [ApiController]
    public class MHolidayTypeController : ControllerBase
    {
        private readonly AppointmentDbContext _context;

        public MHolidayTypeController(AppointmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MHolidayType>>> GetAll()
        {
            return await _context.MHolidayTypes.ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MHolidayType>> GetById(byte id)
        {
            var type = await _context.MHolidayTypes.FindAsync(id);
            if (type == null)
                return NotFound();

            return type;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<MHolidayType>> Create(MHolidayType type)
        {
            _context.MHolidayTypes.Add(type);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = type.Id }, type);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(byte id, MHolidayType type)
        {
            if (id != type.Id)
                return BadRequest();

            _context.Entry(type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MHolidayTypes.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var type = await _context.MHolidayTypes.FindAsync(id);
            if (type == null)
                return NotFound();

            _context.MHolidayTypes.Remove(type);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
