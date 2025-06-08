namespace s24196_apbd_final.DTOs;

public class CustomerPurchaseDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhoneNumber { get; set; } = null;

    public List<PurchasedTicketDto> Purchases { get; set; } = [];
}