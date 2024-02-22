# DarkFiddler
DarkFiddler is Fiddler classic extension repainting the tool with dark colors. That's useful for users who prefers dark themes.

### Open the source project
The project references Fiddler.exe itself and it's expected to be already installed on the development computer.
1) Project reference - C:\Program Files\Fiddler\Fiddler.exe
2) Start external program - C:\Program Files\Fiddler\Fiddler.exe
3) Post-build event copies the output assembly to user's profile - %USERPROFILE%\Documents\Fiddler2\Scripts

Tune the project setting based on your locations.

### Installation
Build the code. Ensure "DarkFiddler.dll" assembly was copied to %USERPROFILE%\Documents\Fiddler2\Scripts directory - it should happen automaticaly during build.

### Supported Fiddler versions
Tested with Fiddler classic v5.0.20204.45441

### Changelog
- 2024-02-22 - the first version
