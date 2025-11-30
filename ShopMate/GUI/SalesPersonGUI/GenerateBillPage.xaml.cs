using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.GUI
{
    public sealed partial class GenerateBillPage : Page
    {
        private readonly BillManagementBL bmBL;
        private readonly CustomerServiceBL csBL;
        private readonly ProductManagementBL pmBL;

        public ObservableCollection<BillItemVM> BillItems { get; } = [];

        public GenerateBillPage()
        {
            InitializeComponent();
            DataContext = this;

            bmBL = new BillManagementBL();
            csBL = new CustomerServiceBL();
            pmBL = new ProductManagementBL();

            _ = LoadCustomers();
            _ = LoadProducts();
            _ = LoadKPI();
            UpdateSummary();
        }

        private async Task LoadCustomers()
        {
            CustomerComboBox.ItemsSource = await csBL.GetAllCustomers();
        }

        private async Task LoadProducts()
        {
            ProductComboBox.ItemsSource = await pmBL.GetAllProducts();
        }

        private async Task LoadKPI()
        {
            try
            {
                var totalSales = await bmBL.GetTotalSalesToday();
                TotalSalesTextBlock.Text = $"{totalSales:C}";

                var totalBills = await bmBL.GetTotalBillsToday();
                TotalBillsTextBlock.Text = totalBills.ToString();

                var avgBillVal = await bmBL.GetAverageBillValueToday();
                AvgBillValTextBlock.Text = $"{avgBillVal:C}";

                var lowStock = await bmBL.GetLowStockProductsCount();
                LowStockTextBlock.Text = lowStock.ToString();
            }
            catch
            {
                TotalSalesTextBlock.Text = "$0.00";
                TotalBillsTextBlock.Text = "0";
                AvgBillValTextBlock.Text = "$0.00";
                LowStockTextBlock.Text = "0";
            }
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is ProductDTO pDTO)
            {
                QuantityNumberBox.Maximum = pDTO.Stock;
                QuantityNumberBox.Value = 1;
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is ProductDTO selectedProd &&
                QuantityNumberBox.Value > 0)
            {
                int qty = (int)QuantityNumberBox.Value;

                var existing = BillItems.FirstOrDefault(i => i.ProductId == selectedProd.ID);

                if (existing != null)
                {
                    existing.Quantity += qty;
                }
                else
                {
                    BillItems.Add(new BillItemVM
                    {
                        ProductId = selectedProd.ID,
                        ProductName = selectedProd.Name,
                        UnitPrice = selectedProd.Price,
                        Quantity = qty
                    });
                }

                QuantityNumberBox.Value = 1;
                UpdateSummary();
            }
        }


        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is BillItemVM item)
            {
                BillItems.Remove(item);
                UpdateSummary();
            }
        }

        private void UpdateSummary()
        {
            int totalQty = BillItems.Sum(i => i.Quantity);
            decimal subtotal = BillItems.Sum(i => i.Total);

            TotalItemsTextBlock.Text = $"Items: {totalQty}";
            SubtotalTextBlock.Text = $"Subtotal: {subtotal:C}";
            TotalTextBlock.Text = $"Total: {subtotal:C}";
        }

        private async void GenerateBill_Click(object sender, RoutedEventArgs e)
        {
            if (!BillItems.Any())
            {
                await ShowDialog("Add at least one item.");
                return;
            }

            if (CustomerComboBox.SelectedItem is not CustomerDTO customer)
            {
                await ShowDialog("Select a customer.");
                return;
            }

            UpdateSummary();
            decimal total = BillItems.Sum(i => i.Total);

            var billDTO = new BillDTO
            {
                // Sales Person UserID
                UserId = 5,
                CustomerId = customer.ID,
                CreatedAt = DateTime.Now,
                Total = total
            };

            var itemDTOs = BillItems.Select(i => new BillItemDTO
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName!,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                LineTotal = i.Total
            }).ToList();

            int? billId = await bmBL.CreateBill(billDTO, itemDTOs);
            _ = LoadKPI();
            _ = LoadProducts();

            if (billId == null)
            {
                await ShowDialog("Failed to save bill.");
                return;
            }

            try
            {
                string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                string fileName = $"ShopMate_Bill_{billId}_{timestamp}.txt";

                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                if (!Directory.Exists(dir))
                    dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string path = Path.Combine(dir, fileName);

                var sb = new StringBuilder();
                sb.AppendLine("===== ShopMate - Bill =====");
                sb.AppendLine($"Bill ID: {billId}");
                sb.AppendLine($"Customer: {customer.Name}");
                sb.AppendLine($"Date: {DateTime.Now}");
                sb.AppendLine("----------------------------");
                sb.AppendLine(string.Format("{0,-20} {1,8} {2,5} {3,10}", "Product", "Price", "Qty", "Total"));

                foreach (var item in BillItems)
                    sb.AppendLine(string.Format("{0,-20} {1,8:C} {2,5} {3,10:C}", item.ProductName, item.UnitPrice, item.Quantity, item.Total));

                sb.AppendLine("----------------------------");
                sb.AppendLine($"Items: {BillItems.Sum(i => i.Quantity)}");
                sb.AppendLine($"Subtotal: {total:C}");
                sb.AppendLine($"Total: {total:C}");
                sb.AppendLine("============================");

                await File.WriteAllTextAsync(path, sb.ToString());

                var dialog = new ContentDialog
                {
                    Title = "Bill Generated",
                    Content = $"Saved to:\n{fileName}",
                    CloseButtonText = "OK",
                    PrimaryButtonText = "Show in Folder",
                    XamlRoot = XamlRoot
                };

                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    Process.Start(new ProcessStartInfo("explorer.exe", $"/select,\"{path}\"")
                    {
                        UseShellExecute = true
                    });
                }

                BillItems.Clear();
                UpdateSummary();
            }
            catch (Exception ex)
            {
                await ShowDialog($"Bill saved but file export failed.\n\n{ex.Message}");
            }
        }

        private async Task ShowDialog(string msg)
        {
            var d = new ContentDialog
            {
                Title = "Error",
                Content = msg,
                CloseButtonText = "OK",
                XamlRoot = XamlRoot
            };
            await d.ShowAsync();
        }
    }
}
