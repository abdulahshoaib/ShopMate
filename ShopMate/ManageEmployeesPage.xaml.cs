using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace ShopMate.Pages
{
    public sealed partial class ManageEmployeesPage : Page
    {
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public ManageEmployeesPage()
        {
            this.InitializeComponent();
            EmployeesListView.ItemsSource = Employees;
        }

        private void OnBackClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate back to AdminDashboardPage
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add employee logic
        }

        private void RemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Remove employee logic
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
