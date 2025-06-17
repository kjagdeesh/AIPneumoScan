import os
import sys
import onnxruntime as ort
import numpy as np
import torchvision.transforms as transforms
from PIL import Image
import sqlite3

# ---------------------------
# 📦 Image Preprocessing
# ---------------------------
def load_image(image_path):
    """Load and preprocess image to match model input format."""
    transform = transforms.Compose([
        transforms.Resize((224, 224)),
        transforms.ToTensor(),
        transforms.Normalize(mean=[0.485, 0.456, 0.406],
                             std=[0.229, 0.224, 0.225])
    ])
    image = Image.open(image_path).convert("RGB")
    image = transform(image)
    return image.unsqueeze(0).numpy().astype(np.float32)  # Shape: [1, 3, 224, 224]

# ---------------------------
# 🤖 Prediction Function
# ---------------------------
def predict(image_path, model_path="pneumonia_model.onnx"):
    """Predict class label and confidence from an X-ray image using ONNX model."""
    image = load_image(image_path)

    # Load ONNX model and run inference
    session = ort.InferenceSession(model_path)
    outputs = session.run(None, {"input": image})

    logits = outputs[0][0]
    softmax = np.exp(logits - np.max(logits)) / np.sum(np.exp(logits - np.max(logits)))
    predicted_index = int(np.argmax(softmax))
    confidence = float(softmax[predicted_index])

    class_names = ["NORMAL", "PNEUMONIA"]
    return class_names[predicted_index], confidence

# ---------------------------
# 💡 Get Care Suggestion
# ---------------------------
def get_care_suggestion(label, db_path="care_suggestions.db"):
    """Fetch care suggestion from SQLite DB using predicted label."""
    try:
        conn = sqlite3.connect(db_path)
        cursor = conn.cursor()
        cursor.execute("SELECT suggestion FROM CareInfo WHERE label = ?", (label,))
        result = cursor.fetchone()
        conn.close()
        return result[0] if result else "No care suggestion available."
    except Exception as e:
        return f"Error reading care database: {e}"

# ---------------------------
# 🚀 Entry Point
# ---------------------------
if __name__ == "__main__":
    # ✅ Use this sample image path (change if needed)
    # default_image = "chest_xray/test/PNEUMONIA/person120_bacteria_573.jpeg"
    default_image = "chest_xray/test/NORMAL/IM-0001-0001.jpeg"

    if len(sys.argv) >= 2:
        image_path = sys.argv[1]
    else:
        print(f"⚠️ No image path provided, using default: {default_image}")
        image_path = default_image

    if not os.path.exists(image_path):
        print(f"❌ Error: Image file not found: {image_path}")
        sys.exit(1)

    if not os.path.exists("pneumonia_model.onnx"):
        print("❌ Error: pneumonia_model.onnx not found in current directory.")
        sys.exit(1)

    print(f"🔍 Running prediction on: {image_path}")

    label, confidence = predict(image_path)
    care = get_care_suggestion(label)

    print(f"\n🩻 Prediction: {label} ({confidence:.2%})")
    print(f"💊 Care Suggestion: {care}\n")
