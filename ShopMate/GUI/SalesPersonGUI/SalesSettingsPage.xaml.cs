using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ShopMate.BL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace ShopMate.GUI.SalesPersonGUI;
public sealed partial class SalesSettingsPage : Page
{
    private readonly UserManagementBL userBL;
    private readonly EmployeeManagementBL empBL;
    public SalesSettingsPage()
    {
        InitializeComponent();

        userBL = new UserManagementBL();
        empBL = new EmployeeManagementBL();

        LoadInitialValues();
    }

    private void LoadInitialValues()
    {
        try
        {
            if (GlobalSession.CurrentUser != null)
            {
                UsernameTextBox.Text = GlobalSession.CurrentUser.Username;
            }

            if (GlobalSession.CurrentEmployee != null)
            {
                NameTextBox.Text = GlobalSession.CurrentEmployee.Name;
                PhoneTextBox.Text = GlobalSession.CurrentEmployee.Phone;
                AddressTextBox.Text = GlobalSession.CurrentEmployee.Address;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"LoadInitialValues error: {ex}");
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Validate new password (only if user typed anything)
            if (!string.IsNullOrWhiteSpace(NewPasswordBox.Password))
            {
                if (NewPasswordBox.Password != ConfirmPasswordBox.Password)
                {
                    await ShowDialog("New password and confirmation do not match.");
                    return;
                }

                if (NewPasswordBox.Password.Length < 5)
                {
                    await ShowDialog("Password must be at least 5 characters.");
                    return;
                }
            }


            // ===============================
            // UPDATE USER ACCOUNT (username + password)
            // ===============================
            if (GlobalSession.CurrentUser != null)
            {
                var u = GlobalSession.CurrentUser;

                bool userUpdated = false;

                // update username
                if (!string.IsNullOrWhiteSpace(UsernameTextBox.Text) &&
                    UsernameTextBox.Text != u.Username)
                {
                    u.Username = UsernameTextBox.Text.Trim();
                    userUpdated = true;
                }

                // update password if entered
                if (!string.IsNullOrWhiteSpace(NewPasswordBox.Password))
                {
                    // IMPORTANT: Replace with your password hasher!
                    u.PasswordHash = HashPassword(NewPasswordBox.Password);
                    userUpdated = true;
                }

                if (userUpdated)
                {
                    bool ok = await userBL.UpdateUser(u);
                    if (!ok)
                    {
                        await ShowDialog("Failed to update user account.");
                        return;
                    }
                }
            }

            if (GlobalSession.CurrentEmployee != null)
            {
                var emp = GlobalSession.CurrentEmployee;

                emp.Name = NameTextBox.Text.Trim();
                emp.Phone = PhoneTextBox.Text.Trim();
                emp.Address = AddressTextBox.Text.Trim();

                bool ok = await empBL.UpdateEmployee(emp);

                if (!ok)
                {
                    await ShowDialog("Failed to update employee details.");
                    return;
                }
            }

            await ShowDialog("Profile updated successfully!");


            // Clear password fields
            OldPasswordBox.Password = "";
            NewPasswordBox.Password = "";
            ConfirmPasswordBox.Password = "";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SaveButton_Click error: {ex}");
            await ShowDialog("An unexpected error occurred while saving.");
        }
    }


    private void OnBackClicked(object sender, RoutedEventArgs e)
    {
        Navigate(typeof(SalesPersonDashboardPage));
    }

    private void Navigate(Type t)
    {
        var window = (Application.Current as App)?._window;
        var frame = window?.Content as Frame;

        frame?.Navigate(t);
    }


    private async Task ShowDialog(string msg)
    {
        var dlg = new ContentDialog
        {
            Title = "Notice",
            Content = msg,
            CloseButtonText = "OK",
            XamlRoot = this.Content.XamlRoot
        };

        await dlg.ShowAsync();
    }
    private string HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}