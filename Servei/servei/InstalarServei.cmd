@echo off
:: Ruta específica de tu ejecutable
set EXE_PATH="C:\serveis\Servei\servei\servei\bin\Debug\servei.exe"

echo Instal·lant el servei ControlServiceDAM...
echo (El servei no s'iniciarà automàticament)

:: Comando de instalación de .NET
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe %EXE_PATH%

echo.
echo Fase d'instal·lació completada. 
echo Ara pots iniciar el servei des del botó del teu formulari.
pause