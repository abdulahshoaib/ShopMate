using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using ShopMate.GUI.AdminGUI;

namespace ShopMate.GUI.Controls
{
    public sealed partial class AdminSidebarControl : UserControl
    {
        public AdminSidebarControl()
        {
            this.InitializeComponent();
        }

        private void OnDashboardClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(AdminDashboardPage));

        private void OnManageEmployeesClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(ManageEmployeesPage));

        private void OnManageCustomersClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(ManageCustomersPage));

        private void OnManageProductsClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(ManageProductsPage));

        private void OnSignOutClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(LoginPage));

        private void OnReportClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(ReportPage));
        
        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            Frame? frame = window?.Content as Frame;

            frame?.Navigate(t);
        }

        private void OnSettingsClicked(object sender, RoutedEventArgs e)
            => Navigate(typeof(SettingsPage));
    }
}