using DemoApp.Pages;

namespace DemoApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateOrderPage), typeof(CreateOrderPage));
            Routing.RegisterRoute(nameof(SelectCustomerPage), typeof(SelectCustomerPage));
        }
    }
}
