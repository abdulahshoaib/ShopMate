using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ShopMate.GUI
{
    public sealed partial class SettingsPage : Page
    {
        private readonly string _settingsFilePath;

        public SettingsPage()
        {
            InitializeComponent();
            _settingsFilePath = GetSettingsFilePath();
            _ = LoadSettingsAsync();
        }

        private async void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(TaxRateTextBox.Text ?? "0", out decimal tax) || tax < 0)
            {
                await ShowErrorAsync("Enter a valid non-negative tax rate.");
                return;
            }

            var settings = new AppSettings
            {
                UseDarkTheme = DarkThemeToggle.IsOn,
                DefaultTaxRate = tax,
                Currency = CurrencyComboBox.SelectedItem as string ?? "USD"
            };

            try
            {
                var dir = Path.GetDirectoryName(_settingsFilePath);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                await File.WriteAllTextAsync(_settingsFilePath, JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
                var dlg = new ContentDialog
                {
                    Title = "Settings saved",
                    Content = "Application settings have been saved.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dlg.ShowAsync();
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Could not save settings: {ex.Message}");
            }
        }

        private async void ResetSettings_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ContentDialog
            {
                Title = "Reset settings",
                Content = "Reset settings to defaults?",
                PrimaryButtonText = "Reset",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                ApplyDefaults();
                try
                {
                    if (File.Exists(_settingsFilePath)) File.Delete(_settingsFilePath);
                }
                catch { }
            }
        }

        private async Task LoadSettingsAsync()
        {
            try
            {
                if (!File.Exists(_settingsFilePath))
                {
                    ApplyDefaults();
                    return;
                }

                var json = await File.ReadAllTextAsync(_settingsFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                DarkThemeToggle.IsOn = settings.UseDarkTheme;
                TaxRateTextBox.Text = settings.DefaultTaxRate.ToString();
                CurrencyComboBox.SelectedItem = settings.Currency;
            }
            catch
            {
                ApplyDefaults();
            }
        }

        private void ApplyDefaults()
        {
            DarkThemeToggle.IsOn = true;
            TaxRateTextBox.Text = "0";
            CurrencyComboBox.SelectedItem = "USD";
        }

        private string GetSettingsFilePath()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShopMate");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "settings.json");
        }

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

        private sealed class AppSettings
        {
            public bool UseDarkTheme { get; set; } = true;
            public decimal DefaultTaxRate { get; set; } = 0;
            public string Currency { get; set; } = "USD";
        }
    }
}