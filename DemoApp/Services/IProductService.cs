using DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetProductsAsync();
    Task<List<ProductDto>> SearchProductsAsync(string searchTerm);
}
