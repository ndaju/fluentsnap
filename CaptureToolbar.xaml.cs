namespace FluentSnap;
public partial class CaptureToolbar : Wpf.Ui.Controls.FluentWindow
{
 readonly Action<CaptureMode> start; public CaptureToolbar(CaptureMode selected,Action<CaptureMode> start){InitializeComponent();this.start=start;ModeBox.SelectedIndex=(int)selected;Loaded+=(_,_)=>{Left=(SystemParameters.WorkArea.Width-Width)/2+SystemParameters.WorkArea.Left;Top=SystemParameters.WorkArea.Top+24;};}
 void Start_Click(object _,RoutedEventArgs e){var mode=(CaptureMode)Math.Clamp(ModeBox.SelectedIndex,0,3);Close();start(mode);}
 void Close_Click(object _,RoutedEventArgs e){Close();}
}
