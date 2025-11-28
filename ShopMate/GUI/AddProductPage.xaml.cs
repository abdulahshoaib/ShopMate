using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.DL;
using ShopMate.DTOs;
using ShopMate.BL;
using System;

namespace ShopMate.GUI
{
    public sealed partial class AddProductPage : Page
    {
        private readonly ProductManagementBL pmBL;
        public AddProductPage()
        {
            InitializeComponent();

            pmBL = new ProductManagementBL();
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductDTO pDTO = new()
            {
                Name = ProductNameTextBox.Text,
                Price = (decimal)PriceNumberBox.Value,
                Quantity = (int)QuantityNumberBox.Value,
            };

            pmBL.AddProduct(pDTO);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ProductNameTextBox.Text = "";
            PriceNumberBox.Value = 0;
            QuantityNumberBox.Value = 0;
        }
    }
}