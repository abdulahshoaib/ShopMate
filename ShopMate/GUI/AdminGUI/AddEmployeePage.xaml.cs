using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using System.Threading.Tasks;

namespace ShopMate.GUI
{
    public sealed partial class AddEmployeePage : Page
    {
        private readonly EmployeeManagementBL emBL = new EmployeeManagementBL();
        private readonly UserManagementBL umBL = new UserManagementBL();

        public AddEmployeePage()
        {
            this.InitializeComponent();
        }

        private async void OnAddEmployeeClicked(object sender, RoutedEventArgs e)
        {
            bool fail = false;

            // VALIDATIONS
            if (NameTextBox.Text == "")
            {
                NameTextBox.BorderBrush = red();
                NameTextBox.Focus(FocusState.Programmatic);
                fail = true;
            }
            else NameTextBox.BorderBrush = white();

            if (PhoneTextBox.Text == "")
            {
                PhoneTextBox.BorderBrush = red();
                PhoneTextBox.Focus(FocusState.Programmatic);
                fail = true;
            }
            else PhoneTextBox.BorderBrush = white();

            if (AddressTextBox.Text == "")
            {
                AddressTextBox.BorderBrush = red();
                AddressTextBox.Focus(FocusState.Programmatic);
                fail = true;
            }
            else AddressTextBox.BorderBrush = white();

            if (UsernameTextBox.Text == "")
            {
                UsernameTextBox.BorderBrush = red();
                UsernameTextBox.Focus(FocusState.Programmatic);
                fail = true;
            }
            else UsernameTextBox.BorderBrush = white();

            if (PasswordBox.Password == "")
            {
                PasswordBox.BorderBrush = red();
                PasswordBox.Focus(FocusState.Programmatic);
                fail = true;
            }
            else PasswordBox.BorderBrush = white();

            if (fail) return;

            var newEmployee = new EmployeeDTO
            {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text
            };

            var createdEmp = await emBL.AddEmployee(newEmployee);

            if (createdEmp == null)
            {
                await ShowDialog("Error", "Failed to add employee.");
                return;
            }

            var newUser = new UserDTO
            {
                Username = UsernameTextBox.Text,
                PasswordHash = HashPassword(PasswordBox.Password),
                RoleID = 8,
                EmployeeID = createdEmp
            };

            var createdUser = await umBL.AddUser(newUser);

            if (!createdUser)
            {
                await ShowDialog("Warning", "Employee added, but failed to create user login.");
                return;
            }

            await ShowDialog("Success", "Employee and login account created successfully!");

            ClearFields();
        }

        private SolidColorBrush red() =>
            new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));

        private SolidColorBrush white() =>
            new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));


        private void ClearFields()
        {
            NameTextBox.Text = "";
            PhoneTextBox.Text = "";
            AddressTextBox.Text = "";
            UsernameTextBox.Text = "";
            PasswordBox.Password = "";

            NameTextBox.BorderBrush = white();
            PhoneTextBox.BorderBrush = white();
            AddressTextBox.BorderBrush = white();
            UsernameTextBox.BorderBrush = white();
            PasswordBox.BorderBrush = white();
        }

        private string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
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
        private void OnBackClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageEmployeesPage));
        }

        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            var frame = window?.Content as Frame;

            frame?.Navigate(t);
        }
    }
}