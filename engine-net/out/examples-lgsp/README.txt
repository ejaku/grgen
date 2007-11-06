This directory includes four examples on how to use the GrGen.NET API.
"Needed files" specifies the files from the GrGen "bin" directory needed
in the folder where the created example binaries reside.


HelloMutex:
 A very small example creating a graph directly from a specification file,
 creating a ring of two nodes, executing a graph rewrite sequence of the
 Mutex-Benchmark, and dumping the resulting graph into a VCG file.
 
 Needed files: libGr.dll, lgspBackend.dll, grgen.jar, jargs.jar, antlr.jar


BusyBeaverExample:
 The execution of the example may take about a minute.
 This example takes advantage of the easy to use graph rewrite sequences.
 The files Turing3Actions.cs and Turing3Model.cs are generated
 from examples/Turing3/Turing3.grg using the "-keep" option of GrGen.exe.

 Needed files: libGr.dll, lgspBackend.dll


MutexDirectExample:
 This example shows three alternatives on how to use the API.
 The files MutexPimpedActions.cs and MutexPimpedModel.cs are generated
 from examples/Mutex/MutexPimped.grg using the "-keep" option of GrGen.exe.

 Needed files: libGr.dll, lgspBackend.dll


YCompExample:
 Shows how to display the current graph in yComp.
 It starts a yComp instance and connects to it via a TCP port to keep
 the graph in yComp up to date.
 
 Needed files: libGr.dll, lgspBackend.dll, grgen.jar, jargs.jar, antlr.jar,
    and ycomp.bat or ycomp.



Instructions for Mono:

To compile the examples with Mono you have to execute something like this
in the appropriate example directory:

gmcs -r:../../bin/libGr.dll -r:../../bin/lgspBackend.dll BusyBeaverExample.cs Turing3Model.cs Turing3Actions.cs

Execute the generated program with

mono BusyBeaverExample.exe
