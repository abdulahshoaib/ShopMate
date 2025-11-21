using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ShopMate
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void OnSignInClicked(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if ((username == "admin" && password == "admin123") ||
                (username == "sales" && password == "sales123"))
            {
                // Navigate to next page
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Success",
                    Content = "Login Successful!",
                    CloseButtonText = "OK"
                };
                _ = dialog.ShowAsync();
            }
            else
            {
                ErrorTextBlock.Text = "Invalid username or password.";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
