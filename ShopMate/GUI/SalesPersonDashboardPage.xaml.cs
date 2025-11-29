using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ShopMate.GUI
{
    public sealed partial class SalesPersonDashboardPage : Page
    {
        public SalesPersonDashboardPage()
        {
            this.InitializeComponent();
        }

        private void OnAddCustomerClicked(object sender, RoutedEventArgs e)
        {
            this.Content = new AddCustomerPage();
        }

        private void OnAddProductClicked(object sender, RoutedEventArgs e)
        {
            this.Content = new AddProductPage();
        }

        private void OnGenerateBillClicked(object sender, RoutedEventArgs e)
        {
            this.Content = new GenerateBillPage();
        }
        private void OnGenerateReportClicked(object sender, RoutedEventArgs e)
        {
            // this.Content = new GenerateReportPage();
        }
        
        private void OnSignOutClicked(object sender, RoutedEventArgs e)
        {
            this.Content = new LoginPage();
        }
        private void OnDashboardClicked(object sender, RoutedEventArgs e)
        {
            this.Content = new SalesPersonDashboardPage();
        }
    }
}
