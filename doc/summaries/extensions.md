# External Extensions

### External Attribute Types
- Declare in `.gm`: `external class MyType;` (optionally `extends BaseType`)
- May declare external function/procedure methods
- Implement in C#: class implementing `IExternalObject`
- Must provide: `Copy()`, equality, ordering (if needed)

### External Functions
- Declare in `.grg`: `external function myFunc(param:Type) : RetType;`
- Implement in `*ActionsExternalFunctions.cs` partial class
- Callable from rules, computations, and sequences

### External Procedures
- Declare in `.grg`: `external procedure myProc(param:Type) : (RetType);`
- Implement in `*ActionsExternalFunctions.cs` partial class
- Can modify graph (add/delete nodes/edges, set attributes)
- Callable from rules, computations, and sequences

### External Filter Functions
- Declare in `.grg`: `external filter myFilter<rule>(var v:Type);`
- Implement in C#: receives `IList<IMatch>`, can reorder/remove matches
- Applied with `rule\myFilter` syntax

### External Sequences
- Declare in `.grg`: `external sequence mySeq(param:Type) : (RetType);`
- Implement in C#: method in `*ActionsExternalFunctions.cs`
- Callable from sequences like built-in defined sequences

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
- `[prio:N]` — matching priority for pattern elements (default 1000, higher = earlier in search plan)
- `[maybeDeleted]` — suppress error when element may be homomorphically matched with a deleted element
- `[containment]` — mark edge type as containment (used for XMI export)
- `[parallelize:N]` — parallelize pattern matcher with N threads
- `[validityCheck:false]` — disable contained-in-graph checks for a node/edge or all elements of a rule
