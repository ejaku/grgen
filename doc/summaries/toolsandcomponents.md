# Tools and Components

### GrGen.exe (Compiler Driver)
- Usage: `GrGen.exe [-keep] [-debug] [-r ASSEMBLY] [-o DIR] [-statistics FILE] SPEC.grg`
- `-keep` -- keep generated C# source files
- `-debug` -- include debug symbols
- `-r` -- reference external assembly
- `-o` -- output directory
- `-statistics FILE` -- use host graph statistics for search plan optimization
- Invokes Java frontend (`grgen.jar`), then compiles generated C# to DLLs

### GrShell.exe (Command-Line Shell)
- Usage: `GrShell.exe [script.grs ...] [-N]`
- `-N` -- no interactive prompt after scripts
- Can pipe scripts: `echo "exec [r]" | GrShell.exe`

### GGrShell.exe (GUI Shell)
- Windows Forms GUI wrapping GrShell functionality

### Output Assemblies
- `lgsp-*Model.dll` -- graph model (types, attribute storage)
- `lgsp-*Actions.dll` -- compiled rules and sequences
- Generated from `.gm` and `.grg` files

### Core Libraries
- `libGr.dll` -- core graph interfaces and data structures
- `lgspBackend.dll` -- search plan backend (pattern matching engine)
- `libGrShell.dll` -- shell command parsing and execution
- `graphViewerAndSequenceDebugger.dll` -- debugger logic
- `libConsoleAndOS.dll` -- platform abstraction

### Graph Viewers
- **yComp** -- external Java tool, communicates via TCP (port 4242+)
- **MSAGL** -- built-in .NET graph layout, requires `graphViewerAndSequenceDebuggerWindowsForms.dll`

### Persistent Storage
- `libGrPersistenceProviderSQLite.dll` + `System.Data.SQLite.dll` -- SQLite-backed graph persistence
