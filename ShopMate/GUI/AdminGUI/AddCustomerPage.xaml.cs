using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using System.Drawing;
using System.Threading.Tasks;

namespace ShopMate.GUI
{
    public sealed partial class AddCustomerPage : Page
    {
        private readonly CustomerServiceBL csBL;
        public AddCustomerPage()
        {
            this.InitializeComponent();

            this.csBL = new CustomerServiceBL();
        }
        private async void OnAddCustomerClicked(object sender, RoutedEventArgs e)
        {
            bool fail = false;

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                NameTextBox.BorderBrush = Red();
                NameTextBox.Focus(FocusState.Programmatic);
                fail = true;
            }
            else NameTextBox.BorderBrush = White();

            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                PhoneTextBox.BorderBrush = Red();
                PhoneTextBox.Focus(FocusState.Programmatic);
                fail = true;
            }
            else PhoneTextBox.BorderBrush = White();

            if (double.IsNaN(AgeNumberBox.Value) || AgeNumberBox.Value < 5)
            {
                AgeNumberBox.BorderBrush = Red();
                AgeNumberBox.Focus(FocusState.Programmatic);
                fail = true;
            }
            else AgeNumberBox.BorderBrush = White();

            if (fail) return;


            var customer = new CustomerDTO
            {
                Name = NameTextBox.Text.Trim(),
                Phone = PhoneTextBox.Text.Trim(),
                Address = AddressTextBox.Text.Trim(),
                Age = (int)AgeNumberBox.Value,
                Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Male"
            };


            bool success = await csBL.AddCustomerAsync(customer);

            if (success)
            {
                await ShowDialog("Success", "Customer added successfully!");
                ClearFields();
            }
            else
            {
                await ShowDialog("Error", "Failed to add customer. Please try again.");
            }
        }

        private SolidColorBrush Red() =>
            new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));

        private SolidColorBrush White() =>
            new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));

        private void ClearFields()
        {
            NameTextBox.Text = "";
            PhoneTextBox.Text = "";
            AddressTextBox.Text = "";
            GenderComboBox.SelectedIndex = 0;

            AgeNumberBox.Value = double.NaN;
            AgeNumberBox.BorderBrush = White();
        }

        private async Task ShowDialog(string title, string msg)
        {
            var dlg = new ContentDialog
            {
                Title = title,
                Content = msg,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dlg.ShowAsync().AsTask();
        }
    }
}