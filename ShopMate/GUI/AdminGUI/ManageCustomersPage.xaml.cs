using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.GUI.AdminGUI;
using System;

namespace ShopMate.GUI
{
    public sealed partial class ManageCustomersPage : Page
    {
        public ManageCustomersPage()
        {
            this.InitializeComponent();
        }
        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            Frame? frame = window?.Content as Frame;

            frame?.Navigate(t);
        }

        private void OnEditCustomerClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(UpdateCustomerPage));
        }


        private void OnAddCustomerClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(AddCustomerPage));
        }



        private void OnDeleteCustomerClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(DeleteCustomerPage));
        }

        private void OnViewCustomersClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ViewCustomerPage));
        }

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
