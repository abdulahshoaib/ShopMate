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
        private LoginDTO _loginDTO;
        private LoginBL _loginBL;
        public LoginPage()
        {
            _loginBL = new LoginBL();
            _loginDTO = new LoginDTO();
            this.InitializeComponent();
        }

        private sealed record OriginalBrushes(Brush? BorderBrush, Brush? Background);

        

        private void OnSignInClicked(object sender, RoutedEventArgs e)
        {
            _loginDTO.Username = UsernameTextBox.Text;
            _loginDTO.Password = PasswordBox.Password;

            if (!_loginBL.loginuser(_loginDTO))
            {
                ErrorTextBlock.Text = "Invalid username or password.";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
