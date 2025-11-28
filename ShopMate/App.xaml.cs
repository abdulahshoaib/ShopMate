using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShopMate.DL;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ShopMate
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static Frame? MainFrame { get; set; }

        public Window? _window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            await SupabaseInitializer.InitializeAsync();

            _window = new Window();
            var rootFrame = new Frame();
            App.MainFrame = rootFrame;
            rootFrame.Navigate(typeof(GUI.LoginPage));
            _window.Content = rootFrame;
            _window.Activate();
        }
    }
}
