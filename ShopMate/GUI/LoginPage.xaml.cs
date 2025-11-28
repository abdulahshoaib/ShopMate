using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;

namespace ShopMate.GUI
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private sealed record OriginalBrushes(Brush? BorderBrush, Brush? Background);

        

        private void OnSignInClicked(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (username == "admin" && password == "admin123")
            {
                App.MainFrame.Navigate(typeof(AdminDashboardPage));
            }
            else if (username == "sales" && password == "sales123")
            {
                App.MainFrame.Navigate(typeof(SalesPersonDashboardPage));
            }
            else
            {
                ErrorTextBlock.Text = "Invalid username or password.";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
