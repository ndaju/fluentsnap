using System.Text.RegularExpressions;
using System.Windows.Interop;
namespace FluentSnap;
public sealed class ShortcutService
{
 public const int HotKeyId=0x534E; readonly Dictionary<int,string> registered=new(); HwndSource? source; IntPtr hwnd;
 public event EventHandler? CapturePressed;
 public bool Initialize(Window window){hwnd=new WindowInteropHelper(window).Handle;source=HwndSource.FromHwnd(hwnd);source?.AddHook(WndProc);return true;}
 public bool RegisterCapture(string shortcut,out string? error){error=null;if(!TryParse(shortcut,out var modifiers,out var key)){error="Use a combination such as Ctrl+Shift+S.";return false;}registered.TryGetValue(HotKeyId,out var previous);UnregisterAll();if(!Native.RegisterHotKey(hwnd,HotKeyId,modifiers,key)){if(previous is not null&&TryParse(previous,out var oldModifiers,out var oldKey)&&Native.RegisterHotKey(hwnd,HotKeyId,oldModifiers,oldKey))registered[HotKeyId]=previous;error="Windows rejected this shortcut because it is reserved or already in use."+(previous is null?"":" Your previous shortcut remains active.");return false;}registered[HotKeyId]=shortcut;return true;}
 public void UnregisterAll(){foreach(var id in registered.Keys.ToArray())Native.UnregisterHotKey(hwnd,id);registered.Clear();}
 public void Dispose(){UnregisterAll();if(source is not null)source.RemoveHook(WndProc);source=null;}
 IntPtr WndProc(IntPtr _,int msg,IntPtr w,IntPtr l,ref bool handled){if(msg==Native.WM_HOTKEY&&w.ToInt32()==HotKeyId){CapturePressed?.Invoke(this,EventArgs.Empty);handled=true;}return IntPtr.Zero;}
 public static string Format(Key key,ModifierKeys mods){var parts=new List<string>();if(mods.HasFlag(ModifierKeys.Windows))parts.Add("Win");if(mods.HasFlag(ModifierKeys.Control))parts.Add("Ctrl");if(mods.HasFlag(ModifierKeys.Alt))parts.Add("Alt");if(mods.HasFlag(ModifierKeys.Shift))parts.Add("Shift");parts.Add(key.ToString());return string.Join("+",parts);}
 static bool TryParse(string text,out uint mods,out uint key){mods=0;key=0;var parts=text.Split('+',StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries);if(parts.Length<2)return false;foreach(var p in parts[..^1]){switch(p.ToLowerInvariant()){case "ctrl":case "control":mods|=Native.MOD_CONTROL;break;case "shift":mods|=Native.MOD_SHIFT;break;case "alt":mods|=Native.MOD_ALT;break;case "win":case "windows":mods|=Native.MOD_WIN;break;default:return false;}}if(!Enum.TryParse<Key>(parts[^1],true,out var k)||k is Key.None or Key.LeftCtrl or Key.RightCtrl or Key.LeftShift or Key.RightShift or Key.LeftAlt or Key.RightAlt or Key.LWin or Key.RWin)return false;key=(uint)KeyInterop.VirtualKeyFromKey(k);return true;}
}
