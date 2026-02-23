# AST (Abstract Syntax Tree)

The `ast/` package contains ~450 Java classes forming the Abstract Syntax Tree for the GrGen frontend compiler. The AST is built during parsing of `.grg` (rules) and `.gm` (model) files, then undergoes resolution and semantic checking before conversion to IR.

## Directory Structure

```
ast/
├── decl/       ~85 files  - Declarations (rules, functions, patterns)
│   ├── executable/        - Rule, subpattern, function, procedure, filter, sequence declarations
│   └── pattern/           - Node, edge, variable, subpattern usage declarations
├── expr/       ~130 files - Expressions
│   ├── array/             - Array operations (sort, map, filter, accumulate, ...)
│   ├── deque/             - Deque operations
│   ├── graph/             - Graph queries (adjacent, reachable, incident, index access)
│   ├── invocation/        - Function/method invocations
│   ├── map/               - Map operations
│   ├── numeric/           - Numeric expressions
│   ├── procenv/           - Process environment queries
│   ├── set/               - Set operations
│   ├── string/            - String operations
│   └── [root]             - Base expressions, constants, operators, casts
├── model/      ~24 files  - Graph model type definitions
│   ├── decl/              - Member, attribute index, model declarations
│   └── type/              - Node/edge/enum/object type nodes
├── pattern/    ~22 files  - Pattern graph structures (LHS/RHS, alternatives, iterateds, subpattern replacement node)
├── stmt/       ~95 files  - Statements
│   ├── array/             - Array add/remove/clear
│   ├── deque/             - Deque add/remove/clear
│   ├── graph/             - Graph manipulation (add/remove/retype/merge nodes/edges)
│   ├── invocation/        - Procedure/method invocations
│   ├── map/               - Map add/remove/clear
│   ├── set/               - Set add/remove/clear
│   └── procenv/           - Environment procs (emit, debug, transactions, assertions)
├── type/       ~50 files  - Type system
│   ├── basic/             - Primitive types (int, float, bool, string, ...)
│   ├── container/         - Container types (array, set, deque, map)
│   └── executable/        - Function/procedure/rule type signatures
├── util/       ~20 files  - Resolvers and checkers
└── [root]      26 files   - Core infrastructure
```

## Core Base Classes

- **`BaseNode`** - Abstract base for all AST nodes. Manages scoping, parent relationships, child nodes, and the resolve/check phases. Implements `GraphDumpable` and `Walkable`.
- **`UnitNode`** - Root AST node for an entire compilation unit. Contains collections of models, rules, functions, procedures, sequences, packages.
- **`IdentNode`** - Identifier nodes (names for types, variables, declarations).
- **`CollectNode<T>`** - Generic collection node for managing groups of child AST nodes.
- **`ScopeOwner`** - Interface for nodes that manage an identifier scope.

## Two-Phase Processing

1. **Resolve** (`resolveLocal()`) - Convert unresolved `IdentNode` references to resolved `DeclNode` declarations.
2. **Check** (`checkLocal()`) - Semantic validation (type compatibility, scope correctness, constraint checking).

After both phases, AST nodes convert themselves to IR via `constructIR()`.

## Key Patterns

- **Context flags** on `BaseNode`: `CONTEXT_LHS_OR_RHS`, `CONTEXT_ACTION_OR_PATTERN`, `CONTEXT_TEST_OR_RULE`, `CONTEXT_NEGATIVE`, `CONTEXT_INDEPENDENT`, `CONTEXT_COMPUTATION`, etc.
- **Resolver/Checker framework** in `util/`: `DeclarationResolver`, `TypeChecker`, `SimpleChecker`, etc.
- **`DeclNode`** - Base class for all declarations; carries a name (`IdentNode`) and a type (`TypeNode`).
- **`DeclaredCharacter`** interface for anything that declares an identifier.
- **`ConnectionCharacter`** interface for graph connections (edges between nodes in patterns).
