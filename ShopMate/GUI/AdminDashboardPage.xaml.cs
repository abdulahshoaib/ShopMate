using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ShopMate.GUI
{
    public sealed partial class AdminDashboardPage : Page
    {
        public AdminDashboardPage()
        {
            this.InitializeComponent();
        }

        

        private void OnManageEmployeesClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to ManageEmployeesPage
        }
        

        private void OnManageCustomersClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to AddCustomerPage
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
        }
        private void OnDashboardClicked(object sender, RoutedEventArgs e)
        {
        }
    }
}
