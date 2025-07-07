#!/bin/bash
set -euo pipefail

export DOTNET_NOLOGO=1
export DOTNET_CLI_TELEMETRY_OPTOUT=true
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

dotnet build src
dotnet test src --no-build --no-restore