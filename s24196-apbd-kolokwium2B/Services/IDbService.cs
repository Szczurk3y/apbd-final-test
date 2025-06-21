using s24196_apbd_final.DTOs;

namespace s24196_apbd_final.Services;

public interface IDbService
{
    public Task<CustomerPurchaseDto> GetCustomerPurchases(int customerId);
    public Task<int> AddCustomer(AddClientDto customerDto);
}