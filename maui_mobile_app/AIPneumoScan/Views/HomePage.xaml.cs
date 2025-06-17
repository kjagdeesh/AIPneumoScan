namespace AIPneumoScan.Views;

/// <summary>
/// Represents the main landing page of the AI PneumoScan application.
/// Allows users to capture or select chest X-ray images and run AI-based pneumonia predictions.
/// </summary>
public partial class HomePage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomePage"/> class.
    /// Sets the BindingContext to <see cref="ViewModels.HomePageViewModel"/>, which handles image selection and prediction logic.
    /// </summary>
    public HomePage()
	{
		InitializeComponent();
        BindingContext = new ViewModels.HomePageViewModel();
    }
}