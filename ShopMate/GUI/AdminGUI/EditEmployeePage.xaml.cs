using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class EditEmployeePage : Page
    {
        private List<EmployeeDTO> employees = [];
        private EmployeeDTO selectedEmployee = new EmployeeDTO();

        private List<UserDTO> users = [];
        private UserDTO? selectedUser = new UserDTO();

        private readonly UserManagementBL umBL;
        private readonly EmployManagementBL emBL;
        public EditEmployeePage()
        {
            this.InitializeComponent();

            this.umBL = new UserManagementBL();
            this.emBL = new EmployManagementBL();
            LoadData();
        }

        private async void LoadData()
        {
            employees = await emBL.GetAllEmployees();

            EmployeeComboBox.ItemsSource = employees;
            EmployeeComboBox.DisplayMemberPath = "Name";
            EmployeeComboBox.SelectedIndex = 0;
            EmployeeComboBox_SelectionChanged(null, null);
        }

        private async void EmployeeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeComboBox.SelectedItem is EmployeeDTO emp)
            {
                selectedEmployee = emp;

                // Fill employee form fields
                NameTextBox.Text = emp.Name;
                PhoneTextBox.Text = emp.Phone;
                AddressTextBox.Text = emp.Address;

                selectedUser = await umBL.GetUser(emp.ID);

                if (selectedUser != null)
                {
                    UsernameTextBox.Text = selectedUser.Username;
                    PasswordBox.Password = "";
                }
                else
                {
                    UsernameTextBox.Text = "";
                    PasswordBox.Password = "";
                }
            }
        }


        private async void OnSaveEmployeeClicked(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee == null)
            {
                await ShowDialog("Error", "Please select an employee.");
                return;
            }
            
            selectedEmployee.Name = NameTextBox.Text;
            selectedEmployee.Phone = PhoneTextBox.Text;
            selectedEmployee.Address = AddressTextBox.Text;

            bool empUpdated = await emBL.UpdateEmployee(selectedEmployee);

            if (selectedUser != null)
            {
                selectedUser.Username = UsernameTextBox.Text;

                if (!string.IsNullOrWhiteSpace(PasswordBox.Password))
                    selectedUser.PasswordHash = HashPassword(PasswordBox.Password);

                await umBL.UpdateUser(selectedUser);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(UsernameTextBox.Text))
                {
                    var newUser = new UserDTO
                    {
                        Username = UsernameTextBox.Text,
                        PasswordHash = HashPassword(PasswordBox.Password),
                        RoleID = 2,
                        EmployeeID = selectedEmployee.ID
                    };

                    await umBL.AddUser(newUser);
                }
            }

            await ShowDialog("Success", "Employee updated successfully!");
            LoadData();
        }

        private string HashPassword(string password)
        {
            // Simple SHA256 example
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        private async Task ShowDialog(string title, string message)
        {
            var dlg = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dlg.ShowAsync().AsTask();
        }
    }
}