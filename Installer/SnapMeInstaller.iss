; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "SnapMe"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Formode, Inc."
#define MyAppURL "https://formode.ke/"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{D97412AB-1085-418B-AC0D-84448388722F}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={commonpf}\SnapMe\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=F:\BIMHABITAT\SOFTWARES\SnapMe\Installer
OutputBaseFilename=SnapMeInstaller
SetupIconFile=C:\Users\Symon Kipkemei\Desktop\icons8-snap-71.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\bin\Debug\SnapMe.dll"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2020"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\bin\Debug\SnapMe.dll"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2021"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\bin\Debug\SnapMe.dll"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2022"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\bin\Debug\SnapMe.dll"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2023"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\bin\Debug\SnapMe.dll"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2024"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\bin\Debug\SnapMe.dll"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2025"; Flags: ignoreversion

; Add-in manifest files for various Revit versions
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\SnapMe.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2020"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\SnapMe.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2021"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\SnapMe.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2022"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\SnapMe.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2023"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\SnapMe.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2024"; Flags: ignoreversion
Source: "F:\BIMHABITAT\SOFTWARES\SnapMe\SnapMe.addin"; DestDir: "{commonappdata}\Autodesk\Revit\Addins\2025"; Flags: ignoreversion

; NOTE: Don't use "Flags: ignoreversion" on any shared system files
