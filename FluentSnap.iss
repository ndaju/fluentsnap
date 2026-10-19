#define MyAppName "FluentSnap"
#define MyAppVersion "1.0.1"
#define MyAppPublisher "FluentSnap"
#define MyAppExeName "FluentSnap.exe"

[Setup]
AppId={{B4E1D09A-8E68-4BA3-9BD1-FLUENTSNAP01}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\FluentSnap
DefaultGroupName=FluentSnap
OutputDir=installer
OutputBaseFilename=FluentSnap-Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
SetupIconFile=logo.ico
UninstallDisplayIcon={app}\{#MyAppExeName}
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=lowest

[Files]
Source: "publish\*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion

[Icons]
Name: "{group}\FluentSnap"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\FluentSnap"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; GroupDescription: "Additional shortcuts:"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch FluentSnap"; Flags: nowait postinstall skipifsilent
