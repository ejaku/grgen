In order to compile GrGen.NET you first have to execute "genparser.bat"
in the subfolders src/libGr and src/GrShell.
This will generate necessary C# files from .csc files.

After compilation, you should execute "editbin.bat".
(This will e.g. increase maximum stack size to allow for deeper pattern matching.)
(But you have to copy (/add to path) editbin.exe (and link.exe required by it) from e.g. the Visual Studio 2017 C++ build tools)