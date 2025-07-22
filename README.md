# Griffith GO - Mobile AR Application for Campus Wayfinding
Unity source directory: Griffith Go/

Unity Editor Version: 2022.3.61f1 LTS
Reason: LTS support + most compatible with AR Foundation => most stable

!! Tip for share !!
Before making .zip file, Exclude Library/, Temp/, Logs/, Builds/ folders. 

=========================
## Project Configurations

[Add Module]
- Microsoft Visual Studio Community 2022
- Android Build Support:
	Android SDK & NDK Tools
	OpenJDK

[Project Creation]
using AR Mobile Core Template

[Package Manager Install/update]
XR Plugin Management 
AR Foundation 
ARCore XR Plugin

[Build Configuration]
Project Settings > XR Plug-in Management > Android > Google ARCore check
Build Settings > Android > Switch Platform
Player Setting > Active Input Handling > Both
Run device > connect device USB and Refresh > choose device name

[C# scripting IDE]
- Visual Studio Code
