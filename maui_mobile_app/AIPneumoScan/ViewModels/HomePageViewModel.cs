using AIPneumoScan.Services;
using System.Windows.Input;

namespace AIPneumoScan.ViewModels
{
    /// <summary>
    /// ViewModel for the HomePage in the AI PneumoScan application.
    /// Handles image selection/capture and uses a predictor service to analyze chest X-rays.
    /// </summary>
    public class HomePageViewModel : BaseViewModel
    {
        private bool _isBusy;

        /// <summary>
        /// Indicates whether the app is currently processing an operation (e.g., prediction).
        /// Used to control loading indicators or disable UI during operations.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        /// <summary>
        /// Instance of the pneumonia prediction service responsible for analyzing chest X-ray images 
        /// using an ONNX model and returning diagnostic results.
        /// </summary>
        private readonly PneumoniaPredictionService _predictor = new();

        private string _predictionResult;

        /// <summary>
        /// Gets or sets the result of the pneumonia prediction.
        /// </summary>
        public string PredictionResult
        {
            get => _predictionResult;
            set { _predictionResult = value; OnPropertyChanged(nameof(PredictionResult)); }
        }

        private string _lastImagePath;

        private ImageSource _selectedImage = "placholder.png";

        /// <summary>
        /// Gets or sets the currently selected or captured image.
        /// This is bound to the image UI on the HomePage.
        /// </summary>
        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set { _selectedImage = value; OnPropertyChanged(nameof(SelectedImage)); }
        }

        /// <summary>
        /// Command to pick an image from the device's gallery.
        /// </summary>
        public ICommand PickImageCommand { get; }

        /// <summary>
        /// Command to capture an image using the device's camera.
        /// </summary>
        public ICommand CaptureImageCommand { get; }

        /// <summary>
        /// Command to analyze the selected or captured X-ray image for pneumonia.
        /// </summary>
        public ICommand AnalyzeXRay { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageViewModel"/> class.
        /// Sets up commands for image picking, capturing, and prediction.
        /// </summary>
        public HomePageViewModel()
        {
            AnalyzeXRay = new Command(() => RunPrediction());
            PickImageCommand = new Command(async () => await PickImageAsync());
            CaptureImageCommand = new Command(async () => await CaptureImageAsync());
        }

        /// <summary>
        /// Allows the user to pick a photo from the device.
        /// Updates the SelectedImage and stores the image path.
        /// </summary>
        private async Task PickImageAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    _lastImagePath = result.FullPath;
                    SelectedImage = ImageSource.FromFile(_lastImagePath);                    
                }
            }
            catch(Exception ex) 
            {
                await Shell.Current.DisplayAlert("Error", $"Photo selection failed: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Allows the user to capture a photo using the camera.
        /// Updates the SelectedImage and stores the image path.
        /// </summary>
        private async Task CaptureImageAsync()
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();
                if (result != null)
                {
                    _lastImagePath = result.FullPath;
                    SelectedImage = ImageSource.FromFile(_lastImagePath);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Photo capture failed: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Performs the pneumonia prediction using the selected image.
        /// Updates the PredictionResult and navigates to the result page.
        /// </summary>
        private async void RunPrediction()
        {
            if (!string.IsNullOrEmpty(_lastImagePath))
            {
                try
                {
                    IsBusy = true;
                    await Task.Delay(1000);

                    await Task.Run(() =>
                    {
                        var result = _predictor.Predict(_lastImagePath);
                        MainThread.BeginInvokeOnMainThread(() => PredictionResult = result);
                    });

                    IsBusy = false;

                    Preferences.Set("PneumoniaStatus", PredictionResult);
                    await Shell.Current.GoToAsync("ResultPage");
                }
                catch (Exception ex)
                {
                    PredictionResult = $"Error: {ex.Message}";
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", $"Kindly select an image to proceed.", "OK");
            }
        }
    }
}
