# UIToolKit Util Library

![Unity](https://img.shields.io/badge/Unity-2022.3+-black.svg?style=for-the-badge&logo=unity)
![Contributions welcome](https://img.shields.io/badge/Contributions-Welcome-brightgreen.svg?style=for-the-badge)
[![Odin Inspector](https://img.shields.io/badge/Odin_Inspector-Required-blue?style=for-the-badge)](https://odininspector.com/)

#### Git URL

```
https://github.com/Ddemon26/TCS-UIToolKit-Utils
```

***
![UI ToolKit Banner](https://github.com/user-attachments/assets/96359568-b36c-4694-984f-70ad70cba669)
***

![GitHub Forks](https://img.shields.io/github/forks/Ddemon26/TCS-UIToolKit-Utils)
![GitHub Contributors](https://img.shields.io/github/contributors/Ddemon26/TCS-UIToolKit-Utils)
![GitHub Stars](https://img.shields.io/github/stars/Ddemon26/TCS-UIToolKit-Utils)
![GitHub Repo Size](https://img.shields.io/github/repo-size/Ddemon26/TCS-UIToolKit-Utils)
[![Join our Discord](https://img.shields.io/badge/Discord-Join%20Us-7289DA?logo=discord&logoColor=white)](https://discord.gg/knwtcq3N2a)
![Discord](https://img.shields.io/discord/1047781241010794506)

![GitHub Issues](https://img.shields.io/github/issues/Ddemon26/TCS-UIToolKit-Utils)
![GitHub Pull Requests](https://img.shields.io/github/issues-pr/Ddemon26/TCS-UIToolKit-Utils)
![GitHub Last Commit](https://img.shields.io/github/last-commit/Ddemon26/TCS-UIToolKit-Utils)
![GitHub License](https://img.shields.io/github/license/Ddemon26/TCS-UIToolKit-Utils)


## 🚀 **ToolKit Helper** — Elevate Your Unity Workflow with Automation!

ToolKit Helper is a **game-changing** Unity Editor tool that simplifies the tedious process of generating static classes from UXML and Unity StyleSheet files. Powered by **Odin Inspector**, this tool helps you convert your UI elements into easy-to-access C# constants, making your code cleaner, more maintainable, and faster to write.

Say goodbye to manual labor—ToolKit Helper automates the process of **parsing**, **previewing**, and **generating code** for UXML and StyleSheet assets!

## 🔥 **Key Features**
- 🖼️ **UXML Element Parser**: Automatically extract element names from your UXML files and generate C# classes for direct access in code.
- 🔧 **StyleSheet Class Extractor**: Seamlessly parse StyleSheet class names and create constants to ensure styling is easily applied programmatically.
- ✏️ **Static Class Generation**: Effortlessly generate clean, maintainable, and optimized static classes for UI elements and styles.
- ⚡ **Odin Inspector Enhanced UI**: Leverages Odin Inspector for a slick, user-friendly editor experience.
- 📂 **Customizable Paths and Namespaces**: Define custom file paths and namespaces for full control over your generated files.

## 📜 **Table of Contents**
- [Installation](#installation)
- [Usage](#usage)
- [Class Breakdown](#class-breakdown)
- [Contributing](#contributing)
- [License](#license)

## 🛠️ **Installation**
1. **Install Odin Inspector**: This tool requires the [Odin Inspector](https://odininspector.com/) plugin for Unity. Make sure to install it in your project.
2. **Download ToolKit Helper**: Clone or download the repository from GitHub.
3. **Open Unity**: Open the Unity project where you've added the tool.

## 🎮 **Usage**

 <img src="https://github.com/user-attachments/assets/a02015f7-ace6-40e5-86ed-c915790b9437" width="400" height="325">
 <img src="https://github.com/user-attachments/assets/962d3115-d067-4ba2-8eda-0d8a3bb187e7" width="400" height="325">

 
1. **Open the Tool**: Go to `Tools > UIToolKit Helper` in the Unity menu.
2. **Select Your Files**: Choose a StyleSheet or UXML file that you want to parse.
3. **Preview Your Files**: Use the in-editor preview feature to review your StyleSheet or UXML before generating static classes.
4. **Generate Static Classes**: Click the `Generate Static Classes` button, and ToolKit Helper will automatically create the classes and constants.
5. **Customization**: Specify custom namespaces and file paths for more control over where your generated classes are saved.

## 🔍 **Class Breakdown**

### 1. **ParserHelpers**
Handles parsing UXML and StyleSheet files:
- **CreateTextArea**: Creates a text preview area in the Unity editor.
- **ExtractNamesFromUxml**: Traverses a UXML tree and extracts element names.
- **ExtractClassNamesFromStyleSheet**: Uses regex to extract StyleSheet class names.

### 2. **StaticClassGenerator**
Generates static classes from UXML and StyleSheet data:
- **GenerateStaticClass**: Builds a static class in memory with constants for each UI element or StyleSheet class.
- **SaveToFile**: Writes the generated class to a file.

### 3. **GeneratorExtensions**
Helper methods for string manipulation and validation:
- **ConvertToStatic**: Converts strings into valid C# constant names (replaces `-` with `_`, etc.).
- **ConvertToAlphanumeric**: Removes non-alphanumeric characters while allowing underscores.

### 4. **ToolKitHelperEditorWindow**
The main UI editor window:
- **GenerateStaticClasses**: Generates static classes from the selected assets (StyleSheets or UXML).
- **ShowPreview**: Displays a preview of the selected UXML or StyleSheet in Unity.
- **OnStyleSheetChanged / OnVisualTreeAssetChanged**: Updates previews when assets change.

## 🤝 **Contributing**
We welcome contributions to this project! Whether you're fixing bugs, adding features, or improving documentation, your help is appreciated. Follow these steps to get started:
1. Fork the repository.
2. Create a feature branch: `git checkout -b feature/AmazingFeature`.
3. Commit your changes: `git commit -m 'Add some AmazingFeature'`.
4. Push to the branch: `git push origin feature/AmazingFeature`.
5. Open a pull request.

## 📜 **License**
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
