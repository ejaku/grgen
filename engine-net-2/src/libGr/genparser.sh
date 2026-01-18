cd SequenceParser
java -classpath ../../../csharpcc.jar csharpcc SequenceParser.csc
mono ../../../bin/WarningDisabler.exe SequenceParser.cs 0 CS0162
mono ../../../bin/WarningDisabler.exe SequenceParserTokenManager.cs 0 CS0168 CS0219
mono ../../../bin/WarningDisabler.exe ParseException.cs 0 CS1570
cd ..