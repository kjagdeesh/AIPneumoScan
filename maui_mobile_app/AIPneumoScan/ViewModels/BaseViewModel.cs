using System.ComponentModel;
using System.Windows.Input;

namespace AIPneumoScan.ViewModels
{
    /// <summary>
    /// Base view model that implements INotifyPropertyChanged and provides common functionality
    /// used across all view models in the AI PneumoScan application.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the user's full name stored in application preferences.
        /// Defaults to "Jhon Doe" if not set.
        /// </summary>
        public string UserFullName
        {
            get => Preferences.Default.Get("UserFullName", "Jhon Doe");
        }

        /// <summary>
        /// Gets or sets the navigation object used for page navigation.
        /// This must be set by the derived view model or assigned externally.
        /// </summary>
        public INavigation navigation { get; set; }

        /// <summary>
        /// Command to open a URL in the system browser.
        /// Can be bound to UI elements to launch external links.
        /// </summary>
        public ICommand OpenUrlCommand { get; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="name">The name of the property that changed.</param>
        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        public BaseViewModel()
        {
            OpenUrlCommand = new Command<string>(async (url) =>
            {
                if (!string.IsNullOrWhiteSpace(url))
                    await Launcher.Default.OpenAsync(url);
            });
        }
    }
}
