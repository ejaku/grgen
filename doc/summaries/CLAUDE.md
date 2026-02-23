# Chapter Summaries

Per-chapter `.md` summaries of the user manual, one file per `.tex` chapter. These are concise reference cards with key syntax, commands, and concepts. Referenced by test CLAUDE.md files for language guidance.

## Introductory
- `foreword.md`, `introduction.md`, `overview.md`, `quick.md`

## Model Language
- `modellang.md` — Node/edge types, attributes, enums, connection assertions, inheritance
- `modeladvanced.md` — Object/transient types, methods, packages, graph nesting

## Rules and Computations
- `typexpr.md` — Primitive types, operators, expressions, string methods
- `rulelang.md` — Rules, tests, graphlet syntax, matching semantics, rewrite parts
- `rulelangadvanced.md` — Type constraints, typeof, retyping, copy/clone, merging, redirection
- `computations.md` — Control flow, functions/procedures, local variables, assertions
- `imperative.md` — Embedded exec/emit in rules, deferred exec, scan/tryscan

## Nested Patterns and Subpatterns
- `nested.md` — Negatives, independents, alternatives, iterateds, optionals
- `subpatterns.md` — Declaration, usage, recursive patterns, pattern element locking
- `nestedandsubpatternrewriting.md` — Nested/subpattern rewrite rules and restrictions
- `nestedandsubpatternyielding.md` — def variables, yield, ordered evaluation

## Sequences
- `sequences.md` — Rule application, connectives, loops, result assignment
- `sequencesadvanced.md` — Sequence definitions, transactions, backtracking, multi-rule backtracking, for loops, mapping clause, persistence provider transactions, graph nesting
- `sequencesmultirulecalls.md` — Multi-rule calls, match classes, indeterministic choice
- `sequencecomputation.md` — Computation blocks, assignments, built-in functions/procedures
- `sequencegraphquerymanipulation.md` — Array accumulation methods, rule queries

## Data Structures and Features
- `container.md` — set, map, array, deque with methods and operators
- `graph.md` — Graph manipulation, neighbourhood functions, subgraphs, visited flags
- `filters.md` — User-defined, auto-generated, auto-supplied, lambda expression filters
- `indices.md` — Attribute, incidence count, name, uniqueness indices
- `parallelization.md` — Action and sequence parallelization

## Shell and Runtime
- `grshell.md` — GrShell commands: graph creation, sequence execution, configuration
- `validation.md` — Show commands, graph validation
- `debugger.md` — Visual debugger: graph visualization, stepping, breakpoints, watchpoints
- `persistentstorage.md` — SQLite-backed persistent graphs, import/export, record/replay
- `toolsandcomponents.md` — GrGen.exe, GrShell, GGrShell, graph viewers, core libraries

## Reference and Advanced
- `api.md` — C# API: graph/action interfaces, processing environment, events, extensions
- `extensions.md` — External types, functions, procedures, filters, sequences, annotations
- `performance.md` — Search plans, rule design tips, profiling, indices, parallelization
- `techniques.md` — Common patterns: merge/split, traceability, data flow, state space
- `examples.md` — Fractal generation, Busy Beaver Turing machine
- `developing.md` — Build internals, generated code structure, search planning algorithm
- `designgoals.md` — Design philosophy: expressiveness, performance, understandability
