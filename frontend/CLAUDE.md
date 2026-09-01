# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is the compiler frontend of GrGen.NET, implemented in C# (formerly in Java).
It parses `.grg` (rules) and `.gm` (model) files and generates C# code for the backend.

## Build Commands

See BUILDING.md

## Running Tests

See TESTING.md
Note that the compiler tests typically also include the backend part of the compiler.

## Compiler Usage

```bash
mono ../engine-net-2/bin/FrontendGrGen.exe [options] files

# Key options:
-b, --backend=BE     # Backend class (default: SearchPlanBackend2)
-o, --output=DIR     # Output directory
-d, --debug          # Enable debug output
-a, --dump-ast       # Dump AST
-i, --dump-ir        # Dump IR
-t, --timing         # Print timing stats
```

## Package Structure

```
de.unika.ipd.grgen/
├── Main.cs                # Entry point (class Frontend)
├── ast/                   # Abstract Syntax Tree (~450 classes)
│   ├── BaseNode.cs        # Base class for all AST nodes
│   ├── decl/              # Declarations (rules, functions)
│   ├── expr/              # Expressions
│   ├── model/             # Graph model type definitions
│   ├── pattern/           # Pattern definitions
│   ├── stmt/              # Statements
│   ├── type/              # Type system
│   └── util/              # Resolvers and checkers
├── ir/                    # Intermediate Representation (~250 classes)
│   ├── Unit.cs            # Main IR container
│   ├── executable/        # Rule, Function, Procedure IR
│   ├── expr/              # Expressions
│   ├── model/             # Type model IR
│   ├── pattern/           # Pattern IR (Node, Edge)
│   ├── stmt/              # Statements
│   └── type/              # Type system hierarchy
├── be/                    # Backends (code generators)
│   └── Csharp/            # C# backend
│       ├── SearchPlanBackend2.cs  # Main generator
│       ├── ModelGen.cs            # Model code gen
│       └── ActionsGen.cs          # Actions code gen
├── parser/antlr/          # ANTLR parser
│   ├── GrGen.g            # Main grammar (~5000 lines)
│   ├── EmbeddedExec.g     # Embedded sequences grammar (~1,500 lines)
│   └── GRParserEnvironment.cs
└── util/                  # Utilities (dumpers, reporters)
```

## Compiler Pipeline

```
1. ParseInput()     → Parse .grg/.gm files → AST (UnitNode) utilizing ANTLR generated parser
2. ManifestAST()    → Resolve references, check types, check semantic constraints
3. BuildIR()        → Convert AST → IR (Unit)
4. GenerateCode()   → Backend (BE) generates C# code
```

## Grammar Files

- `parser/antlr/GrGen.g` - Main grammar for rules and models
- `parser/antlr/EmbeddedExec.g` - Embedded sequences grammar (sequences, computations, expressions)

After editing grammars, run `./genparser.sh` (Linux) or `genparser.bat` (Windows) to regenerate parser classes.

## Dependencies

- .NET Framework 4.7.2+ (or Mono on Linux)
- ANTLR 3.5 (included in the repository, `antlr-dotnet-csharpbootstrap-3.5.0.2/Antlr3.Runtime.dll` for runtime; `Antlr3.exe` for parser generation)

## Further Reading

- `doc/developing.tex` - Developer guide covering the compiler internals in depth
- `doc/summaries/developing.md` - Concise summary of the developer guide
