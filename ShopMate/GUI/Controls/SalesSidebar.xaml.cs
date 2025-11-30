using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.GUI.SalesPersonGUI;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ShopMate.GUI.Controls
{
    public sealed partial class SalesSidebarControl : UserControl
    {
        public SalesSidebarControl()
        {
            this.InitializeComponent();
        }

        private void OnDashboardClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(SalesPersonDashboardPage));

        private void OnGenerateBillClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(GenerateBillPage));

        private void OnGenerateReportClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(SalesGenerateReport));

        private void OnAddCustomerClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(SalesAddCustomer));

        private void OnAddProductClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(SalesAddProduct));

        private void OnViewInventorClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(SalesViewInventory));
        private void OnSignOutClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(LoginPage));

        private void Navigate(Type t)
        {
            // Try to get the main window's frame from the current window's content
            var window = (Application.Current as App)?._window;
            Frame? frame = window?.Content as Frame;

            frame?.Navigate(t);
        }
    }
}