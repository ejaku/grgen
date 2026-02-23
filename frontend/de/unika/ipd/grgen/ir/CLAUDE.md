# IR (Intermediate Representation)

The `ir/` package contains ~250 Java classes forming the Intermediate Representation. The IR is created after AST resolution and semantic checking, and serves as input to the C# backend code generators.

## Directory Structure

```
ir/
├── executable/  25 files  - Rules, functions, procedures, filters, sequences
├── expr/        23 files  - Expression types and operators
│   ├── array/             - Array operations
│   ├── deque/             - Deque operations
│   ├── graph/             - Graph queries (adjacent, reachable, incident, index access)
│   ├── invocation/        - Function/method invocations
│   ├── map/               - Map operations
│   ├── numeric/           - Numeric expressions
│   ├── procenv/           - Process environment queries
│   ├── set/               - Set operations
│   └── string/            - String operations
├── model/        8 files  - Type model (enum items, indices, connection assertions)
│   └── type/              - Node/edge/enum/object/external type definitions
├── pattern/     26 files  - Pattern graphs and graph entities
├── stmt/        44 files  - Statements (assignments, control flow, graph ops)
│   ├── array/             - Array add/remove/clear
│   ├── deque/             - Deque add/remove/clear
│   ├── graph/             - Graph manipulation (add/remove/retype/merge nodes/edges)
│   ├── invocation/        - Procedure/method invocations
│   ├── map/               - Map add/remove/clear
│   ├── procenv/           - Environment procs (emit, debug, transactions)
│   └── set/               - Set add/remove/clear
├── type/         9 files  - Type system hierarchy
│   ├── basic/             - Primitive types (int, float, bool, string, ...)
│   └── container/         - Container types (array, set, deque, map)
└── [root]       23 files  - Core IR classes and interfaces
```

## Core Base Classes

- **`IR`** - Abstract base for all IR objects. Implements `GraphDumpable`, `XMLDumpable`.
- **`Unit`** - Top-level container representing a complete compilation unit. Aggregates models, rules, functions, procedures, sequences.
- **`Entity`** - Typed instantiation (base for pattern nodes/edges and variables). Tracks type, constness, context (LHS/RHS).
- **`Identifiable`** - Named IR objects with identifier and annotation support.
- **`Ident`** - Identifier wrapper (name + source coordinates).

## Subdirectories

### executable/
Rules, functions, procedures, and filters.
- **`Rule`** - Graph rewrite rule (LHS pattern + RHS pattern + exec statements). Tracks min/max matches for iterated rules.
- **`Function` / `Procedure`** - User-defined functions and procedures with parameters and return types.
- **`FilterFunction` / `MatchClassFilterFunction`** - Filter functions or predicates for matches (post-processing of query results stemming from pattern matching).
- **`Sequence`** - Sequential execution specifications.
- **`ExternalFunction` / `ExternalProcedure`** - Wrappers for C# external code.

### expr/
Many diverse expressions for value computations.
- **`Expression`** - Abstract base. Key method: `collectNeededEntities()` for static analysis.
- **`Constant`**, **`VariableExpression`**, **`Operator`** - Core expression types.
- **`Qualification`** - Attribute access (entity.attribute).
- **`Cast`**, **`Count`**, **`EnumExpression`**, **`GraphEntityExpression`** - Specialized expressions.
- **`BuiltinFunctionInvocationExpr / NeighborhoodQueryExpr`** - Abstract base classes for all builtin functions / a multitude of builtin graph queries.

### model/
Graph model structure.
- **`Model`** - Container for node types, edge types, object types, enum types, indices, external functions.
- **`Index`** - Attribute-based indexing (subtypes: `AttributeIndex`, `IncidenceCountIndex`).
- **`ConnAssert`** - Connection assertions (cardinality constraints on edges).

### pattern/
Pattern graph representations for matching and rewriting.
- **`GraphEntity`** (extends `Entity`) - Base for `Node` and `Edge` in patterns.
- **`PatternGraphLhs`** / **`PatternGraphRhs`** - LHS (match) and RHS (replacement) patterns containing nodes, edges, variables, subpattern usages. Iterated patterns and alternative case patterns are modelled as Rule, negative/independent are PatternGraphLhs. Noteworthy method: `ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern()` propagates non-local elements from nested patterns (alternatives, iterateds) up into the enclosing pattern so that code generation can access them.
- **`Alternative`** / **`SubpatternUsage`** - Nested pattern alternatives (disjunctive case groups) and subpattern instantiation.
- **`RetypedNode` / `RetypedEdge`** - Elements that change type during rewriting.
- **`StorageAccess`**, **`IndexAccess`**, **`NameLookup`**, **`UniqueLookup`** - Bind a pattern element to a graph element by accessing a container or an index.

### stmt/
Imperative statements for rule/procedure bodies.
- **`Assignment`** variants (Var, Member, GraphEntity, Indexed) - Variable/attribute mutations.
- **`CompoundAssignment`** - Container-type compound operations: `|=` (union), `&=` (intersection), `\=` (without), `+=` (concatenate); **`CompoundAssignmentChanged`** additionally records whether a change occurred into a second target attribute via `|>`, `&>`, `=>`.
- **`ConditionStatement`**, **`SwitchStatement`**, **`WhileStatement`**, **`DoWhileStatement`** - Control flow.
- **`ExecStatement`** - Sequence execution.
- **`BuiltinProcedureInvocationBase`** - Built-in operations (add/remove nodes/edges, etc.).

### type/
Type hierarchy.
- **`Type`** - Abstract base. Enum `TypeClass` with 25 categories (int, string, set, array, node, edge, match, ...).
- **`PrimitiveType`** - Built-in types.
- **`DefinedMatchType`**, **`MatchTypeIterated`** - Custom match class types.

## Relationship to Other Stages

```
Parser → AST (resolve/check) → IR (you are here) → C# Backend Code Generation
```

The AST converts to IR via `constructIR()` methods. The IR is then consumed by `SearchPlanBackend2` and its generator classes.

Note: The directory/file/class structure of the IR maps quite well to the directory/file/class structure of the AST (the `ast/` package). Most IR classes have a corresponding AST node class that constructs them.
