namespace AIPneumoScan.ViewModels
{
    /// <summary>
    /// ViewModel for the About page in the AI PneumoScan application.
    /// This ViewModel is responsible for managing logic, state, and navigation
    /// related to the About page UI.
    /// </summary>
    public class AboutPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutPageViewModel"/> class.
        /// </summary>
        /// <param name="navigation">
        /// An instance of <see cref="INavigation"/> used to manage navigation 
        /// from the About page to other pages in the application.
        /// </param>
        public AboutPageViewModel(INavigation navigation) 
        {
            this.navigation = navigation;
        }
    }
}
