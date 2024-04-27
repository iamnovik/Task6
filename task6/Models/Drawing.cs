using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task6.Models;

public class Drawing
{
    [Key]
    public int Id { get; set; }
    public string DrawingData { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TableId { get; set; }

    [ForeignKey("TableId")]
    public Table Table { get; set; }
}