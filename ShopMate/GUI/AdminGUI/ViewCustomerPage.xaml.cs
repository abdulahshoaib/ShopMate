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

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class ViewCustomerPage : Page
    {
        private List<CustomerDTO> allCustomers = [];
        private readonly CustomerServiceBL csBL;
        public ViewCustomerPage()
        {
            InitializeComponent();

            this.csBL = new CustomerServiceBL();
            LoadCustomers();
        }

        private async void LoadCustomers()
        {
            allCustomers = await csBL.GetAllCustomers();
            CustomerListView.ItemsSource = allCustomers;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(query))
            {
                CustomerListView.ItemsSource = allCustomers;
                return;
            }

            var filtered = allCustomers.Where(c =>
                c.Name.ToLower().Contains(query) ||
                c.Phone.ToLower().Contains(query) ||
                (c.Address?.ToLower().Contains(query) ?? false)
            );

            CustomerListView.ItemsSource = filtered;
        }
    }
}
