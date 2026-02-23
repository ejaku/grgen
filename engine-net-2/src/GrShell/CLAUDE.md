# GrShell

Command-line shell for GrGen.NET in the form of a console application, usable interactively or by executing .grs script files.

## Purpose

Primary tool for running .grs scripts, executing and debugging rules and sequences, graph manipulation, and testing. A thin wrapper around libGrShell.

## Output

- `GrShell.exe` - Console executable

## Dependencies

- `libConsoleAndOS` - Console abstraction
- `libGr` - Core graph interfaces
- `libGrShell` - Shell parser and interpreter
- `graphViewerAndSequenceDebugger` - Debugger support

## Key Files

| File | Purpose |
|------|---------|
| `GrShellMain.cs` | Entry point - initializes and runs shell |
| `grshell.ico` | Application icon |
| `app.config` | Application configuration |

## Entry Point

The `GrShellMain` class is minimal:
```csharp
static int Main(string[] args)
{
    PrintVersion();
    GrShellMainHelper.ConstructShell(args, out config, out components);
    return GrShellMainHelper.ExecuteShell(config, components);
}
```

All actual functionality is in `libGrShell`.

## Command-Line Usage

```
Usage: GrShell [-C <command>] [-N] [-SI] [<grs-file>]...
```

| Option | Description |
|--------|-------------|
| `-C <command>` | Execute this shell command first (before any script files); use `;;` to combine multiple commands; use `#§` to terminate a contained `exec` |
| `-N` | Non-interactive, non-GUI mode: exit on error instead of waiting for user input |
| `-SI` | Print to console when `include` directives are entered and exited |
| `<grs-file>...` | Script files to execute sequentially (not arguments passed to a script) |

```bash
# Interactive mode
mono GrShell.exe

# Execute script(s)
mono GrShell.exe script.grs
mono GrShell.exe script1.grs script2.grs

# Execute a command, then a script, non-interactively
mono GrShell.exe -N -C "exec myRule();;show graph" script.grs
```

## Features

- **Graph creation/manipulation** - Create and modify graphs
- **Rule execution** - Apply pattern matching rules
- **Sequence execution** - Execute control flow sequences
- **Graph display** - Show and dump graph structure and element information; both yComp and MSAGL graph viewers available (MSAGL causes GUI freeze in GrShell while the viewer window is open)
- **Debugging** - Console-based sequence debugging with breakpoints; visualizes matched patterns and rewrites in the graph; shows online graph changes
- **Testing** - Compare output against expected results (when using the test bash scripts which in turn use GrShell)
- **Recording** - Record and replay operations
- **Import/Export** - Load and save graphs in various formats

## Running on Linux

```bash
mono bin/GrShell.exe script.grs
```

## Running Tests

The shell is used for backend semantic tests:
```bash
cd engine-net-2/tests
./test.sh  # Runs all test scripts
./test.sh dirname  # Run specific test
```

Test scripts compare shell output against `.grs.data` reference files.
