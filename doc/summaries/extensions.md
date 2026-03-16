# External Extensions

### External Attribute Types
- Declare in `.gm`: `external class MyType;` or `external class MyType extends BaseType { ExternalFunctionDecl... ExternalProcedureDecl... }`
- May declare an inheritance hierarchy and external function/procedure methods; attributes cannot be specified
- Implement in C# the partial class MyType (the generated part declares the inheritance relationship as specified); see `api.md` External Extensions section for the generated and expected file structure
- Types are opaque to GrGen (only function/procedure methods operate on them); explicit cast to `object` required (no implicit cast)
- Methods may register own undo items with the transaction manager to realize rollback for external attributes

### External Functions
- Declare in `.gm`: `external function myFunc(param:Type) : RetType;`
- Callable from rules, computations, and sequences
- See `api.md` for what is generated and what has to be implemented - the same holds for the other constructs specified here

### External Procedures
- Declare in `.gm`: `external procedure myProc(param:Type) : (RetType);`
- Can modify graph (add/delete nodes/edges, set attributes)
- Callable from rules, computations, and sequences

### External Filter Functions
- Declare in `.grg`: `external filter myFilter<rule>(var v:Type);`
- Filter name must be globally unique (unlike predefined auto-generated filters)
- Implement in C#: receives IMatchesExact, with typed match interface `IMatch_r` (generated per rule); match object has typed members for each node/edge/variable; iterateds yield `IMatchesExact<IMatch_r_iterName>`; alternatives have per-case match interfaces; subpattern usages typed with subpattern match interface
- Applied with `rule\myFilter` syntax

### External Sequences
- Declare in `.grg`: `external sequence mySeq(param:Type) : (RetType);`
- Callable from sequences

### External Emit and Parse
- Declare in `.gm`: `external emit class;` (or `external emit graph class;`)
- Custom serialization/deserialization for external types during graph export/import
- Enables display of external types in debugger (incl. yComp)
- See `api.md` for the C# methods to implement (`Parse`, `Serialize`, `Emit`, `External`)

### External Cloning and Comparison
- Declare in `.gm`: `external copy class;` / `external ~ class;` / `external < class;`
- Extends graph element copying or attribute type comparison to handle external types
- Allows value semantics for external types (normally compared by reference identity)

### Shell Commands
- `!CommandLine` — execute command via OS shell
- `external CommandLine` — call `External(CommandLine)` method in extension file (requires `external emit class`)

### Shell and Compiler Parameters
- `GrGen.exe -r <assembly-path>` — reference external assembly in generated assemblies
- `GrGen.exe -keep [<dest-dir>]` — keep generated C# source files
- `GrGen.exe -debug` — compile with debug symbols and validity checking code
- `new add reference Filename` — GrShell equivalent of `-r`
- `new set keepdebug on|off` — GrShell equivalent of `-keep` and `-debug`

### Annotations
- Annotations: `IdentDecl[ident=constant, ...]`; keys must be identifiers, values must be constants; any combination allowed; custom annotations queryable at API level
- Only the following keys have a built-in effect:
- `[prio=N]` on node/edge — matching priority (default 1000, higher = earlier in search plan)
- `[maybeDeleted=true]` on node/edge — suppress error when element may be homomorphically matched with a deleted element
- `[containment=true]` on edge type — mark as containment relation (used for XMI export; default false)
- `[parallelize=N]` on rule/test — parallelize the pattern matcher with N threads
- `[validityCheck=false]` on node/edge — skip contained-in-graph checks (active when `-debug` compiler flag or shell option is set) for that element; on rule — skip checks for all nodes/edges in the rule's patterns
