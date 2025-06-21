using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace s24196_apbd_final.Models;

[Table("Purchased_Ticket")]
[PrimaryKey(nameof(TicketConcertId), nameof(CustomerId))]
public class PurchasedTicket
{
    [ForeignKey(nameof(TicketConcert))]
    public int TicketConcertId { get; set; }
    
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }
    
    public DateTime PurchasedDate { get; set; }

    public TicketConcert TicketConcert { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}