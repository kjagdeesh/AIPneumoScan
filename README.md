# 🧠 Project Overview: AI PneumoScan – AI-powered Pneumonia Detection App

<img src="https://github.com/kjagdeesh/AIPneumoScan/blob/master/maui_mobile_app/AIPneumoScan/Resources/Images/ic_logo.png" width=300/><br><br>

**AI PneumoScan** is a modern, AI-driven mobile application designed to assist with early detection of pneumonia from chest X-ray images. Built with a hybrid technology stack that includes **.NET MAUI for the cross-platform frontend** and **Python for model training and inference,** it combines the power of deep learning with the convenience of offline mobile deployment.

The project leverages a **fine-tuned ResNet50 convolutional neural network**, trained on the popular **Chest X-Ray Pneumonia dataset** from Kaggle, to classify X-ray images as either **NORMAL** or **PNEUMONIA**. The trained model is exported to the **ONNX (Open Neural Network Exchange)** format, which is then used within the MAUI app via **ONNX Runtime for .NET** for fast, offline predictions.

Unlike cloud-based solutions, AI PneumoScan performs all predictions **locally on-device**, ensuring **privacy, security,** and **instant results** even in offline environments. It's ideal for educational use, fieldwork, rural healthcare training simulations, or low-resource settings where internet access is unreliable.

---

## 🚀 Features
- ✅ Cross-platform application: Runs on Android, iOS, Windows, and macOS thanks to .NET MAUI.
- 🧠 AI-based X-ray analysis: ResNet50 architecture achieves high accuracy in detecting pneumonia.
- 📦 Offline-first design: No need to upload patient data to the cloud — privacy is maintained.
- 🏥 Care suggestion system: Optionally provides basic health advice based on the prediction class.
- 🎨 Polished UI/UX: Includes a splash screen, onboarding wizard, dark/light themes, and result visualization.
- 📸 Image handling: Capture X-rays via camera or select from device storage.
- 🔁 Reproducible training pipeline: Python script provided for custom model training and ONNX export.
- 🧪 Extendable backend: SQLite integration for care instructions, modular prediction services.

---

## 📂 Folder Structure

```bash
AI-PneumoScan/
├── maui_mobile_app
│   └── AIPneumoScan/                # .NET MAUI app source
│       ├── Views/                   # UI pages
│       ├── ViewModels/             # MVVM logic
│       ├── Services/               # Prediction logic
│       ├── Converters/             # Value converters
│       └── Resources/              # Images and styles
├── model_training/                   # Python model training, ONNX export
│   ├── train_and_export.py
│   ├── predict_with_care.py
│   ├── care_suggestions.db     # Optional SQLite care info
│   └── chest_xray/             # Place dataset here
│       ├── train/
│       │   ├── NORMAL/
│       │   └── PNEUMONIA/
│       ├── val/
│       │   ├── NORMAL/
│       │   └── PNEUMONIA/
│       └── test/
│           ├── NORMAL/
│           └── PNEUMONIA/
└── images
```

> ⚠️ Ensure `chest_xray` is placed inside the `training/` folder to match Python script paths.

---

## 📦 Dataset (Required)

This project uses the **Chest X-Ray Pneumonia** dataset available on Kaggle:

🔗 [Download Dataset from Kaggle](https://www.kaggle.com/datasets/paultimothymooney/chest-xray-pneumonia)

After downloading:

1. Extract the dataset.
2. Place the `chest_xray/` folder **inside the `training/` directory**.
3. The final path should look like: `training/chest_xray/train/NORMAL/`, etc.

---

## 🧑‍💻 Python Model Training & Prediction

### 1. Install Dependencies

```bash
pip install torch torchvision scikit-learn numpy onnxruntime
```

### 2. Train and Export ONNX Model

```bash
cd training
python train_and_export.py
```

This script:

- Loads the `chest_xray` dataset with augmentations
- Trains a ResNet50 model with validation accuracy output
- Saves model to `pneumonia_model.pth`
- Exports to `pneumonia_model.onnx`

### 3. Run Prediction with Care Suggestion (Optional)

```bash
python predict_with_care.py chest_xray/test/PNEUMONIA/person120_bacteria_573.jpeg
```

Output example:

```
Prediction: PNEUMONIA (95%)
Care Suggestion: Seek immediate medical attention.
```

---

## 🧰 Tech Stack

| Layer               | Technology                 | Purpose                                          |
|--------------------|----------------------------|--------------------------------------------------|
| **Frontend (UI)**   | .NET MAUI                  | Cross-platform app for Android, iOS, Windows     |
| **Model Training**  | PyTorch                    | CNN (ResNet50) training on X-ray dataset         |
| **Model Export**    | ONNX                       | Cross-platform inference model format            |
| **Prediction Engine** | ONNX Runtime for .NET     | Run ONNX model on-device (offline prediction)    |
| **Image Processing** | SkiaSharp (MAUI), Torchvision | Image resize, normalize, tensor conversion  |
| **Data Storage**    | SQLite (optional)          | Store care instructions for each prediction      |
| **Dataset**         | Chest X-Ray (Kaggle)       | Dataset for pneumonia detection (medical X-rays) |

---

## 📱 MAUI App Setup

### 1. Prerequisites

- Visual Studio 2022+ with MAUI workload
- .NET 8 or higher

### 2. Build & Run

- Open `AIPneumoScan.sln`
- Run on Android/iOS/emulator

### 3. ONNX Model

Ensure `pneumonia_model.onnx` is added to `.csproj` as:

```xml
<ItemGroup>
    <Content Include="Resources\Raw\pneumonia_model.onnx">
	    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
	</Content>
</ItemGroup>
```

The app uses `InferenceSession` to load this model and return predictions.

---

## 💾 Care Suggestion Database

Optional SQLite database for personalized recommendations.

Schema:

```sql
CREATE TABLE CareInfo (
  label TEXT PRIMARY KEY,
  suggestion TEXT
);
```

You can query it in Python or integrate with the MAUI result view.

---

## 📸 Screenshots



---

## 📃 License

This project is for **educational and research** purposes only. It is **not** a certified medical device.

---
## ⚠️ Disclaimer

**AI PneumoScan** is intended **strictly for educational, research, and informational purposes only.**

- This application is **not a medical device**.
- It is **not approved** by any medical regulatory authority such as the FDA or WHO.
- The predictions provided by the app are based on AI model inference and **should not be used for diagnosis or treatment decisions**.
- Always consult a licensed healthcare provider for medical evaluation and treatment.
- The creators of this app do **not assume any responsibility** for the decisions made based on the output of this application.

By using this software, you acknowledge and agree that it is provided **as-is**, and any use of its outputs is entirely at your own discretion.

---

## 📚 Attribution

This project uses the **Chest X-Ray Images (Pneumonia)** dataset, publicly available on Kaggle, curated by Paul Timothy Mooney.

- **Dataset Title**: Chest X-Ray Images (Pneumonia)  
- **Source**: [Paul Timothy Mooney - Kaggle Dataset Link](https://www.kaggle.com/datasets/paultimothymooney/chest-xray-pneumonia)  

## 📬 Contact

[Connect on LinkedIn](https://www.linkedin.com/in/kammarijagdeesh/) Feel free to reach out! 
