using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.Windows.Input;
namespace FluentSnap;
public partial class SettingsWindow : Wpf.Ui.Controls.FluentWindow
{
 readonly UserSettings original; UserSettings editing;
 public UserSettings? Result{get;private set;}
 public SettingsWindow(UserSettings settings){InitializeComponent();Owner=System.Windows.Application.Current.MainWindow;original=settings;editing=Clone(settings);DataContext=editing;LoadValues();}
 static UserSettings Clone(UserSettings s)=>new(){CaptureShortcut=s.CaptureShortcut,DefaultMode=s.DefaultMode,OpenToolbarOnShortcut=s.OpenToolbarOnShortcut,RememberLastMode=s.RememberLastMode,ShowTooltips=s.ShowTooltips,RestoreAfterCapture=s.RestoreAfterCapture,StartMinimized=s.StartMinimized,CaptureFolder=s.CaptureFolder,LastMode=s.LastMode};
 void LoadValues(){ShortcutBox.Text=editing.CaptureShortcut;}
 void ShortcutBox_KeyDown(object _,KeyEventArgs e){e.Handled=true;var key=e.Key==Key.System?e.SystemKey:e.Key;if(key is Key.LeftCtrl or Key.RightCtrl or Key.LeftShift or Key.RightShift or Key.LeftAlt or Key.RightAlt or Key.LWin or Key.RWin)return;if(Keyboard.Modifiers==ModifierKeys.None){ErrorText.Text="Add Ctrl, Alt, Shift, or Win to the shortcut.";return;}ShortcutBox.Text=ShortcutService.Format(key,Keyboard.Modifiers);ErrorText.Text="";}
 void BrowseFolder_Click(object _,RoutedEventArgs e){using var dialog=new System.Windows.Forms.FolderBrowserDialog{SelectedPath=editing.CaptureFolder,Description="Choose where FluentSnap saves captures"};if(dialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)editing.CaptureFolder=dialog.SelectedPath;}
 void Save_Click(object _,RoutedEventArgs e){editing.CaptureShortcut=ShortcutBox.Text;Result=editing;DialogResult=true;}
 void Cancel_Click(object _,RoutedEventArgs e){Result=null;DialogResult=false;}
 void Reset_Click(object _,RoutedEventArgs e){if(System.Windows.MessageBox.Show("Reset all FluentSnap preferences to defaults?","Reset settings",MessageBoxButton.YesNo,MessageBoxImage.Question)!=MessageBoxResult.Yes)return;editing=SettingsStore.Defaults();DataContext=editing;LoadValues();}
}
