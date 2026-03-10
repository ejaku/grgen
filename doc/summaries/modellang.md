# Graph Model Language

### Node and Edge Types
- `node class N;` -- minimal node type (inherits from built-in `Node`)
- `node class N extends N1, N2 { ... }` -- with multiple inheritance and body
- `edge class E;` / `directed edge class E;` -- directed edge (inherits from `Edge`)
- `undirected edge class UE;` -- undirected edge (inherits from `UEdge`)
- `arbitrary edge class AE;` -- matches directed or undirected (inherits from `AEdge`, always abstract)
- Multiple inheritance supported for both nodes and edges
- An edge type cannot be both directed and undirected

### Base Type Hierarchy
- `AEdge` (abstract root for all edges) → `Edge` (directed) and `UEdge` (undirected)
- All nodes implicitly inherit from `Node`
- Default when no direction keyword given: directed (`extends Edge`)
- `directed`/`undirected`/`arbitrary` keywords make the intent explicit and control inheritance

### Type Modifiers
- `abstract` -- cannot instantiate; subtypes can still be matched via abstract type; not inherited
- `const` -- rules cannot write attributes defined in this class (shell/API still can); not inherited; does not apply to inherited attributes

### Attributes
- `attr : Type;` -- plain attribute declaration
- `attr : Type = expr;` -- with default initializer (constant expression)
- `const attr : Type;` -- read-only in rules
- `attr = expr;` -- override inherited attribute's default value (AttributeOverwrite)
- Attribute types: `boolean`, `byte`, `short`, `int`, `long`, `float`, `double`, `string`, `object`, enums, containers, node/edge/graph types
- Init expressions evaluated in file order; forward references to other attributes are illegal
- No overwriting of attribute declarations (only of default values); virtual inheritance semantics: exactly one declaration per attribute identifier on any path from type to root

### Enumerations
- `enum Color { red, green, blue = 5 }` -- C-like semantics (auto-increment from 0)
- Implicit cast to `int` and `string`; cannot cast `int` back to `enum`
- Multiple items may share the same int value (C enum semantics)
- Items from another enum type can only be used if that enum is defined before the current one

### Connection Assertions
- `connect N1[mult] --> N2[mult]` inside edge class body
- Multiplicities: `[*]` = `[0:*]`, `[+]` = `[1:*]`, `[n]` = `[n:n]`, `[n:m]`, `[n:*]`
- Arrow variants: `-->` (directed), `<--` (equivalent to `B-->A`), `--` (undirected), `?--?` (arbitrary; directed edges matched in both directions)
- `copy extends` -- inherit connection assertions from direct supertype(s)
- NOT enforced at creation; checked via `validate` command
- Three validation modes:
  - normal: edges not covered by any matching assertion are accepted
  - `strict`: each edge must be covered by at least one matching assertion
  - `strict only specified`: strict only for edge types that have assertions

### Modeling Hints
- Use types and inheritance on both nodes and edges; fine-grained hierarchies improve matching performance
- Represent relationships with typed edges (one edge type per relationship)
- Use `contains` edges for containment (parent → child); `next` edges for ordering (predecessor → successor)
- Combine both: `contains` for tree structure, `next` for sibling ordering
- For statically-known depth/arity use dedicated edge types (e.g. `left`, `right` for binary trees)
- Don't materialize transitive relationships — use `isReachable`/`reachable` or recursive subpatterns
- Don't materialize bidirectional relationships with two edges — use undirected edges
- For sparse node marking, reflexive edges of a special type are more efficient than attributes or visited flags

### Model Includes
- `#using "OtherModel.gm"` -- import base types from another model
