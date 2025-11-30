using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopMate.GUI
{
    public sealed partial class GenerateReportPage : Page
    {
        public GenerateReportPage()
        {
            this.InitializeComponent();
            ReportTypeComboBox.SelectedIndex = 0;
        }

        private async void Preview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var preview = await BuildReportPreviewAsync();
                PreviewTextBox.Text = preview;
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Preview failed: {ex.Message}");
            }
        }

        private async void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var report = await BuildReportPreviewAsync();
                var timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                var type = (ReportTypeComboBoxSelected ?? "report").Replace(" ", "_");
                var fileName = $"ShopMate_{type}_Report_{timestamp}.txt";

                var downloads = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                if (!Directory.Exists(downloads)) downloads = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var fullPath = Path.Combine(downloads, fileName);

                await File.WriteAllTextAsync(fullPath, report, Encoding.UTF8);

                var dlg = new ContentDialog
                {
                    Title = "Report exported",
                    Content = $"Saved report to:\n{fullPath}",
                    CloseButtonText = "OK",
                    PrimaryButtonText = "Show in folder",
                    XamlRoot = this.XamlRoot
                };

                var result = await dlg.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", $"/select,\"{fullPath}\"") { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Could not generate report: {ex.Message}");
            }
        }

        private async Task<string> BuildReportPreviewAsync()
        {
            string type = ReportTypeComboBoxSelected ?? "Customers";
            var sb = new StringBuilder();
            sb.AppendLine("===== ShopMate - Report =====");
            sb.AppendLine($"Type: {type}");
            sb.AppendLine($"Generated: {DateTime.Now}");
            sb.AppendLine("------------------------------");

            // FIXED: Remove ?? because DatePicker.Date is not nullable
            var start = StartDatePicker.Date;
            var end = EndDatePicker.Date;
            sb.AppendLine($"Range: {start:yyyy-MM-dd}  →  {end:yyyy-MM-dd}");
            sb.AppendLine();

            var appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShopMate");

            // Customers
            var custFile = Path.Combine(appDir, "customers.json");
            if (File.Exists(custFile))
            {
                var text = await File.ReadAllTextAsync(custFile);
                try
                {
                    using var doc = JsonDocument.Parse(string.IsNullOrWhiteSpace(text) ? "[]" : text);
                    var arr = doc.RootElement.ValueKind == JsonValueKind.Array ? doc.RootElement : default;
                    int total = arr.ValueKind == JsonValueKind.Array ? arr.GetArrayLength() : 0;
                    sb.AppendLine($"Customers: {total}");
                }
                catch
                {
                    sb.AppendLine("Customers: (invalid json)");
                }
            }
            else sb.AppendLine("Customers: (none)");

            // Products
            var prodFile = Path.Combine(appDir, "products.json");
            if (File.Exists(prodFile))
            {
                var text = await File.ReadAllTextAsync(prodFile);
                try
                {
                    using var doc = JsonDocument.Parse(string.IsNullOrWhiteSpace(text) ? "[]" : text);
                    var arr = doc.RootElement.ValueKind == JsonValueKind.Array ? doc.RootElement : default;
                    int total = arr.ValueKind == JsonValueKind.Array ? arr.GetArrayLength() : 0;
                    sb.AppendLine($"Products: {total}");
                }
                catch
                {
                    sb.AppendLine("Products: (invalid json)");
                }
            }
            else sb.AppendLine("Products: (none)");

            sb.AppendLine();
            if (type.Equals("Summary", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine("Sales Summary");
                sb.AppendLine("Note: Bills aren't stored centrally by default.");
            }

            sb.AppendLine("------------------------------");
            sb.AppendLine("End of report");
            return sb.ToString();
        }

        private string ReportTypeComboBoxSelected => ReportTypeComboBox.SelectedItem as string;

        private async Task ShowErrorAsync(string message)
        {
            var dlg = new ContentDialog
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dlg.ShowAsync();
        }
    }
}