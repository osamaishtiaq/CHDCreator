# 💽 CHD Converter GUI

A simple WPF frontend for `chdman.exe` that converts CD-based game images (`.cue`, `.gdi`, etc.) to `.chd` format for use with emulators like RetroArch, MAME, and others.

> ⚙️ Built with **.NET 8 WPF**, this GUI wraps around `chdman.exe` and simplifies CHD conversion with a user-friendly interface.

---

## Download and Usage
* Go to releases tab and dowload the latest CHDConverter-v1.x.x.zip file. Extract it and run the app. 

## 📦 Features

* Drag-and-drop or file-picker UI
* Converts `.cue` and `.gdi` files to `.chd`
* Real-time output logging from `chdman`
* Color-coded success/failure status
* Auto-select output file location
* Works fully offline

---

## 🛠 Requirements

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or higher
* Windows OS
* `chdman.exe` (included or provided separately via MAME)

---

## 🔧 Setup & Build Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/CHD-Converter-GUI.git
cd CHD-Converter-GUI
```

### 2. Provide `chdman.exe`

The app expects `chdman.exe` to be located in:

```
CHD-Converter-GUI/chdman/chdman.exe
```

You can get `chdman.exe` by downloading MAME from:
➡️ [https://www.mamedev.org/release.html](https://www.mamedev.org/release.html)
Then copy `chdman.exe` into the above folder path.

Alternatively, you can modify the hardcoded path in the code.

### 3. Build the Project

Using CLI:

```bash
dotnet build -c Release
```

Or via Visual Studio:

* Open the `.sln` file
* Set configuration to **Release**
* Build the solution

---

## 🚀 Run the Application

After building, navigate to:

```
bin\Release\net8.0-windows
```

Run:

```
CHDConverterGUI.exe
```

---

## 🖼️ How to Use

1. Click **Browse** and select a `.cue` or `.gdi` file.
2. Click **Convert to CHD**.
3. The output `.chd` file will be created in the same folder as the input file.
4. Console output will be shown in the app window.
5. On success, the file will be auto-selected in File Explorer.

---

## ❗ Notes & Limitations

* `.cue` files must have their associated `.bin` files in the same folder.
* Input file paths should not contain special characters.
* This GUI does not yet support batch conversion.
* The path to `chdman.exe` is currently hardcoded in the app; in future versions it will be user-configurable.

---

## 📄 License

MIT License

---

## 🤝 Contributions

PRs and feature suggestions are welcome. Please file an issue before starting major changes.

---

Would you like me to generate the `.gitignore` and `LICENSE` files as well?
