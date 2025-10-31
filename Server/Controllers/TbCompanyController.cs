using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorAppointmentSystem.Server.Data;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbCompanyController : ControllerBase
    {
        private readonly AppointmentDbContext _context;

        public TbCompanyController(AppointmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TbCompany>>> GetAll()
        {
            return await _context.TbCompanies.ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TbCompany>> GetById(string id)
        {
            var company = await _context.TbCompanies.FindAsync(id);
            if (company == null)
                return NotFound();
            return company;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<TbCompany>> Create(TbCompany company)
        {
            _context.TbCompanies.Add(company);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(string id, TbCompany company)
        {
            if (id != company.Id)
                return BadRequest();

            _context.Entry(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var company = await _context.TbCompanies.FindAsync(id);
            if (company == null)
                return NotFound();

            _context.TbCompanies.Remove(company);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
