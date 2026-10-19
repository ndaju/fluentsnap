# FluentSnap Final Validation

| Test | Execution method | Result | Evidence or observations | Limitations |
|---|---|---|---|---|
| Dialog-result exception | Actual Windows pointer smoke test: launch, click New Capture, drag selection | **Passed** | Process remained alive after selection; no error dialog. Root cause was `Hide()` before `DialogResult`, followed by compositor capture while the overlay still existed. The overlay now closes before capture and returns via `Result`. | Automated probe did not inspect editor pixels directly. |
| Black-screen regression | Source inspection plus actual selector launch/drag | **Passed** | Overlay paints a captured virtual-desktop bitmap behind the dim layer; selection remains visible. | No screenshot artifact exported from the probe. |
| Capture-to-editor handoff | Actual selection smoke test | **Implemented; runtime handoff not pixel-inspected** | Modal returned a `Result` path without exception and main process remained usable. | No GUI automation for reading the editor image dimensions. |
| Escape cancellation | Actual Windows keyboard/pointer smoke test | **Passed** | Escape closed the selector; a subsequent New Capture and drag completed while the process remained alive. | State was inferred from successful repeated interaction. |
| Capture modes | Source inspection | **Implemented; untested per mode** | Rectangle, freeform, window, and fullscreen dispatch through `CaptureService`; window mode uses user32/DWM. | No reliable automated hover/window/multi-monitor harness. |
| Crop and history | Source inspection | **Implemented; untested interactively** | Editor commits snapshots for crop, pen, highlighter, clear, undo, and redo. | Dimensions and pixel restoration require GUI interaction. |
| PNG export and reopening | Source inspection | **Implemented; untested interactively** | `PngBitmapEncoder` and `BitmapImage.OnLoad` are used with disposed file streams. | No automated save-dialog/file-content comparison in this session. |
| Clipboard copy and paste | Source inspection | **Implemented; not cross-app tested** | Uses WPF `Clipboard.SetImage` with error reporting. | Clipboard inspection/paste target unavailable to automation. |
| Multi-monitor and DPI | Source inspection | **Implemented; hardware not available** | Uses `SystemInformation.VirtualScreen`, physical clipping, and PerMonitorV2 manifest. | No second monitor or differing-DPI test hardware. |
| Build | `dotnet build FluentSnap.csproj` | **Passed** | 0 warnings, 0 errors. | None. |
| Launch | Start compiled executable and inspect live window/process | **Passed** | `FluentSnap` window created and remained alive. | No long-running soak test. |

## Confirmed fix

`CaptureOverlay` is displayed with `ShowDialog()`. The old completion path called `Hide()` inside `CaptureAfterHide()` and then assigned `DialogResult`, which violates WPF's modal-window contract. The final path closes the selector first, flushes the dispatcher so the desktop compositor repaints, captures the desktop while the main window remains hidden, and returns the bitmap through the overlay `Result` property. `DialogResult` is no longer used.

## Remaining limits

Protected/secure-desktop/GPU surfaces remain subject to Windows capture restrictions. Pen/marker settings use fixed defaults, crop applies on mouse release, and full editor pixel comparison still needs manual GUI verification.

## Latest editor/UI test

| Test | Execution method | Result | Evidence or observations | Limitations |
|---|---|---|---|---|
| Draw → crop duplication fix | Source inspection and build | Implemented | Annotation visuals are rasterized once and removed after commit; crop reads only the source-resolution surface. | Needs manual visual confirmation using the supplied draw/crop sequence. |
| Editor coordinate alignment | Source inspection and build | Implemented | Pointer coordinates are converted from centered/scaled viewport to image pixels. | Needs hands-on test at multiple window sizes. |
| Toolbar layout | Launch smoke test | Passed | WPF-UI shell launched with grouped controls and no mixed/overlapping layout at the default window size. | No resize matrix automation. |

| Crop disappearance | Source fix + `dotnet build` | Implemented | Native-resolution render temporarily removes the display scale before crop/export and restores it afterward. | Needs manual crop gesture confirmation in the GUI. |

| Draw/marker persistence | Source fix + build/launch | Implemented | Strokes are stored in image coordinates and rasterized with the base image at native resolution. | Needs manual visual confirmation of long strokes and marker opacity. |
| Crop image persistence | Source fix + build/launch | Implemented | Crop samples the native-resolution composited image and resets the preview from the cropped bitmap. | Needs manual crop gesture confirmation. |

## Window/settings/toolbar validation

| Test | Execution method | Result | Evidence or observations | Limitations |
|---|---|---|---|---|
| Build after integration | `dotnet build FluentSnap.csproj` | **Passed** | 0 warnings, 0 errors. | None. |
| Main window launch | Start compiled executable | **Passed** | FluentSnap window appeared and remained alive. | No long-run soak. |
| Settings persistence | Source inspection | Implemented | JSON path is `%LOCALAPPDATA%\\FluentSnap\\settings.json`; load validation and atomic replacement are implemented. | Save/restart interaction not automated here. |
| Shortcut registration | Source inspection | Implemented; runtime conflict result untested | Dedicated service registers/unregisters configured binding and reports failure. | OS-reserved Win-key combinations require manual testing. |
| Window controls | Launch/source inspection | Implemented | Custom title bar handlers cover minimize, maximize/restore, close, fullscreen, drag, and double-click maximize. | Pointer click matrix not automated. |
| Fullscreen | Source inspection | Implemented; runtime untested | State/style/bounds/title-bar state are saved and restored; Escape exits when main window is active. | Needs GUI verification around capture sessions. |
| Floating toolbar | Source inspection | Implemented; runtime untested | Single toolbar instance, mode selector, start/close actions, top-center placement, closes before capture. | Global hotkey and multi-monitor placement need manual testing. |
| Window capture exclusion | Source inspection | Implemented | Active overlay HWND is excluded from user32 window targeting. | Secondary monitor and protected windows unavailable. |

## Remaining limitations

`RegisterHotKey` remains subject to Windows reservations and other applications. Win+Shift+S may be rejected by Windows; the Settings dialog reports the registration failure and preserves a configurable fallback such as Ctrl+Shift+S. Clipboard, save/reopen, and multi-monitor interaction still require manual GUI testing.
