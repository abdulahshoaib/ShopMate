using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Collections.Generic;

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class DeleteProductPage : Page
    {
        private readonly ProductManagementBL pmBL;
        private List<ProductDTO> products = [];
        public DeleteProductPage()
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
                ProductNameTextBox.Text = p.Name;
                DescriptionTextBox.Text = p.Description;
                QuantityTextBox.Text = p.Stock.ToString();
                PriceTextBox.Text = p.Price.ToString("0.00");
            }
        }

        private async void OnDeleteProductClicked(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is not ProductDTO product)
            {
                await ShowDialog("Error", "Please select a product to delete.");
                return;
            }

            // Confirm delete
            var confirmDialog = new ContentDialog
            {
                Title = "Confirm Deletion",
                Content = $"Are you sure you want to delete '{product.Name}'?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
                return;

            // DELETE
            bool success = await pmBL.RemoveProduct(product);

            if (success)
            {
                await ShowDialog("Deleted", "Product deleted successfully.");

                LoadProducts(); // refresh list
                ClearFields();
            }
            else
            {
                await ShowDialog("Error", "Failed to delete product.");
            }
        }

        private void ClearFields()
        {
            ProductNameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            QuantityTextBox.Text = "";
            PriceTextBox.Text = "";
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
