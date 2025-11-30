using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class UpdateProductPage : Page
    {
        private List<ProductDTO> products = [];
        private ProductDTO selectedProduct = new ProductDTO();
        private readonly ProductManagementBL pmBL;
        public UpdateProductPage()
        {
            InitializeComponent();

            this.pmBL = new ProductManagementBL();
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
                QuantityTextBox.Text = p.Stock.ToString();
                PriceTextBox.Text = p.Price.ToString("0.00");
            }
        }

        private async void OnSaveProductClicked(object sender, RoutedEventArgs e)
        {
            if (selectedProduct is null)
            {
                await ShowDialog("Error", "Please select a product to edit.");
                return;
            }

            // Update values
            selectedProduct.Name = ProductNameTextBox.Text;
            selectedProduct.Description = DescriptionTextBox.Text;

            if (int.TryParse(QuantityTextBox.Text, out int qty))
                selectedProduct.Stock = qty;

            if (decimal.TryParse(PriceTextBox.Text, out decimal price))
                selectedProduct.Price = price;

            // Save
            bool success = await pmBL.UpdateProduct(selectedProduct);

            if (success)
            {
                await ShowDialog("Saved", "Product updated successfully.");
                LoadProducts(); // refresh combo
            }
            else
            {
                await ShowDialog("Error", "Failed to update product.");
            }
        }

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
    }
}
