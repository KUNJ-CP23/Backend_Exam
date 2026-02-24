using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_backend.Models;

public class Ticket_Comments
{
    [Required]
    [Key]
    public int id { get; set; }
    
    //delete cascade aapvanu
    public int ticket_id  { get; set; }
    [ForeignKey(nameof(ticket_id))]
    public Tickets Tickets { get; set; }
    
    public int user_id  { get; set; }
    [ForeignKey(nameof(user_id))]
    public Users Users { get; set; }
    
    [Required]
    public string comment { get; set; }
    public DateTime created_at { get; set; } = DateTime.Now;
}

public class AddUpdateTicketCommentsDTO
{
    [Required]
    public string comment { get; set; }
}