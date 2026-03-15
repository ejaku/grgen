# Tools and Components

### Licensing
- GrGen.NET libraries: LGPL (open source, may be shipped with commercial software)
- yComp: academic/non-commercial license (not for commercial shipping)
- MSAGL: MIT license (use freely)
- SQLite/ADO.NET drivers: public domain

### Runtime Requirements
- Executing: .NET Framework 4.7.2+ or Mono 5.10+
- Compiling/debugging with yComp: additionally Java 1.8+
- Binaries are in the `bin` subdirectory

### GrGen.exe (Compiler Driver)
- Usage: `[mono] GrGen.exe [-keep [<dest-dir>]] [-use <existing-dir>] [-debug] [-b <backend-dll>] [-o <output-dir>] [-r <assembly-path>] [-profile] [-nodebugevents] [-noevents] [-statistics <file>] [-lazynic] [-noinline] <rule-set.grg>`
- `-keep [dir]` -- keep generated C# source files in `tmpgrgenN` or given dir; contains `*Model.cs`, `*Actions.cs`, `*Actions_intermediate.cs` (internal debug), `printOutput.txt`
- `-use <dir>` -- skip C# generation; use existing files in dir to build assemblies
- `-debug` -- compile with debug symbols (also adds validity checking code)
- `-b` -- use alternative backend DLL (default: LGSPBackend)
- `-o` -- output directory for generated assemblies
- `-r` -- reference external assembly in compilation
- `-profile` -- instrument matcher code to count search steps
- `-nodebugevents` -- disable action events (faster embedded execs, but debugger breaks)
- `-noevents` -- disable all events incl. attribute change events (maximum rule performance; breaks transactions/backtracking/record-replay)
- `-statistics <file>` -- optimize matchers for graphs described by statistics file (see GrShell `custom graph`)
- `-lazynic` -- evaluate negatives/independents/conditions only at end of matching (not ASAP)
- `-noinline` -- do not inline subpattern usages and independents
- Invokes `grgen.jar` (Java frontend), then compiles generated C# to DLLs
- Note: `java -jar grgen.jar -i yourfile.grg` dumps compiler IR as .vcg for visualization

### GrShell.exe (Command-Line Shell)
- Usage: `[mono] GrShell.exe [-N] [-SI] [-C "<commands>"] [script.grs ...]`
- Scripts (`.grs`) executed in order; `.grs` suffix may be omitted
- `-N` -- non-interactive mode: exit on error with error code instead of waiting for input
- `-SI` -- Show Includes: print to console when includes are entered/left
- `-C "<cmds>"` -- execute quoted GrShell commands immediately (before first script); use `;;` to separate commands on a single line; embedded `exec` must be terminated with `#§`
- Return codes: 0 (success), -1 (script could not be executed), -2 (`validate exitonfailure` failed)
- Note: `new set` configuration options map to compiler flags; set them before `new graph` commands

### GGrShell.exe (GUI Shell)
- WindowsForms application wrapping GrShell functionality
- Fits better with the MSAGL-based WindowsForms graphical debugger

### Output Assemblies
- `lgsp-*Model.dll` -- graph model (types, attribute storage)
- `lgsp-*Actions.dll` -- compiled rules and sequences
- Generated from `.gm` and `.grg` files by GrGen.exe

### libGrShell.dll
- Shared implementation of GrShell and GGrShell
- Embeddable in own .NET (or native via interop) applications to provide graph rewrite shell scripting

### graphViewerAndSequenceDebugger.dll
- Graph display in yComp (external app via TCP connection) and console-based sequence debugging
- Embeddable in own applications

### graphViewerAndSequenceDebuggerWindowsForms.dll
- Extends graphViewerAndSequenceDebugger.dll with MSAGL-based in-process graph viewer and graphical sequence debugger
- Embeddable in own applications, preferably WindowsForms applications

### lgspBackend.dll
- LibGr SearchPlan backend: implements the libGr API together with generated assemblies
- Supports runtime search plan re-analysis and matcher regeneration (see `custom graph` in GrShell)

### libGr.dll
- Core graph library defining GrGen.NET's API (interfaces, data structures, sequence parsing/interpretation)
- Required in any application using GrGen.NET; API docs at http://www.grgen.de/doc/API/

### libConsoleAndOS.dll
- Console interfaces, basic implementation, and OS abstractions
- Required by libGr; must be included in any application using GrGen.NET

### libConsoleAndOSWindowsForms.dll
- WindowsForms GUI console implementation (used by GGrShell and the WindowsForms debugger)
- Include in own WindowsForms applications using GrGen.NET

### libGrPersistenceProviderSQLite.dll
- SQLite-backed persistent graph storage (ongoing changes written so that they survive crashes)
- Requires: `System.Data.SQLite.dll`, `e_sqlite3.dll` (Windows) or `libe_sqlite3.so` (Linux) -- native x64 SQLite builds

### MSAGL Assemblies
Required for MSAGL graph viewer: `Microsoft.Msagl.dll`, `Microsoft.Msagl.Drawing.dll`, `Microsoft.Msagl.GraphViewerGdi.dll`, `System.Buffers.dll`, `System.Memory.dll`, `System.Numerics.Vectors.dll`, `System.Resources.Extensions.dll`, `System.Runtime.CompilerServices.Unsafe.dll`

### yComp (External Graph Viewer)
- Java-based graph visualization tool; not part of GrGen.NET (academic license)
- Usually launched by GrShell; manual: `java -jar yComp.jar [<graph-file>]`; or use `ycomp`/`ycomp.bat` batch scripts (with increased heap)
- Supports VCG, GML, YGF file formats; several layout algorithms (hierarchic default; try organic or orthogonal)
- Requires Java 1.5+; note: newer JVM versions may cause incompatibilities (use OpenJDK 1.8 if needed)
- Communicates with GrShell via TCP (port 4242+)
