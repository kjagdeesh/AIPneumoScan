using AIPneumoScan.Views;

namespace AIPneumoScan
{
    /// <summary>
    /// The main entry point for the AI PneumoScan application.
    /// Handles theme initialization, onboarding flow, and sets the main navigation shell.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Applies the saved theme preference and navigates to either the onboarding or home page based on user status.
        /// </summary>
        public App()
        {
            InitializeComponent();
            ApplySavedTheme();
            MainPage = new AppShell();

            bool hasCompleted = Preferences.Get("HasCompletedOnboarding", false);

            if (!hasCompleted)
                Shell.Current.GoToAsync("//OnboardingPage");
            else
                Shell.Current.GoToAsync("//HomePage");

        }

        /// <summary>
        /// Applies the user's previously selected theme preference.
        /// Defaults to System theme if no preference is stored.
        /// </summary>
        private void ApplySavedTheme()
        {
            int savedIndex = Preferences.Get("SelectedThemeIndex", 2);

            Current.UserAppTheme = savedIndex switch
            {
                0 => AppTheme.Light,
                1 => AppTheme.Dark,
                _ => AppTheme.Unspecified
            };
        }
    }
}
