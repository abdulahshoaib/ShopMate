using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using ShopMate.DTOs;

namespace ShopMate.GUI
{
    public sealed partial class AddCustomerPage : Page
    {
        private readonly CustomerServiceBL csBL;
        public AddCustomerPage()
        {
            this.InitializeComponent();

            this.csBL = new CustomerServiceBL();
        }

        private void SaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDTO cDTO = new CustomerDTO()
            {
                Name = NameTextBox.Text,
                Email = EmailTextBox.Text,
                Address = AddressTextBox.Text,
                Phone = PhoneTextBox.Text,
                Notes = NotesTextBox.Text,
            };
            bool resp = csBL.AddCustomer(cDTO);

            if (resp)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Success",
                    Content = "Customer saved successfully!",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                _ = dialog.ShowAsync();
            } 
            else
            {

                ContentDialog dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Unable to save customer. Try Again",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                _ = dialog.ShowAsync();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = AddressTextBox.Text = PhoneTextBox.Text = EmailTextBox.Text = NotesTextBox.Text = "";
        }
    }
}
