using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;

namespace ShopMate.GUI
{
    public sealed partial class AddEmployeePage : Page
    {

        public AddEmployeePage()
        {
            this.InitializeComponent();
        }

        private void OnAddEmployeeClicked(object sender, RoutedEventArgs e)
        {
            bool f = false;
            if (NameTextBox.Text == "")
            {
                NameTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                NameTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                NameTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if (PhoneTextBox.Text == "")
            {
                PhoneTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                PhoneTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                PhoneTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if (AddressTextBox.Text == "")
            {
                AddressTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                AddressTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                AddressTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if(UsernameTextBox.Text == "")
            {
                UsernameTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                UsernameTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                UsernameTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if(PasswordBox.Password == "")
            {
                PasswordBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                PasswordBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                PasswordBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if (!f)
            {

            }
        }


    }
}
