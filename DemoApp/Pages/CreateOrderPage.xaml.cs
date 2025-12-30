using DemoApp.ViewModels;

namespace DemoApp.Pages;

public partial class CreateOrderPage : ContentPage
{
	public CreateOrderPage(CreateOrderViewModel createOrderViewModel)
	{
		InitializeComponent();
		BindingContext = createOrderViewModel;
    }
}