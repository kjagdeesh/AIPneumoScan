using AIPneumoScan.ViewModels;

namespace AIPneumoScan.Views;

/// <summary>
/// Represents the About page of the AI PneumoScan application.
/// Displays information about the app such as purpose, version, and developer details.
/// </summary>
public partial class AboutPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AboutPage"/> class.
    /// Sets the BindingContext to the corresponding <see cref="AboutPageViewModel"/>.
    /// </summary>
    public AboutPage()
	{
		InitializeComponent();
        BindingContext = new AboutPageViewModel(Navigation);
    }
}