using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorAppointmentSystem.Server.Data;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Server.Controllers;

[ApiController]
[Route("api/TxAppointment")]
public class TxAppointmentController : ControllerBase
{
    private readonly AppointmentDbContext _context;

    public TxAppointmentController(AppointmentDbContext context)
    {
        _context = context;
    }

    // GET: api/TxAppointment/GetAll
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<AppointmentViewModel>>> GetAppointments()
    {
        var appointments = await (from a in _context.TxAppointments
                                  join s in _context.MAppointmentStatuses
                                      on a.AppointmentStatus equals s.StatusId
                                  join c in _context.TbCompanies
                                      on a.CustCompany equals c.Id
                                  select new AppointmentViewModel
                                  {
                                      Id = a.Id,
                                      Title = a.Title,
                                      Description = a.Description,
                                      CustId = a.CustId,
                                      CustName = a.CustName,
                                      CustCompany = c.Name,
                                      Token = a.Token,
                                      Duration = a.Duration,
                                      StartTime = a.StartTime,
                                      EndTime = a.EndTime,
                                      AppointmentStatus = a.AppointmentStatus,
                                      StatusName = s.StatusName,
                                      CreatedBy = a.CreatedBy,
                                      CreatedAt = a.CreatedAt
                                  }).ToListAsync();

        return Ok(appointments);
    }

    // GET: api/TxAppointment/GetById/5
    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<TxAppointment>> GetAppointment(int id)
    {
        var appointment = await _context.TxAppointments.FindAsync(id);

        if (appointment == null)
            return NotFound();

        return Ok(appointment);
    }

    // POST: api/TxAppointment/Create
    [HttpPost("Create")]
    public async Task<ActionResult<TxAppointment>> CreateAppointment([FromBody] TxAppointment appointment)
    {
        string companyId = null;

        if (appointment == null)
            return BadRequest("Invalid appointment data.");

        // 1. Set createdBy
        appointment.CreatedBy = "adm001";

        // 2. Set CreatedAt if not provided
        appointment.CreatedAt = appointment.CreatedAt == default ? DateTime.UtcNow : appointment.CreatedAt;

        // 3. Generate token (e.g., A001, reset daily)
        var today = DateTime.UtcNow.Date;
        var lastToken = await _context.TxAppointments
            .Where(a => a.CreatedAt >= today)
            .OrderByDescending(a => a.Token)
            .Select(a => a.Token)
            .FirstOrDefaultAsync();

        int nextNumber = 1;
        if (!string.IsNullOrEmpty(lastToken) && lastToken.Length == 4 && int.TryParse(lastToken.Substring(1), out var num))
        {
            nextNumber = num + 1;
        }
        appointment.Token = $"A{nextNumber:000}";

        // 4. Set duration
        if (appointment.StartTime != null && appointment.EndTime.HasValue)
            appointment.Duration = appointment.EndTime - appointment.StartTime;
        else
            appointment.Duration = TimeSpan.Zero;

        // 5. Create new customer if no CustId
        if (string.IsNullOrWhiteSpace(appointment.CustId))
        {

            // Map CustCompany to CompanyId
            if (!string.IsNullOrWhiteSpace(appointment.CustCompany))
            {
                var company = await _context.TbCompanies
                    .FirstOrDefaultAsync(c => c.Name == appointment.CustCompany);

                if (company != null)
                    companyId = company.Id;
            }

            var newCustomer = new TbCustomer
            {
                Id = Guid.NewGuid().ToString("N").Substring(0, 10), // 10-char id
                Name = appointment.CustName,
                CompanyId = companyId, // map company from form
                GenderId = 4,       // optionally set gender
                CreatedBy = "adm001",
                CreatedAt = DateTime.UtcNow
            };
            _context.TbCustomers.Add(newCustomer);
            await _context.SaveChangesAsync();

            appointment.CustId = newCustomer.Id;
        }

        // 6. Set appointment status to SCH
        var status = await _context.MAppointmentStatuses.FirstOrDefaultAsync(s => s.StatusId == "SCH");
        if (status != null)
        {
            appointment.AppointmentStatus = status.StatusId;
        }

        TxAppointment form = new TxAppointment()
        {
            Title = appointment.Title,
            Description = appointment.Description,
            CustId = appointment.CustId,
            CustName = appointment.CustName,
            CustCompany = companyId,
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime,
            Duration = appointment.EndTime - appointment.StartTime,
            Token = appointment.Token, // or generate token here
            AppointmentStatus = appointment.AppointmentStatus, // or set default "SCH"
            CreatedBy = appointment.CreatedBy ?? "adm001",
            CreatedAt = appointment.CreatedAt == default ? DateTime.UtcNow : appointment.CreatedAt
        };
        // 7. Save appointment
        _context.TxAppointments.Add(form);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
    }


    // PUT: api/TxAppointment/Update/5
    [HttpPut("Update/{id}")]
    public async Task<ActionResult<TxAppointment>> UpdateAppointment(int id, [FromBody] AppointmentViewModel updated)
    {
        string companyId = null;
        var appointment = await _context.TxAppointments.FindAsync(id);
        if (appointment == null) return NotFound();

        if (!string.IsNullOrWhiteSpace(updated.CustCompany))
        {
            var company = await _context.TbCompanies
                .FirstOrDefaultAsync(c => c.Name == updated.CustCompany);

            if (company != null)
                companyId = company.Id;
        }

        // Update fields
        appointment.Title = updated.Title;
        appointment.Description = updated.Description;
        appointment.CustName = updated.CustName;
        appointment.CustCompany = companyId;
        appointment.StartTime = updated.StartTime;
        appointment.EndTime = updated.EndTime;
        appointment.Duration = updated.EndTime - updated.StartTime;

        await _context.SaveChangesAsync();

        return Ok(appointment);
    }


    // PUT: api/TxAppointment/ChangeStatus/{id}
    [HttpPost("ChangeStatus/{id}")]
    public async Task<ActionResult<TxAppointment>> ChangeStatus(int id, [FromBody] string newStatus)
    {
        var appointment = await _context.TxAppointments.FindAsync(id);
        if (appointment == null) return NotFound();

        appointment.AppointmentStatus = newStatus;
        await _context.SaveChangesAsync();

        return Ok(appointment);
    }


    // DELETE: api/TxAppointment/Delete/5
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointment = await _context.TxAppointments.FindAsync(id);
        if (appointment == null)
            return NotFound();

        _context.TxAppointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //custom functions

    [HttpGet("GetAppointmentList")]
    public async Task<ActionResult<IEnumerable<AppointmentViewModel>>> GetAppointmentList()
    {
        var result = await (
            from a in _context.TxAppointments
            join c in _context.TbCustomers on a.CustId equals c.Id into custGroup
            from c in custGroup.DefaultIfEmpty()
            join comp in _context.TbCompanies on c.CompanyId equals comp.Id into compGroup
            from comp in compGroup.DefaultIfEmpty()
            join s in _context.MAppointmentStatuses on a.AppointmentStatus equals s.StatusId into statusGroup
            from s in statusGroup.DefaultIfEmpty()
            orderby a.StartTime descending
            select new AppointmentViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                CustId = a.CustId,
                CustName = c != null ? c.Name : a.CustName,
                CustCompany = comp != null ? comp.Name : a.CustCompany,
                Token = a.Token,
                Duration = a.Duration,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                AppointmentStatus = a.AppointmentStatus,
                CreatedBy = a.CreatedBy,
                CreatedAt = a.CreatedAt
            }
        ).ToListAsync();

        return Ok(result);
    }

}
