# Advanced Modelling

### Object Types
- `class C extends C1 { attrs }` -- internal class (garbage-collected, root type: `Object`)
- `transient class TC extends TC1 { attrs }` -- transient (cannot be stored in graph attributes, root: `TransientObject`); preliminary concept
- `abstract`/`const` work the same as for node/edge types
- `new ObjectType()` -- plain creation (default attribute values); `new ObjectType@(attr1=v1, ...)` -- creation with initializer
- Casts up/down in type hierarchy; downcast succeeds only if runtime type is compatible
- `clone(obj)` (shallow), `copy(obj)` (deep); also available for transient objects (transient clone: container refs shared, non-transient clone: containers cloned)
- `==`/`!=` reference identity; `~~` deep attribute equality (tree-structured only -- no cycles/joins supported)
- `object class unique;` -- assigns a persistent unique id (`long`) to each instance; queryable via `uniqueof(obj)`

### Methods
- Function methods: `function name(params) : RetType { ... }` in class body; side-effect free, dynamic dispatch, `this` mandatory
- Procedure methods: `procedure name(params) : (RetTypes) { ... }` in class body; may modify graph; no `exec` allowed

### Method Calls
- Function method call: `target.funcName(args)` -- expression context; `target` may be a variable, attribute, or full expression
- Procedure method call: `(out1, out2) = target.procName(args)` -- statement context; `target` must be a variable or `var.attribute`
- `this` is mandatory (unlike Java/C#) to access attributes or call other methods from within a method body
- Dynamic dispatch: actual method called depends on the runtime type of `target`, not its static type

### Packages
- Model: `package P { class/enum declarations }`
- Actions: `package P { rules/tests/patterns/filters/functions/procedures/sequences }`
- Cross-package reference: `PackageName::TypeName`; `global::name` from inside package
- Built-in packages: `Math`, `File`, `Time`, `Transaction`, `Debug`, `Synchronization`

### Graph Nesting
- Attributes of type `graph` -- opaque from outside (only isomorphy comparison); opened by switching into them
- `in g { sequence }` -- sequence-level construct: switch host graph to `g`, execute sequence, switch back; rule pattern matching and rewriting always apply to a single current graph
- `g.r` -- syntactic sugar for `in g { r }` (single rule call)
- `node edge graph;` -- enables `graphof(elem)` function; costs 4/8 bytes per graph element (32/64-bit CLR)
