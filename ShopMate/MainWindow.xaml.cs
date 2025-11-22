using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ShopMate.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ShopMate
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Content = new LoginPage();
        }

        // Added handler to match Click="LoginButton_Click" in MainWindow.xaml
        private void LoginButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // Minimal, safe behavior so build succeeds.
            // Use the UsernameBox and PasswordBox defined in MainWindow.xaml.
            var username = UsernameBox?.Text;
            var password = PasswordBox?.Password;

            // Example: if username provided, reflect it in the window title.
            if (!string.IsNullOrWhiteSpace(username))
            {
                this.Title = $"ShopMate - {username}";
            }
            else
            {
                this.Title = "ShopMate";
            }

            // Optional: replace Content with another Page or Frame navigation here
            // once you have a dashboard page available.
        }
    }
}
