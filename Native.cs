using System.Runtime.InteropServices;
namespace FluentSnap;
public static class Native
{
 public const int WM_HOTKEY=0x0312, MOD_ALT=1, MOD_CONTROL=2, MOD_SHIFT=4, MOD_WIN=8, VK_S=0x53;
 public const uint GA_ROOT=2, GW_HWNDNEXT=2; public const int DWMWA_EXTENDED_FRAME_BOUNDS=9;
 [DllImport("user32.dll")] public static extern bool RegisterHotKey(IntPtr h,int id,uint mod,uint key);
 [DllImport("user32.dll")] public static extern bool UnregisterHotKey(IntPtr h,int id);
 [DllImport("user32.dll")] public static extern bool GetCursorPos(out POINT p);
 [DllImport("user32.dll")] public static extern IntPtr WindowFromPoint(POINT p);
 [DllImport("user32.dll")] public static extern IntPtr GetAncestor(IntPtr h,uint flags);
 [DllImport("user32.dll")] public static extern IntPtr GetWindow(IntPtr h,uint cmd);
 [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr h);
 [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr h,out RECT r);
 [DllImport("dwmapi.dll")] public static extern int DwmGetWindowAttribute(IntPtr h,int attribute,out RECT value,int size);
 [StructLayout(LayoutKind.Sequential)] public struct POINT{public int X,Y;}
 [StructLayout(LayoutKind.Sequential)] public struct RECT{public int Left,Top,Right,Bottom;public int Width=>Right-Left;public int Height=>Bottom-Top;public System.Drawing.Rectangle ToRectangle()=>System.Drawing.Rectangle.FromLTRB(Left,Top,Right,Bottom);}
 public static System.Drawing.Rectangle? WindowUnderCursor(IntPtr excluded=default){if(!GetCursorPos(out var p))return null;var h=GetAncestor(WindowFromPoint(p),GA_ROOT);if(h==excluded)h=GetWindow(h,GW_HWNDNEXT);while(h!=IntPtr.Zero&&!IsWindowVisible(h))h=GetWindow(h,GW_HWNDNEXT);if(h==IntPtr.Zero)return null;if(DwmGetWindowAttribute(h,DWMWA_EXTENDED_FRAME_BOUNDS,out var r,Marshal.SizeOf<RECT>())!=0)GetWindowRect(h,out r);return r.Width>0&&r.Height>0?r.ToRectangle():null;}
}
