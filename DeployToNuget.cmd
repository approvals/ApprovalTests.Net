CreateNuget.cmd
.nuget\NuGet.exe push nuget_packages\ApprovalTests.3.0.?.nupkg
.nuget\NuGet.exe push nuget_packages\ApprovalTests.3.0.?.symbols.nupkg

.nuget\NuGet.exe push nuget_packages\ApprovalUtilities.3.0.?.nupkg
.nuget\NuGet.exe push nuget_packages\ApprovalUtilities.3.0.?.symbols.nupkg
pause 