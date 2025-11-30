using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;
using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;

namespace ShopMate.GUI
{
    public sealed partial class AddProductPage : Page
    {

        public AddProductPage()
        {
            this.InitializeComponent();
        }

        private void OnAddProductClicked(object sender, RoutedEventArgs e)
        {
            bool f = false;
            if (ProductNameTextBox.Text == "") 
            {
                ProductNameTextBox.BorderBrush=new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                ProductNameTextBox.Focus(FocusState.Programmatic);
                f=true;
            }
            else 
            {
                ProductNameTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if (QuantityTextBox.Text == "")
            {
                QuantityTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                QuantityTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                QuantityTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if(PriceTextBox.Text == "")
            {
                PriceTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 0, 0));
                PriceTextBox.Focus(FocusState.Programmatic);
                f = true;
            }
            else
            {
                PriceTextBox.BorderBrush = new SolidColorBrush(ColorHelper.FromArgb(255, 255, 255, 255));
            }
            if(!f)
            {

            }
        }

        
    }
}
