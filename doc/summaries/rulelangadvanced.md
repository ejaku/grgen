# Advanced Matching and Rewriting

### Rule/Pattern Modifiers
- `exact` -- all incident edges of matched nodes must be specified in pattern
- `induced` -- induced subgraph over specified nodes must be fully described
- `dpo` -- double-pushout = `dangling` + `identification` (prevents implicit deletions)
- `dangling` -- deleted nodes must have all incident edges in pattern
- `identification` -- hom-matched elements with different deletion status cannot be same
- Applied globally (`exact rule r { ... }`) or per-node (`exact(n1, n2);`)

### Static Type Constraint
- `x:T\(T1 + T2)` -- exclude types T1, T2 (and their subtypes) from matching

### Dynamic Type Matching
- `el:typeof(x)` -- must have same exact runtime type as x (pattern or rewrite)

### Retyping
- RHS: `y:NewType<x>` -- retype x to NewType, common attributes preserved
- LHS: `new:Type<old>` -- type cast in pattern; fails matching if cast impossible
- Supports `typeof`: `y:typeof(z)<x>` -- retype x to z's dynamic type

### Copy and Clone (RHS)
- `copy<x>` -- deep copy (object-type attributes copied transitively)
- `clone<x>` -- shallow copy (object-type attributes are shared references)

### Node Merging (RHS)
- `bw:Type<b, w>` -- merge nodes b and w; edges from w redirected to surviving node

### Edge Redirection (RHS, modify mode)
- `a !<-e-! y` -- redirect both endpoints
- `a -e->! y` -- redirect target
- `a !<-e- y` -- redirect source

### Attribute Initialization (RHS)
- `n:N@($="foo", attr=42)` -- syntactic sugar for eval assignments; `$` = persistent name
