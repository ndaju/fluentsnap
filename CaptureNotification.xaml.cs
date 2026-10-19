namespace FluentSnap;
public partial class CaptureNotification : Window
{
 readonly Action open;
 public CaptureNotification(Action open,string detail="Click to open it in FluentSnap"){InitializeComponent();this.open=open;DetailText.Text=detail;Loaded+=(_,_)=>{Left=SystemParameters.WorkArea.Right-Width-18;Top=SystemParameters.WorkArea.Bottom-Height-18;};}
 void Open_Click(object _,RoutedEventArgs e){Close();open();}
}
