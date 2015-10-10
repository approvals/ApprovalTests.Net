rem .nuget\NuGet.exe setapikey e39ea-get-the-full-key-on-nuget.org

call CreateNuget.cmd
.nuget\NuGet.exe push nuget_packages\ApprovalUtilities.3.0.??.nupkg
.nuget\NuGet.exe push nuget_packages\ApprovalUtilities.3.0.??.symbols.nupkg

.nuget\NuGet.exe push nuget_packages\ApprovalTests.3.0.??.nupkg
.nuget\NuGet.exe push nuget_packages\ApprovalTests.3.0.??.symbols.nupkg

pause 