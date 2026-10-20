# FluentSnap

**A modern Windows screenshot tool built with WPF and Fluent Design.**

FluentSnap is an open-source screenshot application for Windows, designed to make capturing, editing, and saving screenshots simple through a clean interface inspired by Microsoft's Fluent Design System.

![Platform](https://img.shields.io/badge/platform-Windows-0078D4?logo=windows)
![Framework](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![UI](https://img.shields.io/badge/UI-WPF%20%7C%20WPF%20UI-0078D4)
![Status](https://img.shields.io/badge/status-in%20development-orange)

## Features

- **Multiple capture modes**
  - Rectangular region selection
  - Freeform region selection
  - Window capture
  - Fullscreen capture
  - Virtual desktop and multi-monitor support
- **Keyboard shortcuts** for launching and starting captures.
- **Screenshot editor** with cropping, annotations, and undo/redo.
- **PNG export** for saving screenshots.
- **Clipboard support** for copying captured images.
- **Fluent Design interface** powered by WPF UI and Fluent icons.
- **Configurable settings**, including screenshot output location and shortcuts.
- **Windows notifications** intended to provide access to saved screenshots.
- **Multi-monitor awareness**, including virtual-screen coordinates and DPI scaling.

> FluentSnap is under active development. Some features, including reliable screenshot saving and notification click activation, may still require testing or further implementation. Check the current source code and open issues for the latest status.

## Screenshots

_Add screenshots of the FluentSnap interface, capture toolbar, and editor here._

## Requirements

- Windows 10 or Windows 11
- .NET 8 SDK
- Visual Studio 2022 or another compatible .NET development environment
- Git

The application uses Windows Presentation Foundation (WPF), so it is intended for Windows rather than macOS or Linux.

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/FluentSnap.git
cd FluentSnap
```

Replace `YOUR_USERNAME` with the GitHub account or organization hosting the repository.

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Build the application

```bash
dotnet build
```

### 4. Run FluentSnap

Locate the existing `.csproj` file and run:

```bash
dotnet run --project path/to/FluentSnap.csproj
```

Replace the example project path with the actual location of the project file.

Alternatively, open the solution (`.sln`) in Visual Studio, select the appropriate startup project, and press **F5**.

## How to Use

1. Launch FluentSnap.
2. Choose a capture mode or use a configured keyboard shortcut.
3. Select the area or window you want to capture, or capture the full screen.
4. Open the resulting screenshot in the editor.
5. Crop or annotate the image as needed.
6. Save the screenshot or copy it to the clipboard.

Available capture modes and shortcut behavior depend on the current implementation and application settings.

## Keyboard Shortcuts

FluentSnap is designed to support configurable global keyboard shortcuts.

| Action | Shortcut |
|---|---|
| Open the capture toolbar | Configurable in Settings |
| Start a capture | Configurable in Settings |
| Cancel an active capture | `Esc` |

Check the application's Settings and current source code for the active shortcut assignments. Shortcuts may conflict with Windows or other applications.

## Technology Stack

| Technology | Purpose |
|---|---|
| C# | Application logic |
| .NET 8 | Runtime and framework |
| WPF | Windows desktop UI |
| WPF UI by lepoco | Fluent-style controls and application theming |
| Fluent System Icons | Consistent interface icons |
| Windows APIs | Desktop capture, keyboard shortcuts, and notification integration |

WPF UI project: https://github.com/lepoco/wpfui

## Project Structure

The exact directory layout may evolve during development. The main areas of the application are expected to include:

- **Capture** — screen capture, region selection, window capture, and overlay handling.
- **Editor** — image display, cropping, annotations, and undo/redo.
- **Settings** — user preferences, output directory, and shortcut configuration.
- **Services** — screenshot persistence, clipboard integration, and Windows notifications.
- **Views** — WPF windows, dialogs, and application interface.
- **Resources** — Fluent icons, styles, themes, and application assets.

Refer to the repository's actual source tree for the current implementation.

## Development

Contributions and bug reports are welcome.

Before submitting changes:

1. Create a branch for your work.
2. Keep changes focused and compatible with .NET 8.
3. Preserve existing capture behavior and multi-monitor support.
4. Build the project and run relevant tests.
5. Document any Windows-specific requirements.
6. Open a pull request describing the changes and how they were tested.

### Reporting Bugs

When reporting a bug, include:

- A clear description of the problem.
- Steps to reproduce it.
- Your Windows version.
- Relevant error messages or logs.
- Whether the issue occurs on one or multiple monitors.
- The display scaling settings, if relevant.

Please do not attach private screenshots or sensitive information unless necessary.

## Roadmap

Potential development priorities include:

- [ ] Reliable screenshot saving to the configured directory
- [ ] Native Windows notifications after successful saves
- [ ] Notification click activation that opens the correct screenshot in FluentSnap
- [ ] Reliable single-instance application activation
- [ ] Persistent and validated shortcut settings
- [ ] Expanded automated tests for capture and file handling
- [ ] Improved accessibility and keyboard navigation
- [ ] Application packaging and release automation
- [ ] Additional editor tools and customization options

Items should be checked off as they are implemented and verified.

## Contributing

Contributions are encouraged, including bug fixes, UI improvements, accessibility enhancements, tests, and documentation.

For substantial changes, open an issue first to discuss the proposed approach. Keep pull requests focused and include reproduction steps or tests for bug fixes.

## License

**A license has not yet been specified.**

Before publishing the repository as open source, choose a license and add a `LICENSE` file. Until a license is included, others generally do not have explicit permission to redistribute or modify the code beyond rights granted by applicable law.

Common options include:

- **MIT** — permissive, simple, and suitable for many community projects.
- **Apache-2.0** — permissive with explicit patent-related provisions.
- **GPL-3.0** — requires distributed derivative works to comply with its copyleft terms.

Choose the license that matches your intentions before announcing the project as open source.

## Acknowledgements

- [WPF UI](https://github.com/lepoco/wpfui) — Fluent-inspired controls and theming for WPF.
- [Microsoft .NET](https://dotnet.microsoft.com/) — the application framework.
- [Microsoft Windows](https://www.microsoft.com/windows) — the target desktop platform.

---

**FluentSnap — Capture your screen. Keep your workflow simple.**
