# GrGen

Command-line compiler driver that orchestrates the GrGen.NET build process.

## Purpose

Main build tool that takes .grg rule specification files (with .gm graph model files) and produces compiled action assemblies (and model assemblies). Invokes the Java frontend (resulting in generated intermediate C# files) and the LGSP backend to generate optimized pattern matching code.

## Output

- `GrGen.exe` - Console executable

## Dependencies

- `libConsoleAndOS` - Console abstraction
- `libGr` - Core graph interfaces
- `lgspBackend` - Pattern matching backend and code generator

## Key Files

| File | Purpose |
|------|---------|
| `GrGen.cs` | Main entry point and command-line argument processing |
| `grgen.ico` | Application icon |
| `app.config` | Application configuration |

## Command-Line Usage

```
GrGen [OPTIONS] <grg-file>[.grg]

Options:
  -o <output-dir>       Output directory for generated assemblies
  -keep [<gen-dir>]     Keep generated files (for debugging)
  -debug                Compile assemblies with debug information
  -r <assembly-path>    Reference external assembly
  -use <existing-dir>   Use existing Java-generated C# files
  -usefull <exist-dir>  Use all existing generated C# files
  -b <backend-dll>      Use specified backend (default: LGSPBackend)
  -statistics <path>    Use graph statistics for matcher optimization
  -profile              Instruments the matcher code to count the search steps carried out
  -lazynic              Defer negative/independent checking
  -noinline             Disable subpattern inlining
  -nodebugevents        Disable debug events (faster, breaks debugging of actions)
  -noevents             Disable all events (faster, breaks also transactions, recording, persistence, graph change display)
```

## Compilation Pipeline

1. **Parse arguments** - Process command-line options
2. **Load backend** - Default is `LGSPBackend.Instance`
3. **Create temp directory** - For intermediate files
4. **Process specification** - Call `backend.ProcessSpecification()`:
   - Java frontend parses .grg file
   - Generates intermediate C# code
   - LGSP backend generates search programs
   - Compiles to .dll assemblies
5. **Cleanup** - Delete temp files unless `-keep` specified

## ProcessSpecFlags

Compilation flags passed to the backend:
- `UseNoExistingFiles` - Generate everything fresh
- `UseJavaGeneratedFiles` - Reuse Java frontend output
- `UseAllGeneratedFiles` - Reuse all intermediate files
- `KeepGeneratedFiles` - Don't delete generated C# files
- `CompileWithDebug` - Include debug symbols
- `NoEvents` / `NoDebugEvents` - Optimization flags
- `LazyNIC` / `Noinline` - Matching behavior flags
- `Profile` - Include profiling instrumentation

## Backend Loading

Custom backends can be loaded via `-b` option:
```csharp
Assembly assembly = Assembly.LoadFrom(backendPath);
// Find type implementing IBackend
IBackend backend = (IBackend)Activator.CreateInstance(backendType);
```

## Exit Codes

- `0` - Success
- `1` - Error (invalid arguments, file not found, compilation error)

## Typical Usage

```bash
# Basic compilation
mono GrGen.exe myRules.grg

# Keep generated files for debugging
mono GrGen.exe -keep -debug myRules.grg

# Use statistics for optimized matching
mono GrGen.exe -statistics graph_stats.txt myRules.grg

# Output to specific directory
mono GrGen.exe -o ./lib myRules.grg
```
