using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class DeleteEmployeePage : Page
    {
        private List<EmployeeDTO> employees = [];
        private EmployeeDTO selectedEmployee = new EmployeeDTO();
        private readonly EmployManagementBL emBL;
        public DeleteEmployeePage()
        {
            InitializeComponent();

            this.emBL = new EmployManagementBL();
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            employees = await emBL.GetAllEmployees();
            EmployeeComboBox.ItemsSource = employees;
            EmployeeComboBox.DisplayMemberPath = "Name";
        }

        private void EmployeeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeComboBox.SelectedItem is EmployeeDTO emp)
            {
                selectedEmployee = emp;

                NameTextBox.Text = emp.Name;
                PhoneTextBox.Text = emp.Phone;
                AddressTextBox.Text = emp.Address;
                
                // Password is not shown — placeholder
                PasswordBox.Password = "********";
            }
        }

        private async void OnDeleteEmployeeClicked(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee is null)
            {
                await ShowDialog("Error", "Please select an employee to delete.");
                return;
            }

            var confirm = new ContentDialog
            {
                Title = "Confirm Deletion",
                Content = $"Are you sure you want to delete employee '{selectedEmployee.Name}'?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await confirm.ShowAsync();
            if (result != ContentDialogResult.Primary)
                return;

            // Delete using BL
            bool success = await emBL.RemoveEmployee(selectedEmployee);

            if (success)
            {
                await ShowDialog("Employee Deleted", "Employee was successfully removed.");

                LoadEmployees();
                ClearFields();
            }
            else
            {
                await ShowDialog("Error", "Failed to delete employee.");
            }
        }

        private void ClearFields()
        {
            NameTextBox.Text = "";
            PhoneTextBox.Text = "";
            AddressTextBox.Text = "";
            // UsernameTextBox.Text = "";
            PasswordBox.Password = "";
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
