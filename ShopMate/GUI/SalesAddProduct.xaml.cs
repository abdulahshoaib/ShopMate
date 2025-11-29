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
    public sealed partial class SalesAddProduct : Page
    {
        private readonly ProductManagementBL pmBL = new ProductManagementBL();

        public SalesAddProduct()
        {
            this.InitializeComponent();
        }

        private async void OnAddProductClicked(object sender, RoutedEventArgs e)
        {
            bool hasError = false;

            int stock = 0;
            decimal price = 0m;

            // ---------------------------
            // Safe trimmed values
            // ---------------------------
            string name = (ProductNameTextBox.Text ?? string.Empty).Trim();
            string desc = (DescriptionTextBox.Text ?? string.Empty).Trim();
            string qty = (QuantityTextBox.Text ?? string.Empty).Trim();
            string pr = (PriceTextBox.Text ?? string.Empty).Trim();

            // ---------------------------
            // Name validation
            // ---------------------------
            if (string.IsNullOrEmpty(name))
            {
                ProductNameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                ProductNameTextBox.Focus(FocusState.Programmatic);
                hasError = true;
            }
            else
            {
                ProductNameTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            }

            // ---------------------------
            // Quantity validation
            // ---------------------------
            if (!int.TryParse(qty, out stock) || stock < 0)
            {
                QuantityTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                QuantityTextBox.Focus(FocusState.Programmatic);
                hasError = true;
            }
            else
            {
                QuantityTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            }

            // ---------------------------
            // Price validation
            // ---------------------------
            if (!decimal.TryParse(pr, out price) || price < 0m)
            {
                PriceTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                PriceTextBox.Focus(FocusState.Programmatic);
                hasError = true;
            }
            else
            {
                PriceTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            }

            // ---------------------------
            // If validation fails
            // ---------------------------
            if (hasError)
            {
                var dialog = new ContentDialog
                {
                    Title = "Validation",
                    Content = "Please fix the highlighted fields.",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                await dialog.ShowAsync();
                return;
            }

            // ---------------------------
            // Build DTO safely
            // ---------------------------
            var dto = new ProductDTO
            {
                Name = name,
                Description = desc,
                Price = price,
                Stock = stock,
                ExpiryDate = null,
                LowStockLimit = 5
            };

            try
            {
                bool added = await pmBL.AddProduct(dto);

                if (added)
                {
                    var okDialog = new ContentDialog
                    {
                        Title = "Product Added",
                        Content = $"Product '{dto.Name}' was added successfully.",
                        CloseButtonText = "OK",
                        XamlRoot = this.Content.XamlRoot
                    };
                    await okDialog.ShowAsync();

                    // Clear fields
                    ProductNameTextBox.Text = "";
                    DescriptionTextBox.Text = "";
                    QuantityTextBox.Text = "";
                    PriceTextBox.Text = "";
                }
                else
                {
                    var failDialog = new ContentDialog
                    {
                        Title = "Add Failed",
                        Content = "Product could not be added. Please try again.",
                        CloseButtonText = "OK",
                        XamlRoot = this.Content.XamlRoot
                    };
                    await failDialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"An error occurred while adding product:\n{ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }

        // ---------------------------
        // Helper methods (optional)
        // ---------------------------
        private async Task<bool> UpdateProductAsync(ProductDTO dto)
        {
            if (dto == null) return false;

            try
            {
                return await pmBL.UpdateProduct(dto);
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> RemoveProductByIdAsync(int id)
        {
            if (id <= 0) return false;

            try
            {
                var dto = new ProductDTO { ID = id };
                return await pmBL.RemoveProduct(dto);
            }
            catch
            {
                return false;
            }
        }
    }
}
