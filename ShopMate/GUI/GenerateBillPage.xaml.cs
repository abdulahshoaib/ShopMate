using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ShopMate.GUI
{
    public sealed partial class GenerateBillPage : Page
    {
        // Observable collection to bind to ListView
        public ObservableCollection<BillItem> BillItems { get; } = new ObservableCollection<BillItem>();

        public GenerateBillPage()
        {
            this.InitializeComponent();
            this.DataContext = this;

            // Example products for testing
            ProductComboBox.Items.Add("Sample Product A|10.50");
            ProductComboBox.Items.Add("Sample Product B|5.00");

            UpdateSummary();
        }

        // ----------------- ADD ITEM -----------------
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is string productToken &&
                !string.IsNullOrWhiteSpace(QuantityTextBox.Text) &&
                int.TryParse(QuantityTextBox.Text, out int qty) &&
                qty > 0)
            {
                var parts = productToken.Split('|');
                var name = parts[0];
                decimal price = 0;
                if (parts.Length > 1) decimal.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out price);

                BillItems.Add(new BillItem
                {
                    ProductName = name,
                    Price = price,
                    Quantity = qty
                });

                UpdateSummary();
            }
        }

        // ----------------- REMOVE ITEM -----------------
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is BillItem item)
            {
                BillItems.Remove(item);
                UpdateSummary();
            }
        }

        // ----------------- UPDATE BILL SUMMARY -----------------
        private void UpdateSummary()
        {
            int totalItems = BillItems.Sum(i => i.Quantity);
            decimal subtotal = BillItems.Sum(i => i.Total);

            TotalItemsTextBlock.Text = $"Items: {totalItems}";
            SubtotalTextBlock.Text = $"Subtotal: {subtotal:C}";
            TotalTextBlock.Text = $"Total: {subtotal:C}";
        }

        // ----------------- GENERATE BILL -----------------
        private async void GenerateBill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateSummary();

                string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                string fileName = $"ShopMate_Bill_{timestamp}.txt";

                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                if (!Directory.Exists(downloadsPath)) downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string fullPath = Path.Combine(downloadsPath, fileName);

                // Build bill content
                var sb = new StringBuilder();
                sb.AppendLine("===== ShopMate - Bill =====");
                sb.AppendLine($"Date: {DateTime.Now}");
                sb.AppendLine("----------------------------");
                sb.AppendLine("Product\tPrice\tQty\tTotal");

                foreach (var item in BillItems)
                {
                    sb.AppendLine($"{item.ProductName}\t{item.Price:C}\t{item.Quantity}\t{item.Total:C}");
                }

                sb.AppendLine("----------------------------");
                sb.AppendLine(TotalItemsTextBlock.Text);
                sb.AppendLine(SubtotalTextBlock.Text);
                sb.AppendLine(TotalTextBlock.Text);
                sb.AppendLine("============================");

                // Save the file
                await File.WriteAllTextAsync(fullPath, sb.ToString());

                // Show confirmation dialog
                var dialog = new ContentDialog
                {
                    Title = "Bill exported",
                    Content = $"Saved bill to Downloads as:\n{fileName}",
                    CloseButtonText = "OK",
                    PrimaryButtonText = "Show in folder",
                    XamlRoot = this.XamlRoot   
                };

                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    // Open folder and select file
                    Process.Start(new ProcessStartInfo("explorer.exe", $"/select,\"{fullPath}\"") { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                var err = new ContentDialog
                {
                    Title = "Export failed",
                    Content = $"Could not generate bill: {ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await err.ShowAsync();
            }
        }

        // ----------------- SIDEBAR BUTTON HANDLERS (PLACEHOLDER) -----------------
        private void OnDashboardClicked(object sender, RoutedEventArgs e) { }
        private void OnGenerateBillClicked(object sender, RoutedEventArgs e) { }
        private void OnGenerateReportClicked(object sender, RoutedEventArgs e) { }
        private void OnAddCustomerClicked(object sender, RoutedEventArgs e) { }
        private void OnAddProductClicked(object sender, RoutedEventArgs e) { }
        private void OnSettingsClicked(object sender, RoutedEventArgs e) { }
        private void OnSignOutClicked(object sender, RoutedEventArgs e) { }
    }

    // ----------------- BILL ITEM MODEL -----------------
    public class BillItem
    {
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;

        public string FormattedPrice => Price.ToString("C", CultureInfo.CurrentCulture);
        public string FormattedTotal => Total.ToString("C", CultureInfo.CurrentCulture);
    }
}
