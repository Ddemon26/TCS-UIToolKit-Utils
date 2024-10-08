# UIToolKit Util Library

![Unity Version](https://img.shields.io/badge/Unity-2022.3+-black.svg?style=for-the-badge&logo=unity) ![Contributions Welcome](https://img.shields.io/badge/Contributions-Welcome-brightgreen.svg?style=for-the-badge) ![Language](https://img.shields.io/badge/Language-C%23-blue?style=for-the-badge) ![GitHub Contributors](https://img.shields.io/github/contributors/Ddemon26/TCS-UIToolKit-Utils?style=for-the-badge) ![GitHub Last Commit](https://img.shields.io/github/last-commit/Ddemon26/TCS-UIToolKit-Utils?style=for-the-badge) ![Project Status](https://img.shields.io/badge/Project%20Status-Stable-green?style=for-the-badge)

## Overview

The UIToolKit Util Library provides an advanced Unity Editor utility meticulously designed to automate the generation of static classes from UXML and Unity StyleSheet files. This tool significantly streamlines the process of converting user interface elements into accessible C# constants, resulting in code that is more comprehensible, easier to maintain, and markedly efficient.

This tool represents an evolution in Unity Editor customization, facilitating the automation of **parsing**, **previewing**, and **code generation** for UXML and StyleSheet assets, thereby enhancing developer productivity and mitigating the risk of human error.

---

![UI ToolKit Banner](https://github.com/user-attachments/assets/96359568-b36c-4694-984f-70ad70cba669)

---

## 🚀 ToolKit Helper — Elevate Your Unity Workflow with Automation!

### Git URL

```
https://github.com/Ddemon26/TCS-UIToolKit-Utils
```

---

<img src="https://github.com/user-attachments/assets/a02015f7-ace6-40e5-86ed-c915790b9437" width="400" height="325"> <img src="https://github.com/user-attachments/assets/962d3115-d067-4ba2-8eda-0d8a3bb187e7" width="400" height="325">

## ✨ Key Features

- **Automatic Static Class Generation**: Automatically produces static classes from UXML and StyleSheets, eliminating the need for manual coding.
- **Preview System**: Provides an integrated mechanism to visualize UXML and StyleSheets within the Unity Editor, ensuring a more intuitive workflow.
- **Customizable Output**: Offers the flexibility to specify custom namespaces and output file paths for generated classes, providing greater control over code structure.
- **Regex-Based Style Extraction**: Utilizes sophisticated regex parsing to extract class names from StyleSheets, thereby accelerating the coding process.

## 🛠️ Installation

To begin, clone the repository and integrate the package into your Unity project environment.

```bash
git clone https://github.com/Ddemon26/TCS-UIToolKit-Utils
```

This utility requires **Unity 2022.3+**.

## 📖 Usage

### How to Use ToolKit Helper

1. **Add UXML or StyleSheet Files**: Integrate your UXML and StyleSheet assets into your Unity project.
2. **Open the ToolKit Helper Window**: Navigate to `Window -> UIToolKit Utils -> ToolKit Helper` to access the primary editor interface.
3. **Preview Your Assets**: Utilize the preview pane to visualize UXML and StyleSheet content, ensuring accuracy before code generation.
4. **Generate Static Classes**: Select the `Generate Static Classes` button to initiate automated generation of classes and constants.
5. **Customization**: Define namespaces and file paths for more refined control over the generated class storage locations.

## 🔍 Class Breakdown

### 1. ParserHelpers

Responsible for parsing UXML and StyleSheet files:

- **CreateTextArea**: Generates a textual preview interface within the Unity editor environment.
- **ExtractNamesFromUxml**: Traverses a UXML structure to extract UI element identifiers.
- **ExtractClassNamesFromStyleSheet**: Applies regex-based techniques to extract class names from StyleSheet definitions.

### 2. StaticClassGenerator

Generates static classes based on UXML and StyleSheet input:

- **GenerateStaticClass**: Constructs a static class in memory, incorporating constants for each UI element or StyleSheet class.
- **SaveToFile**: Outputs the generated static class to a file for integration into the broader project.

### 3. GeneratorExtensions

Provides auxiliary methods for string manipulation and validation:

- **ConvertToStatic**: Converts strings to valid C# constant identifiers (replacing characters like `-` with `_`).
- **ConvertToAlphanumeric**: Filters out non-alphanumeric characters, permitting only underscores for enhanced readability and standardization.

### 4. ToolKitHelperEditorWindow

The primary user interface for the editor window:

- **GenerateStaticClasses**: Facilitates the generation of static classes from selected assets, encompassing both StyleSheets and UXML.
- **ShowPreview**: Displays a comprehensive preview of the chosen UXML or StyleSheet asset within Unity.
- **OnStyleSheetChanged / OnVisualTreeAssetChanged**: Ensures that the preview is dynamically updated whenever the linked assets are modified.

## 🤝 Contributing

We actively encourage contributions to this project. Contributions may involve bug fixes, feature enhancements, or documentation improvements. Please follow these steps to begin contributing:

1. Fork the repository.
2. Create a feature branch: `git checkout -b feature/AmazingFeature`.
3. Commit your changes: `git commit -m 'Add some AmazingFeature'`.
4. Push to the branch: `git push origin feature/AmazingFeature`.
5. Submit a pull request for review.

## 📜 License

This project is licensed under the MIT License. For further information, refer to the [LICENSE](LICENSE) file.

## 💬 Support

For assistance or inquiries, please consider joining our [Discord Community](https://discord.gg/knwtcq3N2a) or open an issue on GitHub.
