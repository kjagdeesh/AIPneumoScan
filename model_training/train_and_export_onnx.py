import os
from sklearn.metrics import accuracy_score
import torch
import torchvision.transforms as transforms
import torchvision.models as models
from torchvision.datasets import ImageFolder
from torch.utils.data import DataLoader
from torch import nn, optim
import torch.onnx
import numpy as np

# -----------------------------
# 📁 Configuration & Hyperparams
# -----------------------------
DATA_DIR = "chest_xray"                  # Root folder with 'train', 'val' subfolders
ONNX_FILE = "pneumonia_model.onnx"       # Output ONNX filename
BATCH_SIZE = 32
NUM_EPOCHS = 20
IMAGE_SIZE = (224, 224)
LEARNING_RATE = 1e-4

# -----------------------------
# 📦 Data Transformations
# -----------------------------
train_transform = transforms.Compose([
    transforms.Resize(IMAGE_SIZE),
    transforms.RandomHorizontalFlip(),          # Augment: Flip left/right
    transforms.RandomRotation(10),              # Augment: Rotate image ±10°
    transforms.ToTensor(),
    transforms.Normalize(mean=[0.485, 0.456, 0.406],
                         std=[0.229, 0.224, 0.225]) # ImageNet stats
])

val_transform = transforms.Compose([
    transforms.Resize(IMAGE_SIZE),
    transforms.ToTensor(),
    transforms.Normalize(mean=[0.485, 0.456, 0.406],
                         std=[0.229, 0.224, 0.225])
])

# -----------------------------
# 📂 Load Datasets
# -----------------------------
train_data = ImageFolder(os.path.join(DATA_DIR, "train"), transform=train_transform)
val_data = ImageFolder(os.path.join(DATA_DIR, "val"), transform=val_transform)

train_loader = DataLoader(train_data, batch_size=BATCH_SIZE, shuffle=True)
val_loader = DataLoader(val_data, batch_size=BATCH_SIZE, shuffle=False)

# -----------------------------
# 🧠 Load Pretrained Model & Modify
# -----------------------------
model = models.resnet50(pretrained=True)        # Use ResNet50 for better accuracy than ResNet18

# 🔓 Enable fine-tuning all layers (not freezing any)
for param in model.parameters():
    param.requires_grad = True  # fine-tuning entire model

# 🔁 Replace final classification layer for binary output
model.fc = nn.Linear(model.fc.in_features, 2)  # 2 classes: NORMAL and PNEUMONIA

# -----------------------------
# ⚙️ Training Setup
# -----------------------------
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
model = model.to(device)

criterion = nn.CrossEntropyLoss()           # Binary classification loss
optimizer = optim.Adam(model.parameters(), lr=LEARNING_RATE)
scheduler = torch.optim.lr_scheduler.StepLR(optimizer, step_size=5, gamma=0.1)

# -----------------------------
# 🔁 Training Loop
# -----------------------------
print("Starting training...")
for epoch in range(NUM_EPOCHS):
    model.train()
    running_loss = 0

    for images, labels in train_loader:
        images, labels = images.to(device), labels.to(device)

        optimizer.zero_grad()
        outputs = model(images)
        loss = criterion(outputs, labels)
        loss.backward()
        optimizer.step()

        running_loss += loss.item()

    scheduler.step()

    # -------------------------
    # 🧪 Validation Phase
    # -------------------------
    model.eval()
    preds = []
    targets = []

    with torch.no_grad():
        for images, labels in val_loader:
            images = images.to(device)
            outputs = model(images)
            preds.extend(outputs.argmax(1).cpu().numpy())
            targets.extend(labels.numpy())

    acc = accuracy_score(targets, preds)
    print(f"Epoch {epoch+1}/{NUM_EPOCHS}, Loss: {running_loss/len(train_loader):.4f}, Val Acc: {acc:.4f}")

# -----------------------------
# 💾 Save PyTorch Model
# -----------------------------
torch.save(model.state_dict(), "pneumonia_model.pth")

# -----------------------------
# 📤 Export to ONNX
# -----------------------------
model.eval()
dummy_input = torch.randn(1, 3, 224, 224, device=device)        # Dummy input for shape

torch.onnx.export(
    model,
    dummy_input,
    ONNX_FILE,
    input_names=["input"],
    output_names=["output"],
    dynamic_axes={"input": {0: "batch_size"}, "output": {0: "batch_size"}},
    opset_version=11
)

print(f"✅ ONNX model exported: {ONNX_FILE}")
