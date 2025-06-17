namespace AIPneumoScan.Views;

/// <summary>
/// Represents the result page in the AI PneumoScan application.
/// Displays the diagnosis result from the AI model along with guidance and an icon.
/// </summary>
public partial class ResultPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResultPage"/> class.
    /// Sets the BindingContext to <see cref="ViewModels.ResultViewModel"/>, which loads the diagnosis result and handles navigation.
    /// </summary>
    public ResultPage()
	{
		InitializeComponent();
        BindingContext = new ViewModels.ResultViewModel(Navigation);
    }
}