using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using ShopMate.BL;
using ShopMate.DTOs;
using System;

namespace ShopMate.GUI
{
    public sealed partial class LoginPage : Page
    {
        private readonly LoginBL _loginBL;

        public LoginPage()
        {
            this.InitializeComponent();
            _loginBL = new LoginBL();
        }

        private async void OnSignInClicked(object sender, RoutedEventArgs e)
        {
            var loginDTO = new LoginDTO
            {
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password
            };

            bool success = await _loginBL.LoginUserAsync(loginDTO);

            if (!success)
            {
                ErrorTextBlock.Text = "Invalid username or password.";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}

