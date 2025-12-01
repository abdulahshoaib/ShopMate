using System;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using static ShopMate.GUI.AdminGUI.ViewInventory;

namespace ShopMate.GUI.SalesPersonGUI
{
    public sealed partial class SalesViewInventory : Page
    {
        private readonly ProductManagementBL pmBL;

        public SalesViewInventory()
        {
            InitializeComponent();
            this.pmBL = new ProductManagementBL();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var list = await pmBL.GetAllProducts();
            var rows = new List<ProductRow>();

            bool alt = false;
            foreach (var p in list)
            {
                rows.Add(new ProductRow(p)
                {
                    RowColor = alt ? Windows.UI.Color.FromArgb(20, 255, 255, 255)
                                   : Windows.UI.Color.FromArgb(30, 255, 255, 255)
                });

                alt = !alt;
            }

            ProductsRepeater.ItemsSource = rows;
        }

        private void OnBackClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(SalesPersonDashboardPage));
        }

        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            var frame = window?.Content as Frame;

            frame?.Navigate(t);
        }
    }
}
