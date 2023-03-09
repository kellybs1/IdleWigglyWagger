
using System.Windows;

namespace IdleWigglyWagger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Private members
        ///////////////////////////////

        private IIdleWigglyWagger _wigglyWaggerApp;
        private WigglyWaggerAppVM _wigglyWaggerAppVM;
        private static MainWindow _appWindow;


        // Event Handlers
        ///////////////////////////////
        private void onAppStartup( object sender, StartupEventArgs e )
        {
            // Instantiate App class and ViewModel
            _wigglyWaggerApp = new WigglyWaggerApp();
            _wigglyWaggerAppVM = new WigglyWaggerAppVM( _wigglyWaggerApp );

            _appWindow = new MainWindow
            {
                DataContext = _wigglyWaggerAppVM, // Binds the view to the application viewmodel
                Title = "Idle Wiggly Waggler", // Window Title
                ResizeMode = ResizeMode.CanMinimize, // Stops the ability to resize by dragging or Maximise... can still minimise
                Topmost = true,
                Height = 330,
                Width = 420
            };

            // Subscribe to any window events
            _appWindow.ButtonToggleMouseMovement.Click += onButtonToggleMouseMovementClicked;

            // DO THE THING
            _appWindow.Show();
        }

        private void onButtonToggleMouseMovementClicked( object sender, RoutedEventArgs e )
        {
            _wigglyWaggerApp.MouseMovementIsEnabled ^= true;
        }
    }
}
