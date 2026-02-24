using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Models;

public class Roles
{
    [Required]
    [Key]
    public int id { get; set; }
    public string name { get; set; }
    public ICollection<Users> Users { get; set; }
}