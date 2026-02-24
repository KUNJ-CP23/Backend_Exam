using dotnet_backend.Data;
using dotnet_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _db.Users.Include(u=>u.Role).ToListAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AddUpdateUsersDTO dto)
    {
        var user = new Users
        {
            name = dto.name,
            email = dto.email,
            password = dto.password,
            role_id = dto.role_id
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return Ok(user);
    }
    
}