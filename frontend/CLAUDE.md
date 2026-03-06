# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is the Java frontend compiler for GrGen.NET.
It parses `.grg` (rules) and `.gm` (model) files and generates C# code for the backend.

## Build Commands

See BUILDING.md

## Running Tests

See TESTING.md
Note that the compiler tests typically also include the backend part of the compiler.

## Compiler Usage

```bash
java -cp jars/jargs.jar:jars/antlr-runtime-3.4.jar:../engine-net-2/bin/grgen.jar \
    de.unika.ipd.grgen.Main [options] files

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
├── Main.java              # Entry point
├── ast/                   # Abstract Syntax Tree (~450 classes)
│   ├── BaseNode.java      # Base class for all AST nodes
│   ├── decl/              # Declarations (rules, functions)
│   ├── expr/              # Expressions
│   ├── model/             # Graph model type definitions
│   ├── pattern/           # Pattern definitions
│   ├── stmt/              # Statements
│   ├── type/              # Type system
│   └── util/              # Resolvers and checkers
├── ir/                    # Intermediate Representation (~250 classes)
│   ├── Unit.java          # Main IR container
│   ├── executable/        # Rule, Function, Procedure IR
│   ├── expr/              # Expressions
│   ├── model/             # Type model IR
│   ├── pattern/           # Pattern IR (Node, Edge)
│   ├── stmt/              # Statements
│   └── type/              # Type system hierarchy
├── be/                    # Backends (code generators)
│   └── Csharp/            # C# backend
│       ├── SearchPlanBackend2.java  # Main generator
│       ├── ModelGen.java            # Model code gen
│       └── ActionsGen.java          # Actions code gen
├── parser/antlr/          # ANTLR parser
│   ├── GrGen.g            # Main grammar (4,960 lines)
│   ├── EmbeddedExec.g     # Embedded sequences grammar (~1,486 lines)
│   └── GRParserEnvironment.java
└── util/                  # Utilities (dumpers, reporters)
```

## Compiler Pipeline

```
1. parseInput()     → Parse .grg/.gm files → AST (UnitNode) utilizing ANTLR generated parser
2. manifestAST()    → Resolve references, check types, check semantic constraints
3. buildIR()        → Convert AST → IR (Unit)
4. generateCode()   → Backend (BE) generates C# code
```

## Grammar Files

- `parser/antlr/GrGen.g` - Main grammar for rules and models
- `parser/antlr/EmbeddedExec.g` - Embedded sequences grammar (sequences, computations, expressions)

After editing grammars, run `make .grammar` to regenerate parser classes.

## Dependencies

- Java 1.8+
- ANTLR 3.4 (`jars/antlr-3.4-complete.jar` for build, `antlr-runtime-3.4.jar` for runtime)
- jargs (`jars/jargs.jar` for command-line arguments parsing)

## Further Reading

- `doc/developing.tex` - Developer guide covering the compiler internals in depth
- `doc/summaries/developing.md` - Concise summary of the developer guide
