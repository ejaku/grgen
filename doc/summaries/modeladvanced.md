# Advanced Modelling

### Object Types
- `class C extends C1 { attrs }` -- internal class (garbage-collected, root type: `Object`)
- `transient class TC extends TC1 { attrs }` -- transient (cannot be stored in graph attributes, root: `TransientObject`)
- `new ObjectType@(attr1=v1)` -- creation with initializer
- `clone(obj)` (shallow), `copy(obj)` (deep)
- `==`/`!=` reference identity; `~~` deep attribute equality

### Methods
- Function methods: `function name(params) : RetType { ... }` in class body; side-effect free, dynamic dispatch, `this` mandatory
- Procedure methods: `procedure name(params) : (RetTypes) { ... }` in class body; may modify graph

### Packages
- Model: `package P { class/enum declarations }`
- Actions: `package P { rules/tests/patterns/filters/functions/procedures/sequences }`
- Cross-package reference: `PackageName::TypeName`; `global::name` from inside package
- Built-in packages: `Math`, `File`, `Time`, `Transaction`, `Debug`, `Synchronization`

### Graph Nesting
- Attributes of type `graph`
- `in g { sequence }` -- switch host graph, execute, switch back
- `g.r` -- shorthand for `in g { r }`
- `node edge graph;` -- enables `graphof(elem)` function
- `object class unique;` -- enables `uniqueof(obj)` for object instances
