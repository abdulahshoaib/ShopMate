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
        private void OnGenerateReportClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(SalesGenerateReport));
        }
    }
}
