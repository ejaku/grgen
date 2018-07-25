rem for the following to work, you have to copy (/add to path) editbin.exe (and link.exe required by it) from e.g. the Visual Studio 2017 C++ build tools

rem support deeply nested structures, in pattern matching (GrShell for executing matchers), or in sequence compilation (GrGen), by increasing the max stack size available for the recursive calls
editbin /stack:16777216,16760832 bin\GrShell.exe
editbin /stack:16777216,16760832 bin\GrGen.exe

rem create a 32bit version being able to support 3GB instead of just 2GB if main memory (somwhat obsolete now that 64bit rules)
copy bin\GrShell.exe bin\GrShell3GB.exe
editbin /LARGEADDRESSAWARE bin\GrShell3GB.exe
