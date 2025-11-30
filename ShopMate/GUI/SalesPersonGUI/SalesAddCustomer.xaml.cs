using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Text.RegularExpressions;

namespace ShopMate.GUI
{
    public sealed partial class SalesAddCustomer : Page
    {
        private readonly CustomerServiceBL csBL = new CustomerServiceBL();

        public SalesAddCustomer()
        {
            this.InitializeComponent();
        }

        private async void OnAddCustomerClicked(object sender, RoutedEventArgs e)
        {
            bool hasError = false;

            // ---------------------------
            // Safe trimmed values
            // ---------------------------
            string name = (NameTextBox.Text ?? string.Empty).Trim();
            string phone = (PhoneTextBox.Text ?? string.Empty).Trim();
            string ageRaw = (AgeTextBox.Text ?? string.Empty).Trim();
            string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Male";
            string address = (AddressTextBox.Text ?? string.Empty).Trim();

            // ---------------------------
            // Name validation
            // ---------------------------
            if (string.IsNullOrEmpty(name))
            {
                NameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                NameTextBox.Focus(FocusState.Programmatic);
                hasError = true;
            }
            else
            {
                NameTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            }

            // ---------------------------
            // Phone validation (basic regex)
            // ---------------------------
            if (string.IsNullOrEmpty(phone) || !Regex.IsMatch(phone, @"^\+?\d{7,15}$"))
            {
                PhoneTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                if (!hasError) PhoneTextBox.Focus(FocusState.Programmatic);
                hasError = true;
            }
            else
            {
                PhoneTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            }

            // ---------------------------
            // Age validation
            // ---------------------------
            if (!int.TryParse(ageRaw, out int age) || age < 5)
            {
                AgeTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                if (!hasError) AgeTextBox.Focus(FocusState.Programmatic);
                hasError = true;
            }
            else
            {
                AgeTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            }

            // ---------------------------
            // Address is optional for sales flow — do not fail validation when empty
            // ---------------------------
            AddressTextBox.BorderBrush = new SolidColorBrush(Colors.White);

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
            // Build DTO
            // ---------------------------
            var dto = new CustomerDTO
            {
                Name = name,
                Phone = phone,
                Age = age,
                Gender = gender,
                Address = address
            };

            try
            {
                bool added = await csBL.AddCustomerAsync(dto);
                if (added)
                {
                    var okDialog = new ContentDialog
                    {
                        Title = "Customer Added",
                        Content = $"Customer '{dto.Name}' was added successfully.",
                        CloseButtonText = "OK",
                        XamlRoot = this.Content.XamlRoot
                    };
                    await okDialog.ShowAsync();

                    // Clear fields
                    NameTextBox.Text = "";
                    PhoneTextBox.Text = "";
                    AgeTextBox.Text = "";
                    AddressTextBox.Text = "";
                    GenderComboBox.SelectedIndex = 0;
                }
                else
                {
                    var failDialog = new ContentDialog
                    {
                        Title = "Add Failed",
                        Content = "Customer could not be added. Please try again.",
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
                    Content = $"An error occurred while adding customer:\n{ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }
    }
}
