using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Drawing;
using SkiaSharp;

namespace AIPneumoScan.Services
{
    /// <summary>
    /// Service responsible for performing pneumonia prediction on chest X-ray images
    /// using an ONNX model through ML.NET's ONNX Runtime.
    /// </summary>
    public class PneumoniaPredictionService
    {
        private readonly InferenceSession _session;

        /// <summary>
        /// Initializes the ONNX inference session.
        /// Copies the ONNX model from the app package to a writable directory if not already present.
        /// </summary>
        public PneumoniaPredictionService()
        {
            // Define the destination path for the ONNX model inside writable AppData directory
            var targetPath = Path.Combine(FileSystem.AppDataDirectory, "pneumonia_model.onnx");

            // Copy model from bundled assets if not already copied
            if (!File.Exists(targetPath))
            {
                // Open model from bundled resources (e.g., Resources/Raw/)
                using var stream = FileSystem.OpenAppPackageFileAsync("pneumonia_model.onnx").Result;

                // Create a writable stream in AppData
                using var fileStream = File.Create(targetPath);

                // Copy model file to writable location
                stream.CopyTo(fileStream);
            }

            // Load ONNX model into an inference session
            _session = new InferenceSession(targetPath);
        }

        /// <summary>
        /// Performs inference on the given X-ray image and returns the predicted class with confidence.
        /// </summary>
        /// <param name="imagePath">Full path to the image file.</param>
        /// <returns>A string indicating the predicted label and confidence (e.g., "Pneumonia (94%)").</returns>
        public string Predict(string imagePath)
        {
            // Convert image into ONNX-compatible tensor
            var input = LoadImageAsTensor(imagePath);

            // Prepare named ONNX input
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", input)
            };

            // Run inference
            using var results = _session.Run(inputs);

            // Extract raw output logits
            var output = results.First().AsEnumerable<float>().ToArray();

            // Convert logits to probabilities using Softmax
            var probabilities = ApplySoftmax(output);

            // Get the index of the highest probability
            var predictedIndex = Array.IndexOf(probabilities, probabilities.Max());

            // Convert index to human-readable label
            var label = predictedIndex == 0 ? "Normal" : "Pneumonia";

            // Get confidence score
            var confidence = probabilities[predictedIndex];

            return $"{label} ({confidence:P0})"; // P0 = percentage without decimal
        }

        /// <summary>
        /// Applies the Softmax function to an array of logits to convert them into probabilities.
        /// </summary>
        /// <param name="logits">The raw output from the ONNX model.</param>
        /// <returns>A normalized array of probabilities.</returns>
        private float[] ApplySoftmax(float[] logits)
        {
            float maxLogit = logits.Max(); // for numerical stability
            float sumExp = logits.Select(x => MathF.Exp(x - maxLogit)).Sum(); // sum of exponentials
            return logits.Select(x => MathF.Exp(x - maxLogit) / sumExp).ToArray(); // normalized probabilities
        }

        /// <summary>
        /// Loads an image and converts it into a normalized tensor expected by the ONNX model.
        /// Input format: [1, 3, 224, 224] with standard normalization (ImageNet).
        /// </summary>
        /// <param name="imagePath">Path to the image file.</param>
        /// <returns>A 4D tensor representing the normalized image.</returns>
        private Tensor<float> LoadImageAsTensor(string imagePath)
        {
            // Decode image using SkiaSharp
            var bitmap = SKBitmap.Decode(imagePath);

            // Resize image to match model's input size: 224x224
            bitmap = bitmap.Resize(new SKImageInfo(224, 224), SKFilterQuality.Medium);

            // Create tensor with shape [1, 3, 224, 224] for batch size 1 and RGB channels
            var input = new DenseTensor<float>(new[] { 1, 3, 224, 224 });

            // Normalization parameters (ImageNet mean and std)
            float[] mean = { 0.485f, 0.456f, 0.406f };
            float[] std = { 0.229f, 0.224f, 0.225f };

            // Populate tensor with normalized pixel values
            for (int y = 0; y < 224; y++)
            {
                for (int x = 0; x < 224; x++)
                {
                    var color = bitmap.GetPixel(x, y);

                    // Normalize each channel
                    input[0, 0, y, x] = ((color.Red / 255f) - mean[0]) / std[0];   // R
                    input[0, 1, y, x] = ((color.Green / 255f) - mean[1]) / std[1]; // G
                    input[0, 2, y, x] = ((color.Blue / 255f) - mean[2]) / std[2];  // B
                }
            }
            return input;
        }
    }
}
