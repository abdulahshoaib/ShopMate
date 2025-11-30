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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static ShopMate.GUI.AdminGUI.ViewInventory;

namespace ShopMate.GUI.SalesPersonGUI
{
    public sealed partial class SalesViewInventory : Page
    {
        private readonly ProductManagementBL pmBL;

        public SalesViewInventory()
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
    }
}
