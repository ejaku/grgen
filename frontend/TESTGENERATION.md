# Frontend Compiler Test Creation Guide

The `test/` directory contains tests for the GrGen.NET Java frontend compiler. Each test is a `.grg` file (optionally with a companion `.gm` model file) that is compiled by the frontend. Tests are organized by expected compiler outcome.

## Test Categories

- `test/should_pass/` — Valid GrGen code that must compile without errors or warnings
- `test/should_fail/` — Invalid code that must produce a compiler ERROR (and exit non-zero)
- `test/should_warn/` — Valid code that must produce a compiler WARNING (but still compile successfully)

## Test Structure

A test is a single `.grg` file. If it needs custom types, provide a `.gm` model file with the same base name in the same directory and reference it with `#using`. Tests that only use built-in types (`Node`, `Edge`, `UEdge`, `AEdge`) need no `.gm` file.

Example `test/should_pass/` test with model:
```
# mytest.gm
node class A { x : int; }
edge class R;

# mytest.grg
#using "mytest.gm"
rule r {
    a:A -e:R-> b:A;
    modify {
        c:A;
        a -:R-> c;
    }
}
```

Example `test/should_fail/` test (no model needed):
```
# annot_001.grg
rule r {
    [prio=42];    # invalid annotation triggers ERROR
    modify {}
}
```

Example `test/should_warn/` test:
```
# basic_003.grg
rule r {
    if { true; }  # constant condition triggers WARNING
    replace {}
}
```

## Adding a New Test

1. Create the `.grg` file (and `.gm` if needed) in the appropriate `test/should_*/` directory
2. Run the test to verify the expected outcome: `cd test && ./test.sh should_pass/mytest.grg`
3. Add the expected result line to `test/summary_gold.log` in alphabetical order within its section (OK/ERROR/WARNED)

## Language Reference for Writing Tests

The model language (`.gm`), rule/computation language (`.grg`), and sequence language are all relevant for frontend tests since the frontend parses and type-checks all of them. Per-chapter summaries of the user manual are in `../doc/summaries/`. The relevant chapters are:

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
- `sequencecomputation.md` — Computation blocks, assignments, built-in functions/procedures
- `sequencegraphquerymanipulation.md` — Array accumulation methods, rule queries
- `sequencesmultirulecalls.md` — Multi-rule calls, match classes, indeterministic choice
- `sequencesadvanced.md` — Sequence definitions, transactions, backtracking, multi-rule backtracking, for loops, mapping clause, persistence provider transactions

### Data Structures and Features
- `container.md` — set, map, array, deque with methods and operators
- `graph.md` — Graph manipulation, neighbourhood functions, subgraphs, visited flags
- `filters.md` — User-defined, auto-generated, auto-supplied, lambda expression filters
- `indices.md` — Attribute, incidence count, name, uniqueness indices
- `parallelization.md` — Action and sequence parallelization

The shell language (`grshell.md`, `validation.md`) is *not* relevant for frontend tests — it is only for backend/semantic tests from engine-net-2.

## What to Test

Frontend tests exercise the compiler's ability to:
- **Accept valid constructs** (`should_pass`): every syntactically and semantically valid language feature combination
- **Reject invalid constructs** (`should_fail`): type errors, scope violations, invalid syntax, constraint violations, missing declarations
- **Warn on suspicious constructs** (`should_warn`): constant conditions, unused elements, potentially unintended patterns

### Tips for should_fail Tests

Target a single specific error per test file. The test name should hint at what is being tested (e.g., `alternative_003.grg` for the third alternative-related error case, even though speaking names should be preferred). Common error categories:
- Type mismatches (assigning incompatible types)
- Scope violations (using undeclared elements, redeclaring in nested scope)
- Invalid modifier combinations (e.g., `exact` on wrong construct)
- Constraint violations (e.g., deleting outer elements from iterated)
- Missing required parts (e.g., rule without rewrite block)

### Tips for should_pass Tests

Cover feature combinations, not just isolated features (but don't go to extremes, separation of concern is still preferred). The existing tests use a numeric suffix convention for variants of the same feature (e.g., `array_001.grg` through `array_006.grg`). When a test needs a custom model, create a `.gm` file with the same base name or a descriptive `_model` suffix name.

### Naming Convention

Tests typically follow the pattern `feature_NNN.grg` where `NNN` is a zero-padded sequence number. Descriptive names (e.g., `abstract_element_reference_in_nested_pattern.grg`) are also used for more specific scenarios and should be preferred.
