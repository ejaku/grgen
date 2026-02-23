# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is the C# backend of GrGen.NET (`engine-net-2`). It compiles generated code from the Java frontend and executes graph rewriting operations with the (G)GrShell execution hosts (or can be included by its API in an end-user .NET application).

## Build Commands

```bash
# Build entire solution
dotnet build GrGen.sln

# Build specific configuration
dotnet build GrGen.sln -c Release

# Generate parsers first (required if .csc grammar files changed)
./genparsers.sh   # Linux
genparsers.bat    # Windows
```

Parser generation uses CSharpCC to generate C# from `.csc` files in:
- `src/libGr/SequenceParser/`
- `src/libGrShell/`
- `src/graphViewerAndSequenceDebugger/`

## Running Tests

### Semantic Tests (`tests/`)

```bash
# All backend tests
cd tests && ./test.sh

# Specific test directories
cd tests && ./test.sh array1 arith

# With explicit Mono prefix
cd tests && ./test.sh --mono
```

165 test directories containing ~1343 `.grs` test scripts total. Tests compare GrShell output against `.grs.data` reference files. See `tests/CLAUDE.md` for details on test creation and the GrGen language reference.

### NUnit Tests (`unittests/`)

```bash
cd unittests/GraphAndModelTests && dotnet test
```

NUnit 3 tests that exercise the C# backend API directly (graph creation, node/edge manipulation, model loading). The project references `libGr` and `lgspBackend` from `bin/`.

## Package Structure

```
src/
├── libConsoleAndOS/                      # Cross-platform console I/O abstraction (ConsoleUI, IConsoleOutput/Input, HighlightingMode); foundation, no dependencies
├── libConsoleAndOSWindowsForms/          # Windows Forms GUI console controls (GuiConsoleControl) implementing libConsoleAndOS interfaces; used by GGrShell and the Windows Forms/MSAGL debugger
├── libGr/                                # Core graph library: interfaces, sequence interpreter, and runtime helpers
│   ├── Graph/                            # INode, IEdge, IGraph, IGraphModel; NodeType/EdgeType/ObjectType; AttributeType; BaseGraph; GraphValidator; IIndexSet
│   ├── Actions/                          # IAction, IActions, IFilter, IMatchClass; action invocation and filtering interfaces; BaseActions
│   ├── PatternsAndMatches/               # IMatch, IMatchingPattern, IPatternGraph, IPatternElement, IPatternGraphEmbedding, IAlternative, IIterated
│   ├── Sequence/                         # Sequence/SequenceComputation/SequenceExpression AST; sequence interpreter and variable management
│   ├── SequenceParser/                   # Generated CSharpCC parser for sequence syntax (do not edit)
│   ├── GraphProcessingEnvironments/      # IActionExecutionEnvironment → ISubactionAndOutputAdditionEnvironment → IGraphProcessingEnvironment; ITransactionManager, IRecorder
│   └── IO/                              # Graph import/export (GRS, GXL, XMI, ECore) and visualization dump (DOT, VCG)
├── lgspBackend/                          # LGSP (libGr Search Plan) backend: implements libGr interfaces, generates optimized pattern matching code
│   ├── Graph/                            # lgspGraph (type ringlists), lgspGraphElements (ringlist incidence), lgspNamedGraph, lgspObject, lgspGraphModel, lgspGraphStatistics
│   ├── Actions/                          # lgspActions, lgspMatches, lgspPattern, lgspPatternElements (base classes extended by generated code)
│   ├── GraphProcessingEnvironments/      # lgspGraphProcessingEnvironment, lgspTransactionManager, lgspGlobalVariables, lgspDeferredSequencesManager
│   ├── GraphComparison/                  # Full graph isomorphism checking (canonical forms, interpretation plan)
│   ├── MatcherGenerator/                 # PlanGraph construction and scheduling, search plan graph generation, FilterGenerator
│   ├── SearchProgramBuilder/             # Builds nested-loop search program AST from scheduled search plans
│   ├── SearchProgramOperations/          # Search program AST nodes: candidate getting, checking, accepting/abandoning
│   ├── ExpressionOrYielding/             # Expression evaluation and yield/accumulation code generation
│   └── SequenceGenerator/               # Compiled sequence execution code generation
├── libGrShell/                           # GrShell scripting language: CSharpCC-generated parser (GrShell.csc) and interpreter (GrShellImpl) for .grs scripts
├── graphViewerAndSequenceDebugger/       # Debugger engine (Debugger, DebuggerEnvironment) plus textual debugging and graph rendering interface and functionality (graph viewer yComp directly, or MSAGL via dynamically loaded Windows Forms DLL)
├── graphViewerAndSequenceDebuggerWindowsForms/  # Windows Forms/MSAGL debugger GUI (GuiDebuggerHost, GuiConsoleDebuggerHost, MSAGLClient); loaded dynamically when debug with MSAGL is active
├── GrGen/                                # Compiler driver: invokes Java frontend on .grg files, then LGSP backend to generate optimized action assemblies
├── GrShell/                              # interactive or scriptable command-line shell executable: thin wrapper around libGrShell; can be used in bash scripts for testing
├── GGrShell/                             # GUI shell/workbench executable (Windows Forms): same as GrShell but with GUI console (no UI freeze when MSAGL debugger is used)
└── libGrPersistenceProviderSQLite/       # SQLite persistence provider: mirrors graph changes to a database; only loaded when needed (x64/x86 only)
```

The examples-api directory contains API usage examples, the tools directory special-purpose helper tools.

## Runtime

- Target: .NET Framework 4.7.2
- Linux: Run via Mono (`mono bin/GrShell.exe script.grs`)
- Output: All binaries in `bin/`

## Test Structure

Each test directory contains:
- `.gm` - Model definition
- `.grg` - Rules/grammar
- `.grs` - Test script(s) — multiple scripts can share the same `.gm`/`.grg`
- `.grs.data` - Expected output

## Further Reading

- `doc/developing.tex` - Developer guide covering the backend internals in depth
- `doc/summaries/developing.md` - Concise summary of the developer guide
