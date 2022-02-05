java -classpath ..\..\csharpcc.jar csharpcc GrShell.csc
@if ERRORLEVEL 1 PAUSE
..\..\bin\WarningDisabler.exe GrShell.cs 0 CS0162 CS0169
..\..\bin\WarningDisabler.exe GrShellTokenManager.cs 0 CS0168
..\..\bin\WarningDisabler.exe ParseException.cs 0 CS1570