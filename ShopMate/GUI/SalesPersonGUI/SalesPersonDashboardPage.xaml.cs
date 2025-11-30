using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ShopMate.GUI
{
    public sealed partial class SalesPersonDashboardPage : Page
    {
        public SalesPersonDashboardPage()
        {
            this.InitializeComponent();

            WelcomeTitle.Text = GlobalSession.DisplayUsername;
            DashboardDay.Text = DateTime.Now.ToString("dddd");
            DashboardDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
        }
        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            Frame? frame = window?.Content as Frame;

            frame?.Navigate(t);
        }
        private void OnAddCustomerClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(SalesAddCustomer));
        }

        private void OnAddProductClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(SalesAddProduct));
        }

        private void OnGenerateBillClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(GenerateBillPage));
        }

        private void OnUpdateStockClicked(object sender, RoutedEventArgs e)
        {
            // Navigate(typeof(UpdateStockPage));
        }
    }
}
