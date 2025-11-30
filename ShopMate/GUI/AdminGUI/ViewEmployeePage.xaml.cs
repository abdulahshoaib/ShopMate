using Microsoft.UI.Xaml.Controls;
using ShopMate.BL;
using System.Collections.Generic;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ShopMate.GUI.AdminGUI
{
    public sealed partial class ViewEmployeePage : Page
    {
        private List<EmployeeViewModel> viewModels = [];
        private readonly EmployManagementBL emBL = new EmployManagementBL();
        private readonly UserManagementBL umBL = new UserManagementBL();

        public ViewEmployeePage()
        {
            this.InitializeComponent();
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            var employees = await emBL.GetAllEmployees();
            var users = await umBL.GetAllUsers();

            viewModels = employees.Select(emp =>
            {
                var user = users.FirstOrDefault(u => u.EmployeeID == emp.ID);

                return new EmployeeViewModel
                {
                    Name = emp.Name,
                    Phone = emp.Phone,
                    Address = emp.Address,
                    Username = user?.Username ?? "—"
                };
            }).ToList();

            EmployeeListView.ItemsSource = viewModels;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = SearchBox.Text.Trim().ToLower();

            var filtered = viewModels.Where(v =>
                   v.Name.ToLower().Contains(q) ||
                   v.Phone.ToLower().Contains(q) ||
                   v.Username.ToLower().Contains(q)
            );

            EmployeeListView.ItemsSource = filtered;
        }
    }

    public class EmployeeViewModel
    {
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string Username { get; set; } = "";
    }
}
