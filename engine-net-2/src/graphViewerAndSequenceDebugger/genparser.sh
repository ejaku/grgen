java -classpath ../../csharpcc.jar csharpcc ConstantParser.csc
mono ../../bin/WarningDisabler.exe ConstantParser.cs 0 CS0162 CS0169
mono ../../bin/WarningDisabler.exe ConstantParserTokenManager.cs 0 CS0168
mono ../../bin/WarningDisabler.exe ParseException.cs 0 CS1570