using DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Services;

public class CustomerService : ICustomerService
{
    private readonly List<Customer?> _customers = new()
    {
        new DemoApp.Models.Customer { Id = 1, Name = "Alice Johnson", Phone = "123-456-7890" },
        new DemoApp.Models.Customer { Id = 2, Name = "Bob Smith", Phone = "987-654-3210" },
        new DemoApp.Models.Customer { Id = 3, Name = "Charlie Brown", Phone = "555-555-5555" },
        new DemoApp.Models.Customer { Id = 4, Name = "Diana Prince", Phone = "111-222-3333" },
        new DemoApp.Models.Customer { Id = 5, Name = "Ethan Hunt", Phone = "444-333-2222" }
    };
    public async Task<List<Customer?>> GetCustomersAsync()
    {
        await Task.Delay(1000);

        return _customers;
    }
    public async Task<List<Customer?>> SearchAsync(string searchText)
    {
        await Task.Delay(1000);

        var lowerSearchText = searchText.ToLower();

        var FilteredCustomers = _customers.Where(c =>
            c != null &&
            (c.Name.ToLower().Contains(lowerSearchText) ||
             c.Phone.ToLower().Contains(lowerSearchText))
        ).ToList();

        return FilteredCustomers;
    }
}
