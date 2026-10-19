# FluentSnap investigation record

## Final runtime diagnosis

`CaptureOverlay` is created and opened through `ShowDialog()`. Its completion handlers previously called `Hide()` in `CaptureAfterHide()`, then assigned `DialogResult`. WPF rejects that sequence because a hidden modal window is no longer in a valid dialog state. This exactly explains the localized exception shown after selecting an area.

The final correction closes the selector before capture, flushes the dispatcher so Windows repaints the desktop, captures while the main window remains hidden, and returns the bitmap through the overlay `Result` property. `DialogResult` was removed from the completion path. This preserves the desktop-backed selector and avoids both self-capture and black compositor pixels.

## Runtime evidence

A Windows pointer smoke test launched the compiled app, clicked the New Capture button using its live window rectangle, dragged a selection, and observed the process alive after completion with no error dialog. A second run pressed Escape, then repeated New Capture and selection successfully. These are runtime observations of lifecycle health; they do not replace pixel-level editor or clipboard verification.

## Known limits

REA's native Windows UI observer is unavailable in the current provider setup. Exact Snipping Tool hover states and protected-window behavior remain unverified.

## Latest UI/editor correction

The original editor rendered the full canvas at the viewport size. Crop baked the visible annotation lines into a bitmap but left the same lines as canvas children, so subsequent rendering drew them twice. It also mixed image coordinates with the letterboxed viewport, producing oversized/misaligned strokes.

`ImageEditor` now uses one scaled image-coordinate surface. Committing a stroke rasterizes it once at source-image resolution and resets the temporary visuals. Crop operates in source pixels. This eliminates duplicate drawing after crop/export and keeps strokes aligned while fitting the image to the workspace.

## Crop disappearance fix

The editor displayed the image with a fit-to-view `ScaleTransform`, but `RenderImage()` rendered that transformed surface into a bitmap sized for the original pixels. Crop therefore sampled the wrong coordinate space and could produce an empty or apparently missing image. `RenderImage()` now temporarily renders the image surface at native pixel scale, restores the display transform, and returns a source-resolution bitmap. Crop and save use that same path.

## Editor render correction

The prior crop fix still rendered the live WPF surface back through its transformed layout. That could lose both the image and strokes after crop or a completed draw. The editor now stores strokes in image pixels and renders the base image plus strokes through a separate `DrawingVisual` at native resolution. The fitted canvas is preview-only; crop/save/clipboard never rasterize the scaled viewport.

## Window controls, settings, and toolbar

- Added a custom FluentSnap title bar with Settings, Fullscreen, Minimize, Maximize/Restore, and Close controls. The title bar is draggable and double-click toggles maximize/restore.
- Fullscreen stores window state, style, resize mode, bounds, and title-bar visibility, then restores them on exit or Escape.
- Added `UserSettings` and `SettingsStore`, persisted at `%LOCALAPPDATA%\FluentSnap\settings.json`. Settings are JSON-validated on load and written through a temporary file before replacement.
- Added `ShortcutService` around `RegisterHotKey`/`UnregisterHotKey`. It unregisters before re-registering, reports Windows conflicts, and disposes on shutdown.
- Added a single-instance floating `CaptureToolbar` with all four capture modes. It closes before the existing capture overlay starts, so it cannot become a window-capture target.
- Added `SettingsWindow` with shortcut recording, default capture type, toolbar and persistence preferences, Save, Cancel, and Reset to Defaults.
- Window selection now passes the overlay HWND to native targeting so FluentSnap's own overlay is skipped.

Toolbar placement is intentionally deterministic: the primary monitor work area top center is used because a `WM_HOTKEY` message does not carry the activating monitor. This avoids toolbar duplication or jumping between displays.
