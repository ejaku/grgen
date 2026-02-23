# BE (Backend / Code Generation)

The `be/` package is the code generation layer of the GrGen compiler frontend. It translates the Intermediate Representation (IR) into C# source code targeting the engine-net-2 runtime (libGr and lgspBackend); the generated source files are then extended (with pattern matching code and embedded exec implementations) and compiled into DLLs by GrGen.exe (the C# compiler backend).

## Directory Structure

```
be/
├── Backend.java              # Backend interface (init, generate, done)
├── BackendFactory.java       # Factory interface for backend creation
├── IDBase.java               # Type ID assignment and mapping, using IDTypeModel.java and TypeID.java, all 3 are only used by the C backend
├── C/                        # Legacy C backend (6 files aimed at integration into FIRM compiler, unused)
│   ├── CBackend.java         # Abstract C backend base
│   ├── SearchPlanBackend.java # C code generation (frame-based)
│   ├── InformationCollector.java # Type/attribute metadata extraction
│   ├── MoreInformationCollector.java
│   ├── AttrTypeDescriptor.java
│   └── EnumDescriptor.java
└── Csharp/                   # Primary C# backend (~27K lines total)
    ├── SearchPlanBackend2.java     # Main orchestrator (entry point)
    ├── CSharpBase.java             # Base utilities for all generators (~211KB)
    ├── ModelGen.java               # Graph model code generation (~160KB)
    ├── ModelIndexGen.java          # Index support generation (~85KB)
    ├── ModelExternalGen.java       # External object/function stubs (~29KB)
    ├── ActionsGen.java             # Rule/action code generation (~148KB)
    ├── ActionsMatchGen.java        # Match object generation (~38KB)
    ├── ActionsExpressionOrYieldingGen.java  # Expression (and yielding statement) compilation (~178KB)
    ├── ActionsExecGen.java         # Exec statement generation (~22KB)
    ├── ModifyGen.java              # Graph modification/rewrite code (~75KB)
    ├── ModifyEvalGen.java          # Eval statements in rewrites (~150KB)
    ├── ModifyExecGen.java          # Imperative exec in rules (~13KB)
    ├── ExpressionGenerationState.java
    ├── ModifyGenerationState.java
    ├── ModifyGenerationStateConst.java
    └── ModifyGenerationTask.java
```

## Code Generation Flow (SearchPlanBackend2)

The list below describes the module responsibilities. The actual top-level call structure is: `init()` → `generate()` → `ModelGen.genModel()` → `ActionsGen.genActionlike()`; `ModelIndexGen`/`ModelExternalGen` are called by `ModelGen` internally, and `ActionsExpressionOrYieldingGen`/`ActionsExecGen`/`ModifyGen`/`ModifyEvalGen`/`ModifyExecGen` are called by `ActionsGen` internally.

1. **Initialization** (`init()`) - Receive IR unit as parameter, detect C# keyword conflicts in type names, set type prefixes if needed
2. **Model generation** (`ModelGen`) - Generate `*Model.cs` with type definitions, attribute storage, type hierarchies, connection assertions
3. **Index generation** (`ModelIndexGen`) - Generate attribute/incidence index interfaces and lookup methods
4. **External types** (`ModelExternalGen`) - Generate `*ExternalFunctions.cs` stubs for user-implemented external functions
5. **Action generation** (`ActionsGen`) - Generate `*Actions_intermediate.cs` with rule/subpattern/match-class structures (`LGSPRulePattern`/`LGSPMatchingPattern` subclasses), `PatternGraph` descriptors (element connectivity, type filters, condition predicates), and match object interfaces/implementations; the lgspBackend then inserts the actual nested-loop search programs at the `// GrGen insert Actions here` comment at the end of the file
6. **Expression/YieldingStatements** (`ActionsExpressionOrYieldingGen`) - Compile IR expressions to C# expression trees (using `GRGEN_EXPR`-namespace classes defined in the C# backend) or yielding statement lists; the lgspBackend emits the real code from those trees (/lists)
7. **Exec/imperative** (`ActionsExecGen`) - Generate embedded exec stubs inside `#if INITIAL_WARMUP` ... `#endif` sections: `EmbeddedSequenceInfo` structs (XGRS string + needed-entity metadata), `ApplyXGRS_*` method stubs, and `XGRSClosure_*` classes for subpattern/alternative/iterated execs; the lgspBackend compiles the file with `INITIAL_WARMUP` defined to extract sequence metadata, and parses/processes the XGRS strings, replacing the `ApplyXGRS_*` method stubs by their generated implementation; the frontend only checks syntax / pre-parses for this purpose
8. **Modification** (`ModifyGen`) - Generate RHS rewrite logic (element creation/deletion, retyping, attribute updates)
9. **Evaluation** (`ModifyEvalGen`) - Generate eval statement code within rewrites (assignments, control flow, function calls)
10. **Exec/emit invocation** (`ModifyExecGen`) - Generate the *call sites* for imperative statements inside the Modify method: for `Emit`, calls `procEnv.EmitWriter.Write(...)` with evaluated arguments; for `Exec` in top-level rules, calls `ApplyXGRS_*` directly; for `Exec` in subpatterns/alternatives/iterateds, instantiates the corresponding `XGRSClosure_*` and defers it via `AddDeferredSequence` (executed after the entire top-level rewrite completes via `ExecuteDeferredSequencesThenExitRuleModify`)

## Key Design Aspects

- **`CSharpBase`** is the largest file (~211KB) providing shared utilities: type formatting, container operations, graph queries. Key method: `genExpression()` generates inline C# code executed at runtime — used especially for expressions in the rewrite/modify part of rules.
- **`SourceBuilder`** (from `util/`) used throughout for indentation-aware C# code emission.

## Modify Generation State

The four state/task classes support the `ModifyGen` / `ModifyEvalGen` / `ModifyExecGen` code generation:

- **`ExpressionGenerationState`** - Interface for state needed during expression generation: temporary variable mapping (`mapExprToTempVar`), result variable usage, model access, parallelization and profiling flags.
- **`ModifyGenerationStateConst`** - Interface extending `ExpressionGenerationState`. Provides read-only access to the sets of common/new/deleted/yielded/retyped nodes, edges, and subpatterns, plus attribute tracking (needed attributes, attributes stored before delete, access-via-interface info, match class name).
- **`ModifyGenerationState`** - Mutable implementation of `ModifyGenerationStateConst`. Holds all state needed for generating the rewrite part of an action, computed from the LHS/RHS pattern comparison in the `ModifyGenerationTask`.
- **`ModifyGenerationTask`** - Specifies what rewrite code to generate: `MODIFY` (normal LHS → RHS rewrite); `CREATION` (subpattern-only — with an empty LHS and the subpattern as RHS); `DELETION` (subpattern-only — with the subpattern as LHS and an empty RHS). The CREATION/DELETION rewrites are used when a parent rule's RHS adds or removes a subpattern usage (in contrast to only using the subpattern dependent replacement).

The ModifyGenerationStateConst default access offers a read-only view of the ModifyGenerationState to prevent inadvertent side-effects and enforce visibility of the changed members; note that within `ModifyGen.genModify()` a two-phase usage pattern is used: first a series of `collect*` calls to collect data in the state, then `gen*` methods using it to generate the code.
