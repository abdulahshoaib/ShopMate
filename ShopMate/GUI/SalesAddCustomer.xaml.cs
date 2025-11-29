using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Drawing;
using Microsoft.UI.Xaml.Media;


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
        private void OnAddCustomerClicked(object sender, RoutedEventArgs e)
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

            }
        }
        private void SaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDTO cDTO = new CustomerDTO()
            {
                Name = NameTextBox.Text,
                Address = AddressTextBox.Text,
                Phone = PhoneTextBox.Text,
                Gender = GenderComboBox.SelectedValue.ToString(),
                Age = Convert.ToInt32(AgeTextBox.Text)
            };
            bool resp = csBL.AddCustomer(cDTO);

            if (resp)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Success",
                    Content = "Customer saved successfully!",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                _ = dialog.ShowAsync();
            }
            else
            {

                ContentDialog dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Unable to save customer. Try Again",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                _ = dialog.ShowAsync();
            }
        }

        
    }
}
