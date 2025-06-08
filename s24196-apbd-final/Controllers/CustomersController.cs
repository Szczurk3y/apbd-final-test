using Microsoft.AspNetCore.Mvc;
using s24196_apbd_final.DTOs;
using s24196_apbd_final.Exceptions;
using s24196_apbd_final.Services;

namespace s24196_apbd_final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IDbService _dbService;
    
    public CustomersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id:int}/purchases")]
    public async Task<IActionResult> GetAsync(int id)
    {
        Console.WriteLine("Purchases");
        try
        {
            var customerPurchases = await _dbService.GetCustomerPurchases(id);
            return Ok(customerPurchases);
        }
        catch (NotFoundException e)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddClientDto clientDto)
    {
        try
        {
            var result = await _dbService.AddCustomer(clientDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}
