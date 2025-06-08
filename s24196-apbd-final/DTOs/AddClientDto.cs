namespace s24196_apbd_final.DTOs;

public class AddClientDto
{
    public CustomerDto Customer { get; set; }
    public List<PurchasesDto> Purchases { get; set; }
}