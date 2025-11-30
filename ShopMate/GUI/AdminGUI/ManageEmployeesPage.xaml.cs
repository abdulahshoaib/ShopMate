using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.VoiceCommands;
using System;

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

        }


        private void OnAddEmployeeClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(AddEmployeePage));
        }



        private void OnDeleteEmployeeClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Delete selected customer
        }

        private void OnViewEmployeesClicked(object sender, RoutedEventArgs e)
        {
        }
        // Example data model
        
    }
}
