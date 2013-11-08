set approval_test_version=3.0.5
if not exist .\nuget_packages mkdir nuget_packages
del /Q .\nuget_packages\*.*
.nuget\NuGet.exe pack ApprovalUtilities\ApprovalUtilities.CSharp.nuspec -OutputDirectory .\nuget_packages -Version %approval_test_version%
.nuget\NuGet.exe pack ApprovalTests\ApprovalTests.CSharp.nuspec -OutputDirectory .\nuget_packages -Version %approval_test_version%
pause 