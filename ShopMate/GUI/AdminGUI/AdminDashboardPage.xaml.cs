using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using System;
using System.Globalization;

namespace ShopMate.GUI
{
    public sealed partial class AdminDashboardPage : Page
    {
        private readonly BillManagementBL bmBL;
        public AdminDashboardPage()
        {
            this.InitializeComponent();

            bmBL = new BillManagementBL();

            WelcomeTitle.Text = GlobalSession.DisplayUsername;
            DashboardDay.Text = DateTime.Now.ToString("dddd");
            DashboardDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");

            // start loading KPIs
            LoadKpis();
        }

        private async void LoadKpis()
        {
            try
            {
                var totalSales = await bmBL.GetTotalSalesToday();
                var totalBills = await bmBL.GetTotalBillsToday();
                var avgBill = await bmBL.GetAverageBillValueToday();
                var lowStock = await bmBL.GetLowStockProductsCount();

                KpiSalesTodayValue.Text = totalSales.ToString("C", CultureInfo.CurrentCulture);
                KpiTotalBillsValue.Text = totalBills.ToString();
                KpiAvgBillValue.Text = avgBill.ToString("C", CultureInfo.CurrentCulture);
                KpiLowStockValue.Text = lowStock.ToString();
            }
            catch
            {
                // keep defaults on error
            }
        }

        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            Frame? frame = window?.Content as Frame;

            frame?.Navigate(t);
        }

        private void OnManageEmployeesClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageEmployeesPage));
        }


        private void OnManageCustomersClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageCustomersPage));
        }

        private void OnManageProductsClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(ManageProductsPage));
        }



    }
}
