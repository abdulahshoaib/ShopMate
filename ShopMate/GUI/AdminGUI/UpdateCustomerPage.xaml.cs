using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Collections.Generic;


namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class UpdateCustomerPage : Page
    {
        private List<CustomerDTO> customers = [];
        private CustomerDTO selectedCustomer = new CustomerDTO();
        private readonly CustomerServiceBL csBL;
        public UpdateCustomerPage()
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

        private async void OnSaveCustomerClicked(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer is null)
            {
                await ShowDialog("Error", "Please select a customer to edit.");
                return;
            }

            // Update DTO
            selectedCustomer.Name = NameTextBox.Text;
            selectedCustomer.Phone = PhoneTextBox.Text;
            selectedCustomer.Address = AddressTextBox.Text;

            if (int.TryParse(AgeTextBox.Text, out int age))
                selectedCustomer.Age = age;

            selectedCustomer.Gender =
                (GenderComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";

            // Update
            bool success = await csBL.UpdateCustomer(selectedCustomer);

            if (success)
            {
                await ShowDialog("Success", "Customer updated successfully.");
                LoadCustomers();
            }
            else
            {
                await ShowDialog("Error", "Failed to update customer.");
            }
        }

        private async System.Threading.Tasks.Task ShowDialog(string title, string message)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private void OnBackClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageCustomersPage));
        }

        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            var frame = window?.Content as Frame;

            frame?.Navigate(t);
        }
    }
}