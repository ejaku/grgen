# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

The CLAUDE.md summaries/instructions for onboarding AI agents/coding bots should be also useful summaries/overviews for human developers.
In case of AI coding take care of the content of AICodingPolicy.txt (the restriction to limited parts of the project and the mandatory human review).

## Project Overview

GrGen.NET (graph rewrite generator) is a system software for graph rewriting comprising a compiler at its core, an interactive and scriptable shell interpreter, a debugger offering visual-graphical and step-wise debugging, and an embedded database/ORM-mapper.
It is a tool for transforming graph-structured data using declarative pattern matching and rewrite rules, enriched by advanced data modelling, query-result processing, and imperative and object-oriented programming language constructs.
Its features can also by used via an API in end-user .NET-applications.
Typical uses are in compiler construction, model transformation, computer linguistics/knowledge representation, and engineering domains (in which graph representations of data are natural/intuitive).

**Two main components:**
- **Frontend** (`frontend/`): Java compiler using ANTLR 3.4 that parses `.grg` (rules) and `.gm` (model) files, generates C# code
- **Backend** (`engine-net-2/`): C# engine that compiles the generated code into assemblies and executes graph rewriting operations with the GrShell application

## Technologies

- **C#** (.NET Framework 4.7.2) - Backend engine, shell, debugger
- **Java** (1.8+) - Frontend compiler
- **CSharpCC** - Parser generator for C# (sequence/shell/constant parsers)
- **ANTLR 3.4** - Parser generator for Java frontend
- **SQLite** - Persistent graph storage (`libGrPersistenceProviderSQLite`)
- **MSAGL** - Graph visualization in debugger (.NET)
- **yComp** - External JAVA graph viewer application
- **Mono** - Cross-platform runtime (Linux)

## Build Commands

### Full Build (Linux)
```bash
./make_linux.sh
```

### Full Build (Windows/Cygwin)
```bash
./make_cygwin_windows.sh
```

### Build Components Separately

**Frontend only** (produces `grgen.jar`):
```bash
cd frontend && make
```

**Backend only** (after frontend is built):
```bash
cd engine-net-2 && dotnet build GrGen.sln
```

**Generate parsers only** (CSharpCC-based, required before backend build if parser grammars changed):
```bash
cd engine-net-2 && ./genparsers.sh
```

## Running Tests

### Backend Semantic Tests
```bash
cd engine-net-2/tests && ./test.sh
```
Run specific test directory:
```bash
cd engine-net-2/tests && ./test.sh dirname
```

### Frontend Compiler Tests
```bash
cd frontend/test && ./test.sh
```
Run specific test file:
```bash
cd frontend/test && ./test.sh should_pass/mytest.grg
```

### Frontend Acceptance Tests (JUnit)
```bash
cd frontend && ./unittest/make_unittest.sh
```

### Backend Unit Tests (NUnit)
```bash
cd engine-net-2/unittests/GraphAndModelTests && dotnet test
```

### Examples (smoke tests)
```bash
cd engine-net-2/examples && ./test.sh
```

## Architecture

### Directory Structure

```
grgen/
├── frontend/           # Java compiler
│   ├── de/             # Java sources
│   └── test/           # Compiler tests (should_pass/, should_fail/, should_warn/)
├── engine-net-2/       # C# compiler backend and runtime libraries, as well as Graph Rewrite Shells including debugging and graph visualization
│   ├── src/            # Source projects
│   ├── bin/            # Build output (and released binaries)
│   ├── tests/          # Semantic tests
│   └── examples/       # Example scripts
└── ...                 # See CLAUDE-AUX.md for further non-code directories
```

### Frontend Compiler Pipeline

```
1. parseInput()     → Parse .grg/.gm files via ANTLR → AST (UnitNode)
2. manifestAST()    → Resolve references, check types, validate semantics
3. buildIR()        → Convert AST → IR (Unit)
4. generateCode()   → Backend generates C# source code
```

### C# Projects (engine-net-2/src/)

- **libConsoleAndOS**: Foundation library - cross-platform console I/O abstraction; no dependencies
- **libConsoleAndOSWindowsForms**: Windows Forms GUI console controls implementing libConsoleAndOS interfaces
- **libGr**: Core graph library - graph and pattern matching interfaces, sequence parsing
- **lgspBackend**: Search plan backend - generates and executes search programs for pattern matching (also graph implementation)
- **libGrShell**: Shell scripting library - parses and executes `.grs` shell scripts
- **libGrPersistenceProviderSQLite**: SQLite-based persistent graph storage
- **GrGen**: Compiler driver - invokes Java frontend, compiles generated C# code (main implementation in lgspBackend)
- **GrShell**: Command-line shell for interactive graph manipulation
- **GGrShell**: GUI shell/workbench with Windows Forms
- **graphViewerAndSequenceDebugger**: sequence execution debugger (text-console) with graph visualization functionality
- **graphViewerAndSequenceDebuggerWindowsForms**: GUI/visual debugger and MSAGL-graph-viewer

### Key Design Patterns

- **Scheduled search programs**: Generated nested loops created by scheduling for efficient pattern matching
- **Scalable data structures**: Type and incidence ringlists for fast graph element lookup, traversal, and structure changes; AA-trees for attribute indices
- **Graph change events**: Event driven design/event sourcing: change events from application logic used for persistence and visualization
- **Formal language processing**: Recursive descent parsing, nested symbol tables, partly syntax directed interpretation/translation

### File Types

- `.gm` - Graph model files (node/edge type definitions)
- `.grg` - Graph rewrite rules (patterns and transformations)
- `.grs` - GrShell scripts (test/execution scripts)
- `.grs.data` - Expected output for test scripts

## Runtime Requirements

- .NET Framework 4.7.2+ or Mono
- On Linux, executables run via `mono`: `mono bin/GrShell.exe script.grs`

## Language and Tool Reference

`doc/summaries/` contains per-chapter `.md` summaries of the user manual — concise reference cards for the GrGen languages (model, rules, sequences, computations), tools (shell, debugger, API), and techniques. See `doc/summaries/CLAUDE.md` for a categorized index. See `doc/CLAUDE.md` for the manual's document structure, and `CLAUDE-AUX.md` for an overview of other project documentation files.

For details on creating tests, see the CLAUDE.md files in `engine-net-2/tests/` (backend semantic tests) and `frontend/test/` (frontend compiler tests).
