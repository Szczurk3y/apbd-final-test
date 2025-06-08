using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s24196_apbd_final.Models;

[Table("Customer")]
public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    
    [MaxLength(50)]
    [Required]
    public required string FirstName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string LastName { get; set; }

    [MaxLength(100)] 
    public string? PhoneNumber { get; set; } = null;

    public ICollection<PurchasedTicket> PurchasedTickets { get; set; } = null!;
}