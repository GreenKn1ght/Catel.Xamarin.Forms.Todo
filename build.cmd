@set vswhere="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"

for /f "usebackq tokens=*" %%i in (`call %vswhere% -latest -products * -requires Microsoft.Component.MSBuild -property installationPath`) do (
  set InstallDir=%%i
)
@set MSBUILD="%InstallDir%\MSBuild\15.0\Bin\MSBuild.exe" /tv:15.0 /verbosity:minimal /nologo

@%MSBUILD% build/build.targets.proj /t:BuildRelease %*
@if %ERRORLEVEL% neq 0 goto error

@pause
exit /b 0
:error
@pause
@exit /b 1
