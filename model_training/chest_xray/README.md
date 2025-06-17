# 📁 Pneumonia Detection Dataset Setup

This project uses the **Chest X-Ray Pneumonia** dataset provided by [Paul Timothy Mooney on Kaggle](https://www.kaggle.com/datasets/paultimothymooney/chest-xray-pneumonia).

---

## 📦 Dataset Download

You must manually download the dataset from the following URL:

🔗 **[Chest X-Ray Pneumonia on Kaggle](https://www.kaggle.com/datasets/paultimothymooney/chest-xray-pneumonia)**

---

## 📁 Expected Folder Structure

After extracting the dataset, please ensure it follows this structure inside your project root:

```bash
chest_xray/
├── train/
│   ├── NORMAL/
│   └── PNEUMONIA/
├── val/
│   ├── NORMAL/
│   └── PNEUMONIA/
├── test/
│   ├── NORMAL/
│   └── PNEUMONIA/
```

> ✅ This structure is **mandatory** for training, validation, and ONNX export to work properly.

---

## ⚠️ Note

- The dataset is **not included** in this repository due to size and licensing.
- Please **do not rename or move folders** after downloading.
- All training/validation logic assumes the above folder paths.
