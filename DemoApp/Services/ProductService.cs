using DemoApp.Models;
using DemoApp.Services;
using System.Net.Http.Json;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    private static readonly List<ProductDto> _products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Laptop", Price = 999.99m },
            new ProductDto { Id = 2, Name = "Smartphone", Price = 499.50m },
            new ProductDto { Id = 3, Name = "Wireless Mouse", Price = 25.00m },
            new ProductDto { Id = 4, Name = "Mechanical Keyboard", Price = 80.00m },
            new ProductDto { Id = 5, Name = "Monitor", Price = 150.00m },
            new ProductDto { Id = 6, Name = "USB-C Cable", Price = 15.99m },
            new ProductDto { Id = 7, Name = "External Hard Drive", Price = 120.00m },
            new ProductDto { Id = 8, Name = "Webcam", Price = 45.00m },
            new ProductDto { Id = 9, Name = "Desk Lamp", Price = 30.00m },
            new ProductDto { Id = 10, Name = "Gaming Chair", Price = 210.00m }
};

public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        //return await _httpClient
        //    .GetFromJsonAsync<List<ProductDto>>("products")
        //    ?? new List<ProductDto>();
        await Task.Delay(500); // Simulate network delay
        return _products;
    }

    public async Task<List<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<ProductDto>();

        var lowerSearchTerm = searchTerm.ToLowerInvariant();

        var filteredProducts = _products
            .Where(p => p.Name.ToLowerInvariant().Contains(lowerSearchTerm))
            .ToList();

        await Task.Delay(500);

        return filteredProducts;
    }

}
