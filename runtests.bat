@echo off
echo Starting Tests...
dotnet build -c Debug
dotnet test --no-build -c Debug
rem dotnet build -c Release
rem dotnet test --no-build -c Release
