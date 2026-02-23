# Graph Model Language

### Node and Edge Types
- `node class N;` -- minimal node type (inherits from built-in `Node`)
- `node class N extends N1, N2 { ... }` -- with multiple inheritance and body
- `edge class E;` -- directed edge (inherits from built-in `Edge`)
- `undirected edge class UE;` -- undirected edge (inherits from built-in `UEdge`)
- `arbitrary edge class AE;` -- matches directed or undirected (inherits from `AEdge`, always abstract)
- Multiple inheritance supported for both nodes and edges
- An edge type cannot be both directed and undirected

### Type Modifiers
- `abstract` -- cannot instantiate; subtypes can still be matched via abstract type
- `const` -- rules cannot write attributes defined in this class (shell/API still can); not inherited

### Attributes
- `attr : Type;` -- plain attribute declaration
- `attr : Type = expr;` -- with default initializer (constant expression)
- `const attr : Type;` -- read-only in rules
- `attr = expr;` -- override inherited attribute default value
- Attribute types: `boolean`, `byte`, `short`, `int`, `long`, `float`, `double`, `string`, `object`, enums, containers, node/edge/graph types

### Enumerations
- `enum Color { red, green, blue = 5 }` -- C-like semantics (auto-increment from 0)
- Implicit cast to `int` and `string`; cannot cast `int` back to `enum`

### Connection Assertions
- `connect N1[mult] --> N2[mult]` inside edge class body
- Multiplicities: `[*]`, `[+]`, `[n]`, `[n:m]`, `[n:*]`
- Arrow variants: `-->`, `<--`, `--`, `?--?`
- NOT enforced at creation; checked via `validate` command
- Three validation modes: normal, `strict`, `strict only specified`

### Model Includes
- `#using "OtherModel.gm"` -- import base types from another model
