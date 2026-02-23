# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is the Java frontend compiler for GrGen.NET. It parses `.grg` (rules) and `.gm` (model) files and generates C# code for the backend.

## Build Commands

```bash
# Build compiler (produces grgen.jar)
make

# Fast incremental build
make fast

# Regenerate ANTLR parser (after grammar changes)
make .grammar

# Clean build artifacts
make clean
```

Output JAR: `../engine-net-2/bin/grgen.jar`

## Running Tests

### Compiler Tests (`test/`)

```bash
# All tests
./test/test.sh

# Frontend only (skip C# compilation, faster)
./test/test.sh -f

# Specific test file
./test/test.sh should_pass/mytest.grg

# Verbose output
./test/test.sh -v

# Clean test outputs
./test/test.sh -c
```

Test categories:
- `test/should_pass/` - Must compile successfully (721 files)
- `test/should_fail/` - Must fail with errors (1185 files)
- `test/should_warn/` - Must compile with warnings (52 files)

### JUnit Acceptance Tests (`unittest/`)

```bash
# Build frontend and run acceptance tests
./unittest/make_unittest.sh
```

Quick-running JUnit 4 tests that exercise the full compiler pipeline (parse, AST, IR, code generation) on small `.grg`/`.gm` input files. The test files live in `unittest/` alongside `AcceptanceTest.java`.

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
1. parseInput()     → Parse .grg/.gm files → AST (UnitNode)
2. manifestAST()    → Resolve references, validate
3. buildIR()        → Convert AST → IR (Unit)
4. generateCode()   → Backend generates C# code
```

## Grammar Files

- `parser/antlr/GrGen.g` - Main grammar for rules and models
- `parser/antlr/EmbeddedExec.g` - Expression/statement syntax

After editing grammars, run `make .grammar` to regenerate parser classes.

## Dependencies

- Java 1.8+
- ANTLR 3.4 (`jars/antlr-3.4-complete.jar` for build, `antlr-runtime-3.4.jar` for runtime)
- jargs (`jars/jargs.jar` for command-line parsing)

## Further Reading

- `doc/developing.tex` - Developer guide covering the compiler internals in depth
- `doc/summaries/developing.md` - Concise summary of the developer guide
