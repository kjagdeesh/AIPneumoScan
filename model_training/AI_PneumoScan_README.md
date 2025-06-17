# 🧠 AI PneumoScan - Pneumonia Detection App

**AI PneumoScan** is a cross-platform mobile and desktop application built using .NET MAUI and Python. It enables offline chest X-ray analysis using a fine-tuned **ResNet50 ONNX model** to detect pneumonia. The app provides real-time results with visual feedback and care suggestions.

---

## 🚀 Features

- 📷 Image picker and camera support
- 🧠 ONNX-powered offline pneumonia prediction
- 📊 Display of prediction confidence
- 💡 Personalized care suggestions
- 🎨 Theme switching (Light / Dark / System)
- 📱 Onboarding and result views
- 🖼️ Beautiful splash screen with animations

---

## 📂 Folder Structure

```bash
AI-PneumoScan/
├── AIPneumoScan/                # .NET MAUI app source
│   ├── Views/                   # UI pages
│   ├── ViewModels/             # MVVM logic
│   ├── Services/               # Prediction logic
│   ├── Converters/             # Value converters
│   ├── Resources/              # Images and styles
├── chest_xray/                 # Required dataset (not included)
│   ├── train/
│   │   ├── NORMAL/
│   │   └── PNEUMONIA/
│   ├── val/
│   │   ├── NORMAL/
│   │   └── PNEUMONIA/
│   └── test/
│       ├── NORMAL/
│       └── PNEUMONIA/
├── training/                   # Python training + ONNX export
│   ├── train_and_export.py
│   ├── predict_with_care.py
│   └── care_suggestions.db     # Optional SQLite care info
```

---

## 📦 Dataset (Required)

This project uses the **Chest X-Ray Pneumonia** dataset available on Kaggle:

🔗 [Download Dataset from Kaggle](https://www.kaggle.com/datasets/paultimothymooney/chest-xray-pneumonia)

After downloading, place it under the project root as shown above.

---

## 🧑‍💻 Python Model Training

To train the model and export to ONNX:

### 1. Install Dependencies

```bash
pip install torch torchvision scikit-learn numpy
```

### 2. Run Training Script

```bash
python train_and_export.py
```

This script:

- Loads the chest X-ray dataset with augmentations
- Fine-tunes a pretrained ResNet50 model
- Validates accuracy on the validation set
- Exports the trained model to `pneumonia_model.onnx`

### 3. (Optional) Predict from Image + Care Suggestion

```bash
python predict_with_care.py path/to/image.jpeg
```

You’ll get:

```
Prediction: PNEUMONIA (95%)
Care Suggestion: Seek immediate medical attention.
```

---

## 📱 MAUI App Setup

### 1. Prerequisites

- Visual Studio 2022+ with MAUI workload
- .NET 8 or higher

### 2. Build & Run

- Open `AIPneumoScan.sln`
- Run on Android/iOS/emulator

### 3. ONNX Model

Ensure `pneumonia_model.onnx` is marked as a MAUI asset in your `.csproj`:

```xml
<MauiAsset Include="pneumonia_model.onnx" />
```

The app copies the model at runtime and loads it using `InferenceSession`.

---

## 📊 SQLite Care Info (Optional)

You can use `care_suggestions.db` with a `CareInfo(label TEXT, suggestion TEXT)` table to show dynamic recommendations.

---

## 📸 Screenshots

_You can add screenshots of onboarding, home page, results, etc._

---

## 📃 License

This project is for **educational and research** purposes only. It is **not** a certified medical device.

---

## 🙌 Credits

- Dataset: [Kaggle - Paul Mooney](https://www.kaggle.com/datasets/paultimothymooney/chest-xray-pneumonia)
- ONNX Runtime + PyTorch
- .NET MAUI for cross-platform UI

---

