using System.Text.Json;
using System.Text.Json.Serialization;
namespace FluentSnap;
public sealed class UserSettings
{
 [JsonIgnore] public CaptureMode[] CaptureModes { get; } = Enum.GetValues<CaptureMode>();
 public string CaptureShortcut {get;set;}="Ctrl+Shift+S";
 public CaptureMode DefaultMode {get;set;}=CaptureMode.Rectangle;
 public bool OpenToolbarOnShortcut {get;set;}=true;
 public bool RememberLastMode {get;set;}=true;
 public bool ShowTooltips {get;set;}=true;
 public bool RestoreAfterCapture {get;set;}=true;
 public bool StartMinimized {get;set;}=false;
 public string CaptureFolder {get;set;}=System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),"Screenshoots");
 public string? LastMode {get;set;}
}
public static class SettingsStore
{
 public static string Folder=>System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"FluentSnap");
 public static string FilePath=>System.IO.Path.Combine(Folder,"settings.json");
 public static UserSettings Load(){try{if(!File.Exists(FilePath))return new();var s=JsonSerializer.Deserialize<UserSettings>(File.ReadAllText(FilePath))??new();if(string.IsNullOrWhiteSpace(s.CaptureFolder))s.CaptureFolder=System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),"Screenshoots");if(!Enum.IsDefined(s.DefaultMode))s.DefaultMode=CaptureMode.Rectangle;if(!string.IsNullOrWhiteSpace(s.LastMode)&&Enum.TryParse<CaptureMode>(s.LastMode,true,out var m))s.DefaultMode=m;else s.LastMode=null;return s;}catch{return new();}}
 public static bool Save(UserSettings settings,out string? error){error=null;try{Directory.CreateDirectory(Folder);var tmp=FilePath+".tmp";File.WriteAllText(tmp,JsonSerializer.Serialize(settings,new JsonSerializerOptions{WriteIndented=true}));File.Move(tmp,FilePath,true);return true;}catch(Exception ex){error=ex.Message;return false;}}
 public static UserSettings Defaults()=>new();
}
