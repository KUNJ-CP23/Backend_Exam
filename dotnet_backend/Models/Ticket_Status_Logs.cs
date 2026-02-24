using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_backend.Models;

public class Ticket_Status_Logs
{
    [Required]
    [Key]
    public int id { get; set; }
    
    public int ticket_id { get; set; }
    [ForeignKey(nameof(ticket_id))]
    public Tickets Tickets { get; set; }
    
    public Ticket_Status old_status { get; set; }
    public Ticket_Status new_status { get; set; }

    public int changed_by { get; set; }
    [ForeignKey(nameof(changed_by))]
    public Users Users { get; set; }
    
    public DateTime changed_at { get; set; }
}