using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ShopMate.BL;

using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class ViewInventory : Page
    {
        private readonly ProductManagementBL pmBL;

        public ViewInventory()
        {
            InitializeComponent();
            this.pmBL = new ProductManagementBL();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var list = await pmBL.GetAllProducts();
            var rows = new List<ProductRow>();

            bool alt = false;
            foreach (var p in list)
            {
                rows.Add(new ProductRow(p)
                {
                    RowColor = alt ? Windows.UI.Color.FromArgb(20, 255, 255, 255)
                                   : Windows.UI.Color.FromArgb(30, 255, 255, 255)
                });

                alt = !alt;
            }

            ProductsRepeater.ItemsSource = rows;
        }

        public class ProductRow
        {
            public ProductDTO Product { get; }
            public Windows.UI.Color RowColor { get; set; }

            public string Name => Product.Name;
            public string Description => Product.Description;
            public int Stock => Product.Stock;
            public decimal Price => Product.Price;
            public DateTime? ExpiryDate => Product.ExpiryDate;

            public ProductRow(ProductDTO p) { Product = p; }
        }
    }
}
