using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.VoiceCommands;
using System;
using ShopMate.GUI.AdminGUI;

namespace ShopMate.GUI
{
    public sealed partial class ManageEmployeesPage : Page
    {
        public ManageEmployeesPage()
        {
            this.InitializeComponent();
        }
        private void Navigate(Type t)
        {
            var window = (Application.Current as App)?._window;
            Frame? frame = window?.Content as Frame;

            frame?.Navigate(t);
        }

        private void OnEditEmployeeClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(EditEmployeePage));
        }


        private void OnAddEmployeeClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(AddEmployeePage));
        }



        private void OnDeleteEmployeeClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(DeleteEmployeePage));
        }

        // Ensure the event handler referenced from XAML exists.
        private void OnViewEmployeesClicked(object sender, RoutedEventArgs e)
        {
            //Navigate(typeof(ViewEmployeePage));
        }
        
    }
}
