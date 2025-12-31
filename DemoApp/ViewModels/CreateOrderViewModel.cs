

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Models;
using DemoApp.Pages;
using DemoApp.Services;
using System.Collections.ObjectModel;

namespace DemoApp.ViewModels;

[QueryProperty(nameof(Customer), nameof(Customer))]
public partial class CreateOrderViewModel : ObservableObject
{
    private readonly IProductService _productService;

    public CreateOrderViewModel(IProductService productService)
    {
        _productService = productService;
        //LoadProductsCommand.Execute(null);
    }

    // ---------- Search ----------
    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isSearching;

    public ObservableCollection<ProductDto> Products { get; } = new();
    
    public ObservableCollection<OrderDetail> OrderDetails { get; } = new();

    [ObservableProperty]
    private ProductDto? selectedProduct;

    [ObservableProperty]
    private Customer? customer;

    //[ObservableProperty]
    //private int quantity = 1;

    public decimal TotalAmount => OrderDetails.Sum(x => x.SubTotal);

    [RelayCommand]
    private async Task SearchProducts()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return;

        IsSearching = true;
        Products.Clear();

        var results = await _productService.SearchProductsAsync(SearchText);

        // Auto-add if only one product
        if (results.Count == 1)
        {
            AddOrIncrease(results[0]);
            SearchText = string.Empty;
        }
        else
        {
            foreach (var p in results)
                Products.Add(p);
        }

        IsSearching = false;
    }

    private void AddOrIncrease(ProductDto product)
    {
        var existing = OrderDetails
            .FirstOrDefault(x => x.ProductId == product.Id);

        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            var item = new OrderDetail(
                increase: IncreaseQuantity,
                decrease: DecreaseQuantity,
                remove: RemoveItem
                )
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = 1
            };

            OrderDetails.Add(item);

            // Recalculate total when quantity changes
            //item.PropertyChanged += (_, e) =>
            //{
            //    if (e.PropertyName == nameof(OrderDetail.Quantity))
            //        OnPropertyChanged(nameof(TotalAmount));
            //};

            
        }

        OnPropertyChanged(nameof(TotalAmount));
    }

    [RelayCommand]
    private void AddSelectedProduct(ProductDto? product)
    {
        if (product == null)
            return;

        AddOrIncrease(product);
        Products.Clear();
        SearchText = string.Empty;
    }

    [RelayCommand]
    private void IncreaseQuantity(OrderDetail item)
    {
        item.Quantity++;
        OnPropertyChanged(nameof(TotalAmount));
    }

    [RelayCommand]
    private void DecreaseQuantity(OrderDetail item)
    {
        if (item.Quantity > 1)
        {
            item.Quantity--;
            OnPropertyChanged(nameof(TotalAmount));
        }
    }

    [RelayCommand]
    private void RemoveItem(OrderDetail item)
    {
        //bool confirm = await Application.Current!.MainPage.DisplayAlert(
        //    "Remove Item",
        //    $"Remove {item.ProductName}?",
        //    "Yes", "No");

        //if (!confirm) return;

        OrderDetails.Remove(item);
        OnPropertyChanged(nameof(TotalAmount));
    }


    [RelayCommand]
    private async Task SubmitOrder()
    {
        var order = new Order
        {
            Details = OrderDetails.ToList()
        };

        // TODO: Send order to API or database
        await Application.Current!.MainPage.DisplayAlertAsync(
            "Order Created",
            $"Items: {order.Details.Count}\nTotal: {order.TotalAmount:C}",
            "OK"
        );

        OrderDetails.Clear();
        OnPropertyChanged(nameof(TotalAmount));
    }

    [RelayCommand]
    private async Task SelectCustomer()
    {
        await Shell.Current.GoToAsync(nameof(SelectCustomerPage));
    }
}

