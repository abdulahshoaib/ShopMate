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

        // Properly initialize collection
        public ObservableCollection<BillItemVM> BillItems { get; } = new ObservableCollection<BillItemVM>();

        public GenerateBillPage()
        {
            this.InitializeComponent();
            DataContext = this;

            bmBL = new BillManagementBL();
            csBL = new CustomerServiceBL();
            pmBL = new ProductManagementBL();

            // Centralized async initialization
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                await LoadCustomers();
                await LoadProducts();
                await LoadKPI();
                UpdateSummary();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"InitializeAsync error: {ex}");
            }
        }

        private async Task LoadCustomers()
        {
            try
            {
                var customers = await csBL.GetAllCustomers();
                CustomerComboBox.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadCustomers error: {ex}");
                CustomerComboBox.ItemsSource = null;
            }
        }

        private async Task LoadProducts()
        {
            try
            {
                var products = await pmBL.GetAllProducts();
                ProductComboBox.ItemsSource = products;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadProducts error: {ex}");
                ProductComboBox.ItemsSource = null;
            }
        }

        private async Task LoadKPI()
        {
            try
            {
                var totalSales = await bmBL.GetTotalSalesToday();
                TotalSalesTextBlock.Text = ToPKR(totalSales);

                var totalBills = await bmBL.GetTotalBillsToday();
                TotalBillsTextBlock.Text = totalBills.ToString();

                var avgBillVal = await bmBL.GetAverageBillValueToday();
                AvgBillValTextBlock.Text = ToPKR(avgBillVal);

                var lowStock = await bmBL.GetLowStockProductsCount();
                LowStockTextBlock.Text = lowStock.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadKPI error: {ex}");
                // Fallback safe values in PKR format
                TotalSalesTextBlock.Text = ToPKR(0m);
                TotalBillsTextBlock.Text = "0";
                AvgBillValTextBlock.Text = ToPKR(0m);
                LowStockTextBlock.Text = "0";
            }
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ProductComboBox.SelectedItem is ProductDTO pDTO)
                {
                    QuantityNumberBox.Maximum = pDTO.Stock;
                    QuantityNumberBox.Value = 1;
                }
                else
                {
                    QuantityNumberBox.Maximum = 1;
                    QuantityNumberBox.Value = 1;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ProductComboBox_SelectionChanged error: {ex}");
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductComboBox.SelectedItem is ProductDTO selectedProd &&
                    QuantityNumberBox.Value > 0)
                {
                    int qty = (int)QuantityNumberBox.Value;

                    var existing = BillItems.FirstOrDefault(i => i.ProductId == selectedProd.ID);

                    if (existing != null)
                    {
                        existing.Quantity += qty;
                        // If BillItemVM does not notify Total automatically, ensure UI refresh (depends on VM implementation)
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
            catch (Exception ex)
            {
                Debug.WriteLine($"AddItem_Click error: {ex}");
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button btn && btn.DataContext is BillItemVM item)
                {
                    BillItems.Remove(item);
                    UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RemoveItem_Click error: {ex}");
            }
        }

        private void UpdateSummary()
        {
            try
            {
                int totalQty = BillItems.Sum(i => i.Quantity);

                // compute subtotal using UnitPrice * Quantity for safety
                decimal subtotal = BillItems.Sum(i => (i.UnitPrice * i.Quantity));

                TotalItemsTextBlock.Text = $"Items: {totalQty}";
                SubtotalTextBlock.Text = $"Subtotal: {ToPKR(subtotal)}";
                TotalTextBlock.Text = $"Total: {ToPKR(subtotal)}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UpdateSummary error: {ex}");
                TotalItemsTextBlock.Text = "Items: 0";
                SubtotalTextBlock.Text = $"Subtotal: {ToPKR(0m)}";
                TotalTextBlock.Text = $"Total: {ToPKR(0m)}";
            }
        }

        private async void GenerateBill_Click(object sender, RoutedEventArgs e)
        {
            try
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
                decimal total = BillItems.Sum(i => (i.UnitPrice * i.Quantity));

                var billDTO = new BillDTO
                {
                    // TODO: replace with logged-in user id
                    UserId = 5,
                    CustomerId = customer.ID,
                    CreatedAt = DateTime.Now,
                    Total = total
                };

                var itemDTOs = BillItems.Select(i => new BillItemDTO
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName ?? string.Empty,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    LineTotal = i.UnitPrice * i.Quantity
                }).ToList();

                int? billId = await bmBL.CreateBill(billDTO, itemDTOs);

                // refresh KPIs and products regardless of success/failure of export
                await LoadKPI();
                await LoadProducts();

                if (billId == null)
                {
                    await ShowDialog("Failed to save bill.");
                    return;
                }

                // attempt to export the bill to a text file
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
                    sb.AppendLine(string.Format("{0,-20} {1,12} {2,6} {3,14}", "Product", "UnitPrice", "Qty", "LineTotal"));

                    foreach (var item in BillItems)
                    {
                        decimal lineTotal = item.UnitPrice * item.Quantity;
                        // Format: product, Rs unitprice, qty, Rs linetotal
                        sb.AppendLine(string.Format("{0,-20} {1,12} {2,6} {3,14}",
                            TruncateString(item.ProductName, 20),
                            ToPKRInline(item.UnitPrice),
                            item.Quantity,
                            ToPKRInline(lineTotal)
                        ));
                    }

                    sb.AppendLine("----------------------------");
                    sb.AppendLine($"Items: {BillItems.Sum(i => i.Quantity)}");
                    sb.AppendLine($"Subtotal: {ToPKR(total)}");
                    sb.AppendLine($"Total: {ToPKR(total)}");
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
                    Debug.WriteLine($"GenerateBill export error: {ex}");
                    await ShowDialog($"Bill saved but file export failed.\n\n{ex.Message}");
                    BillItems.Clear();
                    UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GenerateBill_Click error: {ex}");
                await ShowDialog("An unexpected error occurred while generating the bill.");
            }
        }

        private static string ToPKR(decimal amount)
        {
            // returns "Rs 1,234.56"
            return $"Rs {amount:N2}";
        }

        private static string ToPKRInline(decimal amount)
        {
            // returns "Rs 1,234.56" but without wide padding characters (useful for formatted columns)
            return $"Rs {amount:N2}";
        }

        private static string TruncateString(string? text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            if (text.Length <= maxLength) return text;
            return text.Substring(0, maxLength - 1) + "…";
        }

        private async Task ShowDialog(string msg)
        {
            var d = new ContentDialog
            {
                Title = "Notice",
                Content = msg,
                CloseButtonText = "OK",
                XamlRoot = XamlRoot
            };
            await d.ShowAsync();
        }
    }
}
