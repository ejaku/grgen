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
- **Backend** (`engine-net-2/`): C# engine that compiles the generated code into assemblies and executes graph rewriting operations with the GrShell application (that interprets `.grs` graph rewrite script files)

## Build Commands

### Full Build (Linux)
```bash
./make_linux.sh
```

### Full Build (Windows/Cygwin)
```bash
./make_cygwin_windows.sh
```

For more, see frontend/BUILDING.md and engine-net-2/BUILDING.md

## Running Tests

### Full Semantic Tests (in engine-net-2 directory)
```bash
cd engine-net-2/tests && ./test.sh
```

### Full Compiler Tests (in frontend directory)
```bash
cd frontend/test && ./test.sh
```

### Examples (smoke tests)
```bash
cd engine-net-2/examples && ./test.sh
```

For further test instructions, see frontend/TESTING.md and engine-net-2/TESTING.md

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

Classical compiler pipeline with a parser creating an abstract syntax tree, reference resolving and type checking on the syntax tree, that is then lowered to an intermediate representation, and a code generator (producing C#).
For more see the CLAUDE.md in frontend.

### C# Application Projects

- **GrGen**: Compiler driver - invokes Java frontend, extends and compiles generated C# code (main implementation in lgspBackend)
- **GrShell**: Command-line shell for interactive graph manipulation and scripting (real implementation in libGrShell)
- **GGrShell**: GUI shell/workbench with Windows Forms

### C# Library Projects

- **libGr**: Core graph library - graph interfaces, pattern matching interfaces, sequence parsing and interpretation
- **lgspBackend**: Search plan backend - generates search programs for pattern matching, also graph implementation and sequence code generation
- **graphViewerAndSequenceDebugger**: sequence execution debugger (text-console) with graph visualization functionality
- **graphViewerAndSequenceDebuggerWindowsForms**: GUI/visual debugger and MSAGL-graph-viewer

Further projects contain cross-platform consoles, a library with the GrShell main functionality, and the SQLite-based persistent graph storage.
These libraries may be all included in end-user applications (used on API level).
For more see the CLAUDE.md in engine-net-2.

### Key Design Pattern in Frontend and Backend

- **Formal language processing**: Generated recursive-descent (potentially backtracking) parsers, nested symbol tables and type checking, syntax directed interpretation/translation

## File Types

- `.gm` - Graph model files (node/edge type definitions)
- `.grg` - Graph rewrite rules (patterns and transformations)
- `.grs` - GrShell scripts with sequence interpretation (also used as test scripts) (a reduced version is also used as graph import/export format)
- `.grs.data` - Expected output for test scripts

## Runtime Requirements

- .NET Framework 4.7.2+ or Mono
- On Linux, executables run via `mono`: `mono bin/GrShell.exe script.grs`

## Documentation (esp. Language and Tool Reference)

See `CLAUDE-AUX.md` for an overview of the project documentation files (and used technologies).
This includes an index of the CLAUDE.md files contained in the folder structure of the project, covering the user manual, the frontend pipeline steps, and the engine-net-2 projects (normally to be included bottom-up as needed).
