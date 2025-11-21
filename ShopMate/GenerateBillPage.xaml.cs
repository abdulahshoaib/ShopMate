using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ShopMate.Pages
{
    public sealed partial class GenerateBillPage : Page
    {
        public GenerateBillPage()
        {
            this.InitializeComponent();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add selected product and quantity to bill
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Remove selected item from bill
        }

        private void GenerateBill_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Finalize bill and maybe export to PDF/Print
        }
    }
}
