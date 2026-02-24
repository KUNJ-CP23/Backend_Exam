using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Models;

[Index(nameof(email), IsUnique = true)]
public class Users
{
    [Required]
    [Key]
    public int id { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public int role_id { get; set; }
    [ForeignKey(nameof(role_id))]
    public Roles Role { get; set; }
    public DateTime created_at { get; set; } =  DateTime.Now;
    
    public ICollection<Tickets> Tickets { get; set; }
    public ICollection<Ticket_Comments> Ticket_Comments { get; set; }
    public ICollection<Ticket_Status_Logs> Ticket_Status_Logs { get; set; }
}

public class AddUpdateUsersDTO
{
    [Required]
    public string name { get; set; }
    [Required]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public int role_id { get; set; }
}