using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Collections.Generic;

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class UpdateProductPage : Page
    {
        private List<ProductDTO> products = new();
        private ProductDTO selectedProduct = new();
        private readonly ProductManagementBL pmBL = new();

        public UpdateProductPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            products = await pmBL.GetAllProducts();
            ProductComboBox.ItemsSource = products;
            ProductComboBox.DisplayMemberPath = "Name";
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is ProductDTO p)
            {
                selectedProduct = p;

                ProductNameTextBox.Text = p.Name;
                DescriptionTextBox.Text = p.Description;
                QuantityNumberBox.Value = p.Stock;
                PriceNumberBox.Value = (double)p.Price;
            }
        }

        private async void OnSaveProductClicked(object sender, RoutedEventArgs e)
        {
            bool fail = false;

            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text))
            {
                ProductNameTextBox.BorderBrush = Red();
                fail = true;
            }
            else ProductNameTextBox.BorderBrush = White();

            if (double.IsNaN(QuantityNumberBox.Value))
            {
                QuantityNumberBox.BorderBrush = Red();
                fail = true;
            }
            else QuantityNumberBox.BorderBrush = White();

            if (double.IsNaN(PriceNumberBox.Value))
            {
                PriceNumberBox.BorderBrush = Red();
                fail = true;
            }
            else PriceNumberBox.BorderBrush = White();

            if (fail) return;

            selectedProduct.Name = ProductNameTextBox.Text.Trim();
            selectedProduct.Description = DescriptionTextBox.Text.Trim();
            selectedProduct.Stock = (int)QuantityNumberBox.Value;
            selectedProduct.Price = (decimal)PriceNumberBox.Value;

            bool success = await pmBL.UpdateProduct(selectedProduct);

            if (success)
            {
                await ShowDialog("Success", "Product updated successfully.");
                LoadProducts();
            }
            else
            {
                await ShowDialog("Error", "Failed to update product.");
            }
        }

        private SolidColorBrush Red() =>
            new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));

        private SolidColorBrush White() =>
            new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));

        private async System.Threading.Tasks.Task ShowDialog(string title, string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private void OnBackClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageProductsPage));
        }

        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            var frame = window?.Content as Frame;

            frame?.Navigate(t);
        }
    }
}
