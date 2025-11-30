using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ShopMate.GUI
{
    public sealed partial class AdminDashboardPage : Page
    {
        public AdminDashboardPage()
        {
            this.InitializeComponent();
        }

        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            Frame? frame = window?.Content as Frame;

            frame?.Navigate(t);
        }

        private void OnManageEmployeesClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageEmployeesPage));
        }


        private void OnManageCustomersClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageCustomersPage));
        }

        private void OnManageProductsClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageProductsPage));
        }


        private void OnGenerateReportClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(GenerateReportPage));
        }

    }
}
