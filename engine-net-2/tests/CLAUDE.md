# Backend Semantic Test Creation Guide

A semantic test typically consists of: a `.gm` model file, a `.grg` rules/actions file, one or more `.grs` shell scripts, and `.grs.data` expected output files (generated via `gentest.sh`).

## Semantic Gotchas (from experience)

- **Top-down matching**: Nested and subpatterns are matched top-down -- the enclosing pattern elements are bound first, then nested patterns (negative, independent, iterated, multiple, optional, alternative) and subpatterns are matched within that context. The overall matches resemble a cartesian product of outer and inner matches (yielding occurrs then bottom-up after a complete match was found).
- Variables declared in an enclosing pattern cannot be re-declared in a nested pattern (with the same or a different type). To refine a type in an alternative case, use the special retyping/cast syntax declaring a new element (e.g., `v:Variable<f>` to cast `f` to `Variable`).

## Test Data Format

Tests compare GrShell output against `.grs.data` reference files. Each `exec` command produces 3 data lines: done result (True/False), matches found, rewrites performed. Other commands:
- `eval` produces 1 data line: the expression result
- `show num nodes/edges` produces 1 data line (count)
- `show var` produces 1 data line (variable content)
- `validate` produces 1 data line (result)
- `emit(...)` output is captured directly

## Adding Tests

Use `gentest.sh` to generate `.grs.data` files automatically instead of hand-calculating expected values:
```bash
./gentest.sh subdir/script.grs
```

Test directories can contain **multiple `.grs` files** sharing the same `.gm`/`.grg` definitions (e.g., `alternatives/` has `Alternatives.grs`, `AlternativesRewrite.grs`, `AlternativeInIterated.grs`, etc.). Prefer adding a new `.grs` script to an existing directory when the model/rules already cover the needed types.

## GrGen Language Reference

Per-chapter summaries of the GrGen.NET user manual are in `doc/summaries/`. The chapters relevant for writing backend semantic tests are listed below.

### Model Language
- `modellang.md` — Node/edge types, attributes, enums, connection assertions, inheritance
- `modeladvanced.md` — Object/transient types, methods, packages, graph nesting

### Rules and Computations
- `typexpr.md` — Primitive types, operators, expressions, string methods
- `rulelang.md` — Rules, tests, graphlet syntax, matching semantics, rewrite parts
- `rulelangadvanced.md` — Type constraints, typeof, retyping, copy/clone, merging, redirection
- `computations.md` — Control flow, functions/procedures, local variables, assertions
- `imperative.md` — Embedded exec/emit in rules, deferred exec, scan/tryscan

### Nested Patterns and Subpatterns
- `nested.md` — Negatives, independents, alternatives, iterateds, optionals
- `subpatterns.md` — Declaration, usage, recursive patterns, pattern element locking
- `nestedandsubpatternrewriting.md` — Nested/subpattern rewrite rules and restrictions
- `nestedandsubpatternyielding.md` — def variables, yield, ordered evaluation

### Sequences
- `sequences.md` — Rule application, connectives, loops, result assignment
- `sequencesadvanced.md` — Sequence definitions, transactions, backtracking, for loops
- `sequencesmultirulecalls.md` — Multi-rule calls, match classes, indeterministic choice
- `sequencecomputation.md` — Computation blocks, assignments, built-in functions/procedures
- `sequencegraphquerymanipulation.md` — Array accumulation methods, rule queries

### Data Structures and Features
- `container.md` — set, map, array, deque with methods and operators
- `graph.md` — Graph manipulation, neighbourhood functions, subgraphs, visited flags
- `filters.md` — User-defined, auto-generated, auto-supplied, lambda expression filters
- `indices.md` — Attribute, incidence count, name, uniqueness indices
- `parallelization.md` — Action and sequence parallelization

### Shell and Validation
- `grshell.md` — GrShell commands: graph creation, sequence execution, configuration
- `validation.md` — Show commands, graph validation
