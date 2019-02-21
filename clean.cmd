@echo Clean...
@rem x64 OS
@set vswhere="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"

for /f "usebackq tokens=*" %%i in (`call %vswhere% -latest -products * -requires Microsoft.Component.MSBuild -property installationPath`) do (
  set InstallDir=%%i
)
@set MSBUILD="%InstallDir%\MSBuild\15.0\Bin\MSBuild.exe" /tv:15.0 /verbosity:minimal /nologo

@%MSBUILD% build/build.targets.proj /t:Clean %*
@if %ERRORLEVEL% neq 0 goto error

@echo Remove packages...
@rmdir /Q /S src\packages

@echo Remove output...
@rmdir /Q /S output\Release
@rmdir /Q /S output\Debug

@echo Remove bin_obj...
@for /R /D %%i in (*) do @(
	@cd %%i
	if exist bin @rmdir /Q /S bin
	if exist obj @rmdir /Q /S obj		
)

@pause
exit /b 0
:error
@pause
@exit /b 1