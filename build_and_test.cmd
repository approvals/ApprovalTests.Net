dotnet restore --configuration Debug src
dotnet build   --configuration Debug src\ApprovalUtilities\ApprovalUtilities.csproj
dotnet build   --configuration Debug src\ApprovalTests\ApprovalTests.csproj
dotnet build   --configuration Debug src
dotnet test    --configuration Debug src
