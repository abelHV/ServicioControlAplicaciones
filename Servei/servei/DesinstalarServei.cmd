@echo off
set EXE_PATH="C:\serveis\Servei\servei\servei\bin\Debug\servei.exe"
echo Aturant i desinstal·lant el servei...
net stop ControlServiceDAM
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u %EXE_PATH%
pause