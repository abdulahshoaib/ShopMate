using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ShopMate.Pages
{
    public sealed partial class ManageCustomersPage : Page
    {
        public ManageCustomersPage()
        {
            this.InitializeComponent();
        }

        private void OnBackClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate back to SalesPersonDashboardPage
        }

        private void OnAddCustomerClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to AddCustomerPage
        }

        private void OnEditCustomerClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Edit selected customer
        }

        private void OnDeleteCustomerClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Delete selected customer
        }
    }

    // Example data model
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
