using dotnet_backend.Data;
using dotnet_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _db;
    
    public TicketsController(AppDbContext db)
    {
        _db = db;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create(AddUpdateTicketsDTO dto)
    {
        var tick = new Tickets
        {
            title = dto.title,
            description = dto.description,
            priority = dto.priority,
        };
        _db.Tickets.Add(tick);
        await _db.SaveChangesAsync();
        return Ok(tick);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.Tickets.ToListAsync());
    }

    [HttpPatch("{id}/assign")]
    public async Task<IActionResult> Assign(int id, int userId)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket == null) return NotFound();
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return NotFound();
        ticket.assigned_to = userId;
        _db.Tickets.Update(ticket);
        await _db.SaveChangesAsync();
        return Ok(ticket);
            
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> Status(int id, UpdateStatusTicketsDTO dto)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket == null) return NotFound();

        var temp = new Tickets
        {
            status = dto.status
        };
        _db.Tickets.Update(temp);
        await _db.SaveChangesAsync();
        return Ok(temp);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var t = await _db.Tickets.FindAsync(id);
        if (t == null) return NotFound();
        _db.Tickets.Remove(t);
        await _db.SaveChangesAsync();
        return Ok(new {message="Ticket deleted" });
    }


    [HttpPost("{id}/comments")]
    public async Task<IActionResult> AddComment(int id, AddCommentDTO dto)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket == null) return NotFound();
        var comment = new Ticket_Comments
        {
            ticket_id = id,
            user_id = 1,
            comment = dto.comment,
            created_at = ticket.created_at
        };
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();
        return Ok(comment);
    }
    [HttpGet("{id}/comments")]
    public async Task<IActionResult> GetComments(int id)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket == null) return NotFound();
        
        var commments = await _db.Comments
            .Where(c => c.ticket_id == id)
            .ToListAsync();
        
        return Ok(commments);
    }

    
    
}