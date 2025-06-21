using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s24196_apbd_final.Models;

[Table("Concert")]
public class Concert
{
    [Key]
    public int ConcertId { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string Name { get; set; }
    
    public DateTime Date { get; set; }
    public int AvailableTickets { get; set; }

    public ICollection<TicketConcert> TicketConcerts { get; set; } = null!;
}