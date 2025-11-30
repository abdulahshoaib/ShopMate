using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Collections.Generic;

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class DeleteCustomerPage : Page
    {
        private List<CustomerDTO> customers = [];
        private CustomerDTO selectedCustomer = new CustomerDTO();
        private readonly CustomerServiceBL csBL;
        public DeleteCustomerPage()
        {
            InitializeComponent();

            this.csBL = new CustomerServiceBL();
            LoadCustomers();
        }

        private async void LoadCustomers()
        {
            customers = await csBL.GetAllCustomers();
            CustomerComboBox.ItemsSource = customers;
            CustomerComboBox.DisplayMemberPath = "Name";
            CustomerComboBox.SelectedIndex = 0;
        }

        private void CustomerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerComboBox.SelectedItem is CustomerDTO c)
            {
                selectedCustomer = c;

                NameTextBox.Text = c.Name;
                PhoneTextBox.Text = c.Phone;
                AddressTextBox.Text = c.Address;
                AgeTextBox.Text = c.Age?.ToString() ?? "";

                // Gender
                if (c.Gender == "Male")
                    GenderComboBox.SelectedIndex = 0;
                else if (c.Gender == "Female")
                    GenderComboBox.SelectedIndex = 1;
            }
        }

        private async void OnDeleteCustomerClicked(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer is null)
            {
                await ShowDialog("Error", "Please select a customer to delete.");
                return;
            }

            var confirm = new ContentDialog
            {
                Title = "Confirm Delete",
                Content = $"Are you sure you want to delete customer '{selectedCustomer.Name}'?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await confirm.ShowAsync();

            if (result != ContentDialogResult.Primary)
                return;

            bool success = await csBL.RemoveCustomer(selectedCustomer.ID);

            if (success)
            {
                await ShowDialog("Customer Deleted", "Customer was successfully removed.");
                LoadCustomers();
                ClearFields();
            }
            else
            {
                await ShowDialog("Error", "Failed to delete customer.");
            }
        }

        private void ClearFields()
        {
            NameTextBox.Text = "";
            PhoneTextBox.Text = "";
            AddressTextBox.Text = "";
            AgeTextBox.Text = "";
            GenderComboBox.SelectedIndex = -1;
        }

        private async System.Threading.Tasks.Task ShowDialog(string title, string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}
