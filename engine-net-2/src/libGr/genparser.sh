cd SequenceParser
java -classpath ../../../csharpcc.jar csharpcc SequenceParser.csc
../../../bin/WarningDisabler.exe SequenceParser.cs 0 CS0162
../../../bin/WarningDisabler.exe SequenceParserTokenManager.cs 0 CS0168 CS0219
../../../bin/WarningDisabler.exe ParseException.cs 0 CS1570
cd ..