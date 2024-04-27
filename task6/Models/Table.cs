using System.ComponentModel.DataAnnotations;

namespace task6.Models;

public class Table
{
    [Key]
    public int Id { get; set; }
   
    public String Name { get; set; } = String.Empty;
    public ICollection<Drawing> Drawings { get; set; }
}