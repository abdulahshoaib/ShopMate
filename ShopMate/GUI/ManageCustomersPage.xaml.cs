using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.VoiceCommands;

namespace ShopMate.GUI
{
    public sealed partial class ManageCustomersPage : Page
    {
        public ManageCustomersPage()
        {
            this.InitializeComponent();
        }
        private void OnManageEmployeesClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to ManageEmployeesPage
        }
        private void OnEditCustomerClicked(object sender, RoutedEventArgs e)
        {

        }
        private void OnManageProductsClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to AddProductPage
        }
        private void OnGenerateReportClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to GenerateBillPage
        }
        private void OnSettingsClicked(object sender, RoutedEventArgs e)
        {
            // TODO:
        }
        private void OnSignOutClicked(object sender, RoutedEventArgs e)
        {
            this.Content = new GUI.LoginPage();
        }
        private void OnDashboardClicked(object sender, RoutedEventArgs e)
        {
            this.Content = new GUI.AdminDashboardPage();
        }

        private void OnAddCustomerClicked(object sender, RoutedEventArgs e)
        {
            this.Content = new GUI.AddCustomerPage();
        }

        

        private void OnDeleteCustomerClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Delete selected customer
        }
        
        private void OnViewCustomersClicked(object sender, RoutedEventArgs e)
        {
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
}
