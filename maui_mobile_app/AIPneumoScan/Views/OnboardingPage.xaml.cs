using AIPneumoScan.ViewModels;

namespace AIPneumoScan.Views;

/// <summary>
/// Represents the onboarding page of the AI PneumoScan application.
/// Guides users through a series of slides introducing the app features and collects the user's name.
/// </summary>
public partial class OnboardingPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OnboardingPage"/> class.
    /// Sets the BindingContext to <see cref="OnboardingViewModel"/>, which controls the onboarding logic and navigation.
    /// </summary>
    public OnboardingPage()
	{
		InitializeComponent();
        BindingContext = new OnboardingViewModel(Navigation);
    }
}