using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorAppointmentSystem.Server.Data;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Server.Controllers
{
    [Route("api/MAppointmentStatus")]
    [ApiController]
    public class MAppointmentStatusController : ControllerBase
    {
        private readonly AppointmentDbContext _context;

        public MAppointmentStatusController(AppointmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MAppointmentStatus>>> GetAll()
        {
            return await _context.MAppointmentStatuses.ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MAppointmentStatus>> GetById(byte id)
        {
            var status = await _context.MAppointmentStatuses.FindAsync(id);
            if (status == null)
                return NotFound();

            return status;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<MAppointmentStatus>> Create(MAppointmentStatus status)
        {
            _context.MAppointmentStatuses.Add(status);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = status.Id }, status);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(byte id, MAppointmentStatus status)
        {
            if (id != status.Id)
                return BadRequest();

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MAppointmentStatuses.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var status = await _context.MAppointmentStatuses.FindAsync(id);
            if (status == null)
                return NotFound();

            _context.MAppointmentStatuses.Remove(status);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
