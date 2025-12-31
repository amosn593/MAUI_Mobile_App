using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Models;
using DemoApp.Services;
using System.Collections.ObjectModel;

namespace DemoApp.ViewModels;

public partial class SelectCustomerViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;

    public SelectCustomerViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
        LoadCommand.Execute(null);
    }


    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isSearching;

    public ObservableCollection<Customer> Customers { get; } = new();

    [ObservableProperty]
    private Customer? selectedCustomer;

    [RelayCommand]
    private async Task Load()
    {
        IsSearching = true;
        Customers.Clear();
        var results = await _customerService.GetCustomersAsync();
        foreach (var c in results)
            Customers.Add(c);
        IsSearching = false;
    }

    [RelayCommand]
    private async Task Search()
    {
        IsSearching = true;
        Customers.Clear();
        var results = await _customerService.SearchAsync(SearchText);
        foreach (var c in results)
            Customers.Add(c);
        IsSearching = false;
    }

    partial void OnSelectedCustomerChanged(Customer? value)
    {
        if (value == null) return;

        Select(value);
        SelectedCustomer = null; // clear selection
    }

    private async void Select(Customer customer)
    {
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            ["Customer"] = customer
        });
    }
}
