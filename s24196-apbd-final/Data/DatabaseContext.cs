using Microsoft.EntityFrameworkCore;
using s24196_apbd_final.Models;

namespace s24196_apbd_final.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<TicketConcert> TicketConcerts { get; set; }
    public DbSet<PurchasedTicket> PurchasedTickets { get; set; }
        
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(new List<Customer>()
        {
            new() { CustomerId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "555-555-5555" },
            new() { CustomerId = 2, FirstName = "Jane", LastName = "Doe", PhoneNumber = null },
            new() { CustomerId = 3, FirstName = "Julie", LastName = "Doe", PhoneNumber = null }
        });
        
        modelBuilder.Entity<Ticket>().HasData(new List<Ticket>()
        {
            new() { TicketId = 1, SerialNumber = "#REF1", SeatNumber = 10 },
            new() { TicketId = 2, SerialNumber = "#REF2", SeatNumber = 20 },
            new() { TicketId = 3, SerialNumber = "#REF3", SeatNumber = 30 }
        });
        
        modelBuilder.Entity<Concert>().HasData(new List<Concert>()
        {
            new() { ConcertId = 1, Name = "Concert 1", Date = DateTime.Parse("2025-01-01"), AvailableTickets = 12000 },
            new() { ConcertId = 2, Name = "Opener", Date = DateTime.Parse("2025-03-12"), AvailableTickets = 15000 },
            new() { ConcertId = 3, Name = "SBM", Date = DateTime.Parse("2025-06-05"), AvailableTickets = 10000 }
        });
        
        modelBuilder.Entity<TicketConcert>().HasData(new List<TicketConcert>()
        {
            new() { TicketConcertId = 1, TicketId = 1, ConcertId = 1, Price = 199.99 },
            new() { TicketConcertId = 2, TicketId = 2, ConcertId = 2, Price = 299.99 },
            new() { TicketConcertId = 3, TicketId = 3, ConcertId = 3, Price = 399.99 }
        });
        
        modelBuilder.Entity<PurchasedTicket>().HasData(new List<PurchasedTicket>()
        {
            new() { TicketConcertId = 1, CustomerId = 1, PurchasedDate = DateTime.Parse("2025-01-01") },
            new() { TicketConcertId = 2, CustomerId = 2, PurchasedDate = DateTime.Parse("2025-03-03")  },
            new() { TicketConcertId = 3, CustomerId = 3, PurchasedDate = DateTime.Parse("2025-04-05")  }
        });
    }
}