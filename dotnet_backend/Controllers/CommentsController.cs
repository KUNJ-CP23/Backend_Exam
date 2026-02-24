using dotnet_backend.Data;
using dotnet_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController:ControllerBase
{
    private readonly AppDbContext _db;
    
    public CommentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateComment(int id, AddUpdateTicketCommentsDTO dto)
    {
        var comment = await _db.Comments.FindAsync(id);
        if (comment == null) return NotFound();

        var com = new Ticket_Comments
        {
            comment = dto.comment
        };
        _db.Comments.Update(comment);
        await _db.SaveChangesAsync();
        return Ok(comment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var c = await _db.Comments.FindAsync(id);
        if (c == null) return NotFound();
        _db.Comments.Remove(c);
        await _db.SaveChangesAsync();
        return Ok(new {message="COmment Deleted"});
    }
}