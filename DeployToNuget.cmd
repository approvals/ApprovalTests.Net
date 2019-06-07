rem nuget_cli\NuGet.exe setapikey e39ea-get-the-full-key-on-nuget.org
rem increment version in Directory.Build.props
rem delete nuget_packages and rebuild
rem choco install nuget.commandline

NuGet push nuget_packages\ApprovalUtilities.3.?.?.nupkg -Source nuget.org
NuGet push nuget_packages\ApprovalTests.3.?.?.nupkg -Source nuget.org

pause 