using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using ShopMate.BL;
using ShopMate.DTOs;
using ShopMate.DL;
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
                // Ensure Supabase client initialized. Try to initialize here if not.
                if (SupabaseInitializer.client == null)
                {
                    try
                    {
                        await SupabaseInitializer.InitializeAsync();
                    }
                    catch (Exception initEx)
                    {
                        var initErr = new ContentDialog
                        {
                            Title = "Initialization failed",
                            Content = $"Could not initialize backend client: {initEx.Message}\nPlease check network or app configuration.",
                            CloseButtonText = "OK",
                            XamlRoot = this.Content.XamlRoot
                        };
                        await initErr.ShowAsync();
                        return;
                    }
                }

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
                        Content = "Product could not be added. Please try again.\n(If this persists, check backend logs or network.)",
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
       
    }
}
