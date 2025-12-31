using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public Customer? Customer { get; set; }
    public List<OrderDetail> Details { get; set; } = new();
    public decimal TotalAmount => Details.Sum(d => d.SubTotal);
}

public partial class OrderDetail : ObservableObject
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SubTotal))]
    private decimal unitPrice;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SubTotal))]
    private int quantity = 1;

    public decimal SubTotal => Quantity * UnitPrice;

    public IRelayCommand IncreaseCommand { get; }
    public IRelayCommand DecreaseCommand { get; }
    public IRelayCommand RemoveCommand { get; }

    public OrderDetail(
        Action<OrderDetail> increase,
        Action<OrderDetail> decrease,
        Action<OrderDetail> remove)
    {
        IncreaseCommand = new RelayCommand(() => increase(this));
        DecreaseCommand = new RelayCommand(() => decrease(this));
        RemoveCommand = new RelayCommand( async() => remove(this));
    }

    //partial void OnQuantityChanged(int oldValue, int newValue)
    //{
    //    if (newValue < 1)
    //        Quantity = 1;
    //}
}
