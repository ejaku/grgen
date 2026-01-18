java -classpath ../../csharpcc.jar csharpcc GrShell.csc
mono ../../bin/WarningDisabler.exe GrShell.cs 0 CS0162 CS0169
mono ../../bin/WarningDisabler.exe GrShellTokenManager.cs 0 CS0168
mono ../../bin/WarningDisabler.exe ParseException.cs 0 CS1570