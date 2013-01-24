@echo off

SET version=0
FOR /F "tokens=1" %%a IN ('git rev-list master') DO SET /A version+=1
FOR /F "tokens=*" %%a IN ('git rev-parse HEAD') DO SET commit=%%a

echo [assembly: System.Reflection.AssemblyVersion("1.0.%version%")] > src\AssemblyVersion.cs
echo [assembly: System.Reflection.AssemblyDescription("Compiled from %commit%")] >> src\AssemblyVersion.cs