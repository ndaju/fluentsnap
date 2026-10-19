# FluentSnap

FluentSnap is a Windows WPF screenshot utility with a dark FluentSnap identity, WPF-UI 4.3.0 theme foundation, real virtual-desktop capture, and an image editor.

## Build and run

Prerequisites: Windows 10/11 and .NET 8 SDK with Windows Desktop support.

```powershell
dotnet build FluentSnap.csproj
dotnet run --project FluentSnap.csproj
```

Global shortcut: **Ctrl+Shift+S** starts the selected capture mode. If another app owns it, FluentSnap reports the conflict and the New Capture button remains usable.

The capture selector is backed by a real desktop snapshot. After a selection it closes before the final physical-pixel capture runs, then returns the result to the editor. This avoids black compositor pixels and does not depend on `DialogResult`.

See [VALIDATION.md](VALIDATION.md) for executed runtime checks and remaining manual tests. Reference investigation is in [INVESTIGATION.md](INVESTIGATION.md).

## Settings and shortcuts

Preferences are stored locally at `%LOCALAPPDATA%\FluentSnap\settings.json`. The default global shortcut is **Ctrl+Shift+S** for backward compatibility. Settings can change it to another valid modifier combination; Windows may reserve combinations such as Win+Shift+S, and FluentSnap reports registration failures instead of pretending they succeeded.

The shortcut opens a single floating toolbar near the top center of the work area. The toolbar selects Rectangle, Freeform, Window, or Fullscreen and closes before the existing capture selector starts.

Toolbar placement uses the primary monitor work area as a deterministic fallback. A future monitor-aware placement can use the cursor/display that activated the shortcut; the current rule is documented because a global hotkey has no pointer context in this implementation.
