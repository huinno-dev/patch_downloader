@echo off

set mypath=%~dp0

if defined ProgramFiles(x86) (
    @echo 64-bit Architecture
	pnputil.exe -i -a %mypath%\x64\xrusbser.inf
) else (
    @echo 32-bit Architecture
	pnputil.exe -i -a %mypath%\x86\xrusbser.inf
)

rem timeout /t 5



