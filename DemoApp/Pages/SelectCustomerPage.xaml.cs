using DemoApp.ViewModels;

namespace DemoApp.Pages;

public partial class SelectCustomerPage : ContentPage
{
	public SelectCustomerPage(SelectCustomerViewModel selectCustomerViewModel)
	{
		InitializeComponent();
		BindingContext = selectCustomerViewModel;
    }
}