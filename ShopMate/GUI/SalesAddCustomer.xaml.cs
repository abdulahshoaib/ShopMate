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
    public sealed partial class SalesAddCustomer : Page
    {
        private readonly CustomerServiceBL csBL;
        public SalesAddCustomer()
        {
            this.InitializeComponent();

            this.csBL = new CustomerServiceBL();
        }
        private async void OnAddCustomerClicked(object sender, RoutedEventArgs e)
        {
            bool f = false;
            if (NameTextBox.Text == "")
            {
                NameTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                NameTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                NameTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if (PhoneTextBox.Text == "")
            {
                PhoneTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                PhoneTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                PhoneTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if (AgeTextBox.Text == "" || Convert.ToInt32(AgeTextBox.Text) < 5)
            {
                AgeTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                AgeTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                AgeTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if(!f)
            {
                var customer = new CustomerDTO
                {
                    Name = NameTextBox.Text.Trim(),
                    Phone = PhoneTextBox.Text.Trim(),
                    Address = AddressTextBox.Text,
                    Age = Convert.ToInt32(AgeTextBox.Text),
                    Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Male"
                };
                if (await csBL.AddCustomerAsync(customer))
                {
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = "Success",
                        Content = "Customer added successfully!",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                    NameTextBox.Text = "";
                    PhoneTextBox.Text = "";
                    AddressTextBox.Text = "";
                    AgeTextBox.Text = "";
                    GenderComboBox.SelectedIndex = 0;
                }
                else
                {
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Failed to add customer. Please try again.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                }
            }
        }
        

        
    }
}
