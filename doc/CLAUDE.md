# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This directory contains the LaTeX source for the GrGen.NET User Manual (`grgen.pdf`, ~7MB).

## Build Commands

```bash
# Build the PDF (runs pdflatex multiple times, bibtex, makeindex, rail diagrams)
./build grgen

# Build draft version (faster, shows overfull/underfull boxes)
./build --draft grgen
```

Requirements: pdflatex, bibtex, makeindex, thumbpdf, and the `rail` tool (included as `./rail`)

## Document Structure

Main file: `grgen.tex`

### Introductory Chapters
- `foreword.tex` - Foreword, version history highlights, acknowledgments
- `introduction.tex` - Introduction to graph rewriting concepts and theory
- `overview.tex` - System architecture, components, and workflow overview
- `quick.tex` - Quick start tutorial with a complete example

### Core Language Reference
- `modellang.tex` - Graph model language: node types, edge types, attributes, inheritance
- `rulelang.tex` - Rule language basics: patterns, LHS/RHS, modifications, matching semantics
- `typexpr.tex` - Primitive types, operators, expressions, type checks and casts, string methods
- `rulelangadvanced.tex` - Advanced rules: type constraints, typeof, retyping, copy/clone, merging, redirection, homomorphic matching, induced subgraphs, exact patterns

### Pattern Matching
- `nested.tex` - Nested and alternative patterns: negatives, independents, alternatives, iterateds, optionals
- `subpatterns.tex` - Reusable subpatterns and pattern composition
- `nestedandsubpatternrewriting.tex` - Rewriting with nested patterns and subpatterns
- `nestedandsubpatternyielding.tex` - Yielding values from nested patterns

### Control Flow (Sequences)
- `sequences.tex` - Graph rewrite sequences: rule chaining, loops, conditionals
- `sequencecomputation.tex` - Computations in sequences: variables, expressions, assignments
- `sequencegraphquerymanipulation.tex` - Graph queries and manipulation in sequences
- `sequencesmultirulecalls.tex` - Multi-rule calls and backtracking
- `sequencesadvanced.tex` - Advanced sequences: transactions, parallelism, pause/resume

### Imperative Features
- `imperative.tex` - Imperative statements in rules: eval blocks, exec statements
- `computations.tex` - Computation statements: if/else, loops, switches, procedures

### Data Structures
- `container.tex` - Container types: arrays, maps, sets, deques with operations
- `graph.tex` - Graph operations: adjacency, reachability, induced subgraphs

### Advanced Features
- `filters.tex` - Match filters: ordering, selecting, auto-generated and user-defined filters
- `modeladvanced.tex` - Advanced modeling: external types, function methods, copy/compare
- `indices.tex` - Attribute indices and incidence indices for fast lookup
- `parallelization.tex` - Parallel and concurrent graph processing

### Shell and Runtime Environment
- `grshell.tex` - GrShell command reference: graph creation, rule execution, I/O, scripting
- `debugger.tex` - Visual debugger: breakpoints, stepping, match highlighting, graph visualization
- `validation.tex` - Graph validation: connection assertions, type constraints
- `persistentstorage.tex` - SQLite-based persistent graph storage
- `toolsandcomponents.tex` - Overview of GrGen, GrShell, GGrShell, yComp, MSAGL components

### Reference and Advanced Topics
- `performance.tex` - Performance tuning: search plans, profiling, optimization tips
- `examples.tex` - Detailed example walkthroughs
- `api.tex` - C# API reference for embedding GrGen in applications
- `extensions.tex` - External attribute evaluation and procedural extensions
- `developing.tex` - Understanding and extending GrGen internals (for contributors)
- `designgoals.tex` - Design philosophy and goals behind GrGen
- `techniques.tex` - Common techniques and design patterns for graph rewriting

### Supporting Files
- `diss.bib` - Bibliography (BibTeX)
- `packages.tex` - LaTeX package imports
- `fig/` - Figures and diagrams
- `bench/` - Benchmark data for performance chapter
- `rail.sty`, `rail` - Railroad diagram support for syntax diagrams

## Code Listing Environments

The manual defines custom LaTeX environments for code:
- `grgen` / `grgenlet` - GrGen rule language
- `grshell` / `grshelllet` - GrShell commands
- `csharp` / `csharplet` - C# code
- `bash` - Shell commands

## Chapter Summaries

`summaries/` contains concise `.md` reference cards, one per `.tex` chapter. See `summaries/CLAUDE.md` for a categorized index.
