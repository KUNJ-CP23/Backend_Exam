using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_backend.Models;

public enum Ticket_Status
{
    OPEN,
    IN_PROGRESS,
    RESOLVED,
    COMPLETED
}

public enum Ticket_Priority
{
    LOW,
    MEDIUM,
    HIGH
}

public class Tickets
{
    [Required]
    [Key]
    public int id { get; set; }
    [Required]
    public string title { get; set; }
    [Required]
    public string description { get; set; }
    
    public Ticket_Status status { get; set; } = Ticket_Status.OPEN;
    public Ticket_Priority priority { get; set; } = Ticket_Priority.MEDIUM;
    
    [Required]
    public int created_by { get; set; }
    [ForeignKey(nameof(created_by))]
    public Users CreatedBy { get; set; }
    
    public int? assigned_to { get; set; }
    
    public Users AssignedTo { get; set; }
    
    public DateTime created_at { get; set; } = DateTime.Now;
    
    public ICollection<Ticket_Comments> Ticket_Comments { get; set; }
    public ICollection<Ticket_Status_Logs> Ticket_Status_Logs { get; set; }
    
}

public class AddUpdateTicketsDTO
{
    [Required]
    [MinLength(5)]
    public string title { get; set; }
    [Required]
    [MaxLength(50)]
    public string description { get; set; }
    
    public Ticket_Priority priority { get; set; }
}

public class UpdateStatusTicketsDTO
{
    [Required]
    public Ticket_Status status { get; set; }
}

public class AddCommentDTO
{
    [Required]
    public string comment { get; set; }
}