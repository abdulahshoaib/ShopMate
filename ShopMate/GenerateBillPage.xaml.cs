using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace ShopMate
{
    public sealed partial class GenerateBillPage : Page
    {
        public ObservableCollection<BillItem> BillItems { get; } = new ObservableCollection<BillItem>();

        public GenerateBillPage()
        {
            this.InitializeComponent();
            this.DataContext = this;

            // Example product list for testing (optional)
            ProductComboBox.Items.Add("Sample Product A|10.50");
            ProductComboBox.Items.Add("Sample Product B|5.00");
            UpdateSummary();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is string productToken &&
                !string.IsNullOrWhiteSpace(QuantityTextBox.Text) &&
                int.TryParse(QuantityTextBox.Text, out int qty) && qty > 0)
            {
                var parts = productToken.Split('|');
                var name = parts[0];
                decimal price = 0m;
                if (parts.Length > 1) decimal.TryParse(parts[1], out price);

                var item = new BillItem
                {
                    ProductName = name,
                    Price = price,
                    Quantity = qty
                };
                BillItems.Add(item);
                UpdateSummary();
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is BillItem item)
            {
                BillItems.Remove(item);
                UpdateSummary();
            }
        }

        private void GenerateBill_Click(object sender, RoutedEventArgs e)
        {
            // Minimal action to avoid build/runtime errors: just update summary
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            int totalItems = BillItems.Sum(i => i.Quantity);
            decimal subtotal = BillItems.Sum(i => i.Total);

            TotalItemsTextBlock.Text = $"Items: {totalItems}";
            SubtotalTextBlock.Text = $"Subtotal: {subtotal.ToString("C", CultureInfo.CurrentCulture)}";
            TotalTextBlock.Text = $"Total: {subtotal.ToString("C", CultureInfo.CurrentCulture)}";
        }
    }

    // kept inside same file per request — no new files
    public class BillItem
    {
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;

        // Preformatted strings to avoid using StringFormat or converters in XAML
        public string FormattedPrice => Price.ToString("C", CultureInfo.CurrentCulture);
        public string FormattedTotal => Total.ToString("C", CultureInfo.CurrentCulture);
    }
}
