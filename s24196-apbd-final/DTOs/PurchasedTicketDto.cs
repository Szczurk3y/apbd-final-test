namespace s24196_apbd_final.DTOs;

public class PurchasedTicketDto
{
    public required DateTime Date { get; set; }
    public required double Price { get; set; }
    public required TicketDto TicketDto { get; set; }
    public required ConcertDto ConcertDto { get; set; }
}