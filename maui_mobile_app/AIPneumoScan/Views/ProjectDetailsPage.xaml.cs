namespace AIPneumoScan.Views;

/// <summary>
/// Represents the Project Details page in the AI PneumoScan application.
/// This page is intended to display detailed information about the project,
/// such as its key features, technologies used, data sources, or acknowledgments.
/// </summary>
public partial class ProjectDetailsPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectDetailsPage"/> class.
    /// </summary>
    public ProjectDetailsPage()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        string url = "https://github.com/kjagdeesh/AIPneumoScan";
        await Launcher.Default.OpenAsync(url);
    }
}