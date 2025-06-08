using Microsoft.EntityFrameworkCore;
using s24196_apbd_final.Data;
using s24196_apbd_final.DTOs;
using s24196_apbd_final.Exceptions;
using s24196_apbd_final.Models;

namespace s24196_apbd_final.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    
    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CustomerPurchaseDto> GetCustomerPurchases(int customerId)
    {
        var customerPurchases = await _context.PurchasedTickets
            .Select(p => new CustomerPurchaseDto
            {
                Id = p.Customer.CustomerId,
                FirstName = p.Customer.FirstName,
                LastName = p.Customer.LastName,
                PhoneNumber = p.Customer.PhoneNumber,
                Purchases = p.TicketConcert.PurchasedTickets.Select(pt => new PurchasedTicketDto
                {
                    Date = pt.PurchasedDate,
                    Price = p.TicketConcert.Price,
                    TicketDto = new TicketDto
                    {
                        Serial = p.TicketConcert.Ticket.SerialNumber,
                        SeatNumber = p.TicketConcert.Ticket.SeatNumber
                    },
                    ConcertDto = new ConcertDto
                    {
                        Name = p.TicketConcert.Concert.Name,
                        Date = p.TicketConcert.Concert.Date
                    }
                }).ToList()
            }).FirstOrDefaultAsync(c => c.Id == customerId);

        if (customerPurchases is null)
        {
            throw new NotFoundException();
        }

        return customerPurchases;
    }

    public async Task<int> AddCustomer(AddClientDto clientDto)
    {
        var customer = await _context.Customers
            .Where(c => c.CustomerId == clientDto.Customer.Id)
            .Include(customer => customer.PurchasedTickets)
            .FirstOrDefaultAsync();

        if (customer is not null) throw new CustomerAlreadyExistsException();
        if (clientDto.Purchases.Count > 5) throw new Exception("Klient nie może kupić więcej niż 5 biletów");
        
        var newCustomer = await _context.Customers.AddAsync(new Customer()
        {
            CustomerId = clientDto.Customer.Id,
            FirstName = clientDto.Customer.FirstName,
            LastName = clientDto.Customer.LastName,

        });
        await _context.SaveChangesAsync();
        return 1;
    }
}