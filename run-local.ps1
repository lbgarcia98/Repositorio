$ErrorActionPreference = "Stop"

Set-Location $PSScriptRoot

& "C:\Program Files\dotnet\dotnet.exe" run --no-build --urls "http://localhost:5066"
