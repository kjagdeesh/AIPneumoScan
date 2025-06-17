namespace AIPneumoScan.Views;

/// <summary>
/// Represents the settings page of the AI PneumoScan application.
/// Allows users to select and apply a preferred theme (Light, Dark, or System).
/// </summary>
public partial class SettingPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingPage"/> class.
    /// Sets the BindingContext to <see cref="ViewModels.SettingPageViewModel"/> which manages theme selection and persistence.
    /// </summary>
    public SettingPage()
	{
		InitializeComponent();
        BindingContext = new ViewModels.SettingPageViewModel(Navigation);
    }
}