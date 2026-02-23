# libGrShell

Shell scripting language parser and interpreter for GrGen.NET .grs scripts.

## Purpose

Provides the GrShell scripting language: a CSharpCC-generated parser for .grs script files and interactive input, and an interpreter that executes the parsed commands against the libGr API. Graph manipulation, rule application, and debugging are available directly through the API; libGrShell adds the shell command language and scripting layer on top.
It is used in the GrShell, the GGrShell, but can also be used from API level.

## Output

- `libGrShell.dll` - Library assembly
- `libGrShell.xml` - XML documentation (Release builds)

## Dependencies

- `libConsoleAndOS` - Console abstraction
- `libGr` - Core graph interfaces
- `lgspBackend` - Pattern matching backend
- `graphViewerAndSequenceDebugger` - Debugger support

## Key Files

| File | Size | Purpose |
|------|------|---------|
| `GrShell.csc` | 65KB | Parser grammar (CSharpCC input) |
| `GrShell.cs` | 174KB | Generated parser (do not edit) |
| `GrShellImpl.cs` | 238KB | Shell command interpreter (largest) |
| `GrShellDriver.cs` | 5KB | Parser entry point and version info |
| `GrShellMainHelper.cs` | 12KB | Shell initialization and configuration |
| `GrShellSequenceApplierAndDebugger.cs` | 34KB | Sequence execution with debugging |
| `ShellGraphProcessingEnvironment.cs` | 1.5KB | Shell-specific graph context |
| `FromToReader.cs` | 4KB | File I/O utilities for scripts |

## Generated Files (CSharpCC)

These files are auto-generated from `GrShell.csc` - do not edit manually:
- `GrShell.cs` - Main parser
- `GrShellConstants.cs` - Token constants
- `GrShellTokenManager.cs` - Lexer
- `Token.cs`, `SimpleCharStream.cs`, `ParseException.cs`, `TokenMgrError.cs`

**Regenerate with:** `./genparser.sh` (Linux) or `genparser.bat` (Windows)

## Key Classes

### GrShellDriver

Parser entry point and script execution controller:
- `VersionString` - GrGen version identifier
- Manages a stack of token sources for nested `include` handling (each included file pushes a new `GrShellTokenManager`)
- Manages a stack of `bool` results for `if`/`else`/`endif` conditional execution (`ParsedIf()`, `ParsedElse()`, `ParsedEndif()`)
- `ExecuteCommandLine()` - returns whether the current conditional context allows execution
- Manages error handling and quit/EOF state

### GrShellImpl

Main shell command interpreter (~238KB), implementing `IGrShellImplForDriver` and `IGrShellImplForSequenceApplierAndDebugger`. Organized into regions:
- Variable get/set; graph element/class object lookup by variable or name
- Attribute get/set; container add/remove
- Type lookup and type comparison
- `help` commands (per-command help text)
- External shell execution and file system commands
- `new graph` commands (create graph from spec, with `NewGraphOptions`)
- Graph commands (delete graph, clear, subgraph operations)
- `new` graph element/class object commands (create nodes, edges, objects with attributes)
- Graph element commands (delete, retype, redirect)
- `show` commands (type info, graph/element/object info, graph visualization, actions/backend/var info)
- Sequence and `debug enable/disable` commands
- `dump` commands (graph dump configuration)
- `debug on` event watching configuration
- `debug` layout and mode commands
- Graph I/O commands (import, export, record, replay, save session)
- `validate` commands (connection assertion checking)
- `select` commands (backend, graph, actions selection)
- `custom` commands (backend-specific extensions)
- Shell and environment configuration; compiler configuration (`NewGraphOptions`)

### GrShellMainHelper

Shell initialization:
- `ConstructShell()` - Parse command-line args, produce a `GrShellConfigurationAndControlState` and a `GrShellComponents`
- `ExecuteShell()` - Run the main shell loop using the configuration and components
- `GrShellComponents` - Struct holding the wired-together shell objects: `GrShell` (parser), `GrShellImpl` (interpreter), `IGrShellImplForDriver`, `GrShellDriver`
- `GrShellConfigurationAndControlState` - Struct holding command-line options and runtime control flags

### GrShellSequenceApplierAndDebugger

Sequence execution with debugging support:
- Applies sequences with optional debugging
- Integrates with the `graphViewerAndSequenceDebugger` (breakpoints and stepping are handled there)

## Shell Commands

The grammar in `GrShell.csc` defines commands including:
- `new graph Model "name"` - Create new graph (also a persist with version exists for the persistent graph)
- `new :NodeType($="name", attr=value)` - Create nodes
- `new @("srcname") -:EdgeType-> @("tgtname")` - Create edges
- `select` - Select active backend, graph, actions
- `show` / `dump` - Display graph information
- `validate` - Check connection assertions
- `exec seq` - Execute sequences
- `debug exec seq` - Enter debugger (see Debugger Shell Commands below)
- `include "script.grs"` - Include another script
- `record "file.grs"` / `replay "file.grs"` - Record/replay
- `export "file.grs"` / `import "file.grs"` - Graph I/O
- `quit` / `exit` - Exit shell

## Debugger Shell Commands

The `debug` command enters debugger mode with two primary modes:
- **debug with yComp** (default): graph rendered in external yComp app; sequence output on the shell's main console
- **debug with MSAGL**: Windows Forms/MSAGL debugger; the host graph is always rendered in a separate extra window (outside the main debugger window with consoles); `debug set option` settings apply:
  - `gui true/false` - full GUI debugger window (`true`, default) or naked GUI console based host (`false`)
  - `twopane true/false` - with `gui false`: two GUI consoles for main work object + I/O/logs (`true`, default) or one console for everything (`false`)

## Grammar Modifications

To modify shell syntax:
1. Edit `GrShell.csc` (CSharpCC grammar)
2. Run `./genparser.sh` to regenerate parser
3. Update `GrShellImpl.cs` if semantic actions needed
4. Rebuild the project
