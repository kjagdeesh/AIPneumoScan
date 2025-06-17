using AIPneumoScan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIPneumoScan.ViewModels
{
    /// <summary>
    /// ViewModel for displaying pneumonia detection results in the AI PneumoScan application.
    /// Based on the saved prediction result, this ViewModel prepares the summary, message, and icon.
    /// </summary>
    public class ResultViewModel : BaseViewModel
    {
        /// <summary>
        /// The diagnostic result to display, such as "Normal" or "Pneumonia Detected".
        /// </summary>
        public string DiagnosisResult { get; set; }

        /// <summary>
        /// Additional context message to explain the diagnostic result.
        /// </summary>
        public string DiagnosisMessage { get; set; }

        /// <summary>
        /// The image path representing the result status (e.g., a checkmark or warning icon).
        /// </summary>
        public string ResultIcon { get; set; }

        /// <summary>
        /// Command to navigate back to the home page.
        /// </summary>
        public ICommand BackCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultViewModel"/> class.
        /// Reads the result from app preferences, interprets it, and sets corresponding display values.
        /// </summary>
        /// <param name="navigation">The Shell navigation object used to push the HomePage.</param>
        public ResultViewModel(INavigation navigation)
        {
            string result = Preferences.Get("PneumoniaStatus", string.Empty);

            this.navigation = navigation;

            DiagnosisResult = result.Contains("Pneumonia", StringComparison.OrdinalIgnoreCase) ? result + " Detected" : "Normal";
            DiagnosisMessage = result.Contains("Pneumonia", StringComparison.OrdinalIgnoreCase)
                ? "Signs of pneumonia detected. Further medical evaluation is strongly recommended."
                : "No signs of pneumonia detected. Please consult your doctor for confirmation.";

            ResultIcon = result.Contains("Pneumonia", StringComparison.OrdinalIgnoreCase)
                ? "pneumonia.png" : "normal.png";

            BackCommand = new Command(async () =>
            {
                var page = new HomePage();
                page.BindingContext = new HomePageViewModel();

                // Navigate manually to that page
                await Shell.Current.Navigation.PushAsync(page);
            });
        }
    }
}
