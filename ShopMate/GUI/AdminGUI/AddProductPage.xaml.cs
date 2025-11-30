using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Threading.Tasks;

namespace ShopMate.GUI
{
    public sealed partial class AddProductPage : Page
    {
        private readonly ProductManagementBL pmBL = new ProductManagementBL();

        public AddProductPage()
        {
            this.InitializeComponent();
        }

        private async void OnAddProductClicked(object sender, RoutedEventArgs e)
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

            int quantity = (int)QuantityNumberBox.Value;
            decimal price = (decimal)PriceNumberBox.Value;

            ProductDTO newProduct = new ProductDTO
            {
                Name = ProductNameTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim(),
                Stock = quantity,
                Price = price
            };

            bool success = await pmBL.AddProduct(newProduct);

            if (!success)
            {
                await ShowDialog("Error", "Failed to add product.");
                return;
            }

            await ShowDialog("Success", "Product added successfully!");
            ClearFields();
        }

        private SolidColorBrush Red() =>
            new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));

        private SolidColorBrush White() =>
            new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));

        private void ClearFields()
        {
            ProductNameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            QuantityNumberBox.Value = double.NaN;
            PriceNumberBox.Value = double.NaN;

            ProductNameTextBox.BorderBrush = White();
            DescriptionTextBox.BorderBrush = White();
            QuantityNumberBox.BorderBrush = White();
            PriceNumberBox.BorderBrush = White();
        }

        private async Task ShowDialog(string title, string message)
        {
            var dlg = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dlg.ShowAsync().AsTask();
        }
    }
}
