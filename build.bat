@ECHO OFF

call version.bat
%windir%\Microsoft.Net\Framework\v4.0.30319\msbuild src\GitAutoCommit.sln  /property:Configuration=Release /property:Platform="Any CPU"

if ERRORLEVEL 1 pause

copy /y license.txt build\license.txt