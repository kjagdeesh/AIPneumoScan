using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AIPneumoScan.ViewModels
{
    /// <summary>
    /// ViewModel responsible for managing onboarding slides and collecting the user's name
    /// in the AI PneumoScan application.
    /// </summary>
    public class OnboardingViewModel : BaseViewModel
    {
        /// <summary>
        /// Collection of onboarding slides displayed to the user.
        /// </summary>
        public ObservableCollection<OnboardingItem> OnboardingItems { get; set; }

        /// <summary>
        /// Command executed when the user taps the "Next" or "Get Started" button.
        /// </summary>
        public ICommand NextCommand { get; }

        public int _currentPosition;

        /// <summary>
        /// Current position of the onboarding carousel or slider.
        /// Used to track which onboarding item is currently shown.
        /// </summary>
        public int CurrentPosition
        {
            get => _currentPosition;
            set { _currentPosition = value; OnPropertyChanged(nameof(CurrentPosition)); }
        }

        private string _userName;

        /// <summary>
        /// User's full name input at the end of the onboarding process.
        /// </summary>
        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(nameof(UserName)); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnboardingViewModel"/> class.
        /// Prepares the onboarding steps and command bindings.
        /// </summary>
        /// <param name="navigation">The navigation instance used to control Shell routing.</param>
        public OnboardingViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            NextCommand = new Command(ExecuteNext);
            CurrentPosition = 0;

            OnboardingItems = new ObservableCollection<OnboardingItem>
            {
                new OnboardingItem
                {
                    Title = "AI-Powered Pneumonia Detection",
                    Description = "Analyze chest X-rays instantly using offline AI.",
                    Image = "slide1.png",
                    ButtonText = "Next",
                    ShowButton = true
                },
                new OnboardingItem
                {
                    Title = "Offline & Secure",
                    Description = "No internet required. Your data stays on your device.",
                    Image = "slide2.png",
                    ButtonText = "Next",
                    ShowButton = true
                },
                new OnboardingItem
                {
                    Title = "Fast & Lightweight",
                    Description = "Optimized ONNX model ensures smooth performance.",
                    Image = "slide3.png",
                    ButtonText = "Next",
                    ShowButton = true
                },
                new OnboardingItem
                {
                    Title = "Disclaimer",
                    Description = "AI PneumoScan is not a medical device. This application is intended for educational, research, and informational purposes only. It is not approved by any medical regulatory authority and should not be used as a substitute for professional medical advice, diagnosis, or treatment.",
                    Image = "slide4.png",
                    ButtonText = "Next",
                    ShowButton = true
                },
                new OnboardingItem
                {
                    Title = "Let’s Get Started!",
                    Description = "Tell us your name to personalize your experience.",
                    Image = "slide5.png",
                    ButtonText = "Get Started",
                    ShowButton = true,
                    IsLast = true
                }
            };
        }

        /// <summary>
        /// Advances the onboarding to the next slide.
        /// If the last slide is reached, saves the user's name and navigates to the main screen.
        /// </summary>
        private async void ExecuteNext()
        {
            if (CurrentPosition < OnboardingItems.Count - 1)
            {
                CurrentPosition++;
            }
            else
            {
                // Save user name
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    Preferences.Set("UserFullName", UserName);

                    // Mark onboarding as completed
                    Preferences.Set("HasCompletedOnboarding", true);

                    // Navigate to home or main screen
                    Shell.Current.GoToAsync("//HomePage");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Required", "Please enter your full name before continuing.", "OK");
                }
            }
        }
    }

    /// <summary>
    /// Represents a single onboarding slide including title, description, image, and button configuration.
    /// </summary>
    public class OnboardingItem
    {
        /// <summary>
        /// The title text shown on the onboarding slide.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description or body text shown on the slide.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The image path associated with the onboarding slide.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The text shown on the action button for this slide.
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// Whether to show the button on this slide.
        /// </summary>
        public bool ShowButton { get; set; }

        /// <summary>
        /// Whether this slide is the final onboarding screen.
        /// </summary>
        public bool IsLast { get; set; }
    }
}
