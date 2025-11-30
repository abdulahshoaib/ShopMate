using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.VoiceCommands;
using System;
using ShopMate.GUI.AdminGUI;

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

        }


        private void OnAddProductClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(AddProductPage));
        }



        private void OnDeleteProductClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Delete selected customer
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
