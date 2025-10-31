using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorAppointmentSystem.Server.Data;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Server.Controllers
{
    [Route("api/MSettings")]
    [ApiController]
    public class MSettingsController : ControllerBase
    {
        private readonly AppointmentDbContext _context;

        public MSettingsController(AppointmentDbContext context)
        {
            _context = context;
        }

        // GET: api/MSettings/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<MSettings>>> GetAll()
        {
            return await _context.MSettings.ToListAsync();
        }

        // GET: api/MSettings/GetById/1
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MSettings>> GetById(byte id)
        {
            var setting = await _context.MSettings.FindAsync(id);
            if (setting == null)
                return NotFound();

            return setting;
        }

        // POST: api/MSettings/Create
        [HttpPost("Create")]
        public async Task<ActionResult<MSettings>> Create(MSettings setting)
        {
            _context.MSettings.Add(setting);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = setting.Id }, setting);
        }

        // PUT: api/MSettings/Update/1
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(byte id, MSettings setting)
        {
            if (id != setting.Id)
                return BadRequest();

            _context.Entry(setting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MSettings.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/MSettings/Delete/1
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var setting = await _context.MSettings.FindAsync(id);
            if (setting == null)
                return NotFound();

            _context.MSettings.Remove(setting);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
