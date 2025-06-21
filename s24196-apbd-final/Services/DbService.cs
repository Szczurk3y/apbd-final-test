using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

    private async Task<Customer?> GetCustomer(int customerId)
    {
        var customer = await _context.Customers
            .Include(c => c.PurchasedTickets).ThenInclude(purchasedTicket => purchasedTicket.TicketConcert)
            .ThenInclude(ticketConcert => ticketConcert.PurchasedTickets).Include(customer => customer.PurchasedTickets)
            .ThenInclude(purchasedTicket => purchasedTicket.TicketConcert)
            .ThenInclude(ticketConcert => ticketConcert.Ticket).Include(customer => customer.PurchasedTickets)
            .ThenInclude(purchasedTicket => purchasedTicket.TicketConcert)
            .ThenInclude(ticketConcert => ticketConcert.Concert)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        
        return customer;
    }

    public async Task<CustomerPurchaseDto> GetCustomerPurchases(int customerId)
    {
        var customer = await GetCustomer(customerId);
        
        if (customer is null) throw new NotFoundException("Customer not found.");
        if (customer.PurchasedTickets.IsNullOrEmpty()) throw new NotFoundException("No purchased tickets.");
        
        var customerPurchases = new CustomerPurchaseDto {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Purchases = customer.PurchasedTickets
                .Select(p => new PurchasedTicketDto
                {
                    Date = p.PurchasedDate,
                    Price = p.TicketConcert.Price,
                    Ticket = new TicketDto
                    {
                        Serial = p.TicketConcert.Ticket.SerialNumber,
                        SeatNumber = p.TicketConcert.Ticket.SeatNumber
                    },
                    Concert = new ConcertDto
                    {
                        Name = p.TicketConcert.Concert.Name,
                        Date = p.TicketConcert.Concert.Date
                    }
                }).ToList()
        };

        return customerPurchases;
    }

    public async Task<int> AddCustomer(AddClientDto clientDto)
    {
        var customer = await GetCustomer(clientDto.Customer.Id);

        if (customer is not null) throw new CustomerAlreadyExistsException("Customer already exists.");
        
        Dictionary<string, int> ticketsSet = clientDto.Purchases
            .GroupBy(t => t.ConcertName)
            .ToDictionary(g => g.Key, g => g.Count());

        if (ticketsSet.Values.Any(c => c > 5))
            throw new TooManyTicketsException("Only 5 tickets per concert is allowed.");
        
        var newCustomer = new Customer
        {
            FirstName = clientDto.Customer.FirstName,
            LastName = clientDto.Customer.LastName
        };
        await _context.Customers.AddAsync(newCustomer);
        await _context.SaveChangesAsync();

        foreach (var purchase in clientDto.Purchases)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(c => c.Name == purchase.ConcertName);
            if (concert is null) throw new NotFoundException($"Concert {purchase.ConcertName} was not found.");
            
            var ticket = new Ticket
            {
                SeatNumber = purchase.SeatNumber,
                SerialNumber = $"TK{DateTime.Now.Year}/{purchase.ConcertName}/S{purchase.SeatNumber}"
            };
            
            var ticketResult = await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            var ticketConcert = new TicketConcert
            {
                ConcertId = concert.ConcertId,
                TicketId = ticketResult.Entity.TicketId,
                Price = purchase.Price
            };
            
            var ticketConcertResult = await _context.TicketConcerts.AddAsync(ticketConcert);
            await _context.SaveChangesAsync();

            var purchasedTicket = new PurchasedTicket
            {
                TicketConcertId = ticketConcertResult.Entity.TicketConcertId,
                CustomerId = newCustomer.CustomerId,
                PurchasedDate = DateTime.Today
            };
            
            var purchasedTicketResult = await _context.PurchasedTickets.AddAsync(purchasedTicket);
            await _context.SaveChangesAsync();
        }
        
        return newCustomer.CustomerId;
    }
}