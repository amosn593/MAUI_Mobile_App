using DemoApp.Models;

namespace DemoApp.Services;

public interface ICustomerService
{
    Task<List<Customer?>> SearchAsync(string searchText);
    Task<List<Customer?>> GetCustomersAsync();
}
