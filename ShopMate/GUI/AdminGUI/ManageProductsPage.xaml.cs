using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.GUI.AdminGUI;
using System;

namespace ShopMate.GUI
{
    public sealed partial class ManageProductsPage : Page
    {
        public ManageProductsPage()
        {
            this.InitializeComponent();
        }
        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            Frame? frame = window?.Content as Frame;

            frame?.Navigate(t);
        }

        private void OnEditProductClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(UpdateProductPage));
        }


        private void OnAddProductClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(AddProductPage));
        }



        private void OnDeleteProductClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(DeleteProductPage));
        }

        private void OnViewProductsClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ViewInventory));
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
