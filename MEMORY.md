# FluentSnap working memory

## Decision log

- 2026-10-19: WPF/.NET 8 selected with no third-party dependencies.
- 2026-10-19 Phase 2: Added virtual-screen capture, polygon masking, window detection, global shortcut, crop and snapshot history.
- 2026-10-19 Phase 3: Added official WPF-UI 4.3.0, desktop-backed selector, and corrected virtual-screen capture.
- 2026-10-19 Final stabilization: Confirmed the DialogResult exception came from hiding a modal CaptureOverlay before setting DialogResult. The overlay now closes before the final screen copy; the result is returned through its `Result` property and `DialogResult` is no longer used. Dispatcher scheduling replaced the cross-thread delay/continuation.
- 2026-10-19 Final stabilization: Actual Windows pointer smoke tests completed selection, Escape cancellation, and a repeated capture without process failure. Build passed with zero warnings/errors.

## Files to keep current

- `INVESTIGATION.md`: evidence, diagnosis, and architecture decisions.
- `README.md`: user-facing capabilities and limitations.
- `VALIDATION.md`: executed checks and untested scenarios.

## Latest UI/editor correction

- The draw/crop duplication was fixed by rendering annotations into a source-resolution image surface and clearing temporary line visuals after each commit.
- The editor now converts pointer positions from the centered, scaled viewport into actual image pixels before drawing or cropping.
- The toolbar was reorganized into capture, mode, annotation, history, and export groups for clearer use at normal window widths.
- Build and launch smoke test passed with zero warnings/errors after the change.

## Crop correction

- Fixed crop disappearing by rendering the editing surface in native image coordinates before cropping.
- The fit-to-view transform is restored immediately after rasterization, so the workspace remains visually fitted.
- Build passed with zero warnings/errors after the correction.

## Latest correction

- Replaced transformed-canvas feedback rendering with image-space stroke data plus a native-resolution `DrawingVisual` export path.
- Crop, save, clipboard, undo, redo, and annotation commit all use the same stable raster output.
- Build and launch smoke test passed after the rewrite.

## Final stabilization additions

- `Settings.cs`: validated JSON preferences and atomic persistence.
- `ShortcutService.cs`: dedicated global hotkey registration and lifecycle cleanup.
- `SettingsWindow.xaml(.cs)`: configurable shortcut/mode/preferences UI.
- `CaptureToolbar.xaml(.cs)`: single floating toolbar for all capture modes.
- `MainWindow`: custom controls, fullscreen state restoration, settings application, and toolbar orchestration.
- `Native`: window-under-pointer exclusion for the active FluentSnap overlay.
