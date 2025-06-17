namespace AIPneumoScan
{
    /// <summary>
    /// Defines the application's Shell, which manages global navigation routes and tabbed structure for AI PneumoScan.
    /// </summary>
    public partial class AppShell : Shell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppShell"/> class.
        /// Sets up routing for pages not directly included in the TabBar (e.g., ResultPage).
        /// </summary>
        public AppShell()
        {
            InitializeComponent();

            // Register non-tabbed navigation routes
            Routing.RegisterRoute("ResultPage", typeof(Views.ResultPage));
        }
    }
}
