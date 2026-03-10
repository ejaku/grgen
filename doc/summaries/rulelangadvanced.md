# Advanced Matching and Rewriting

### Pattern Modifiers (usable with tests and rules)
- `exact` -- all incident edges of matched nodes must be specified in pattern; match fails otherwise
- `induced` -- all edges between the specified nodes must be in the pattern; `induced(a,b,c)` ≠ `induced(a,b); induced(b,c)`
- Applied globally (`exact rule r { ... }` / `exact test t { ... }`) or per-node set (`exact(n1, n2);`)
- Internally resolved to equivalent NACs: O(n) for `exact`, O(n²) for `induced`

### Rule-Only Modifiers (cannot be used with tests)
- `dpo` -- double-pushout semantics = `dangling` + `identification`; forbids rule application when conditions would be violated (in contrast to SPO which ruthlessly deletes)
- `dangling` -- **forbids** rule application if a node to be deleted has incident edges not specified in the pattern (dangling condition; requires exact-semantics on deleted nodes)
- `identification` -- **forbids** rule application if two hom-matched elements have conflicting deletion status (identification condition; prevents deleting an element that is also being preserved)
- Applied globally (`dpo rule r { ... }`) or per-node (`dangling(n1);`)

### Static Type Constraint
- `x:T\T1` / `x:T\(T1 + T2)` -- exclude one or more types (and their subtypes) from matching

### Dynamic Type Matching
- `el:typeof(x)` -- must have same exact runtime type as x (pattern or rewrite)

### Retyping
- RHS: `y:NewType<x>` -- retype x to NewType, common supertype attributes preserved; source and target types need not be related in hierarchy
- During eval, both old (`x`) and new (`y`) names are alive for reading/writing
- Retyping is conceptually performed *after* the SPO-conforming rewrite
- LHS: `new:Type<old>` -- type cast in pattern; fails matching if runtime type of `old` is not compatible; `new` gives access to `old` as `Type`; allows type refinement in nested patterns
- Supports `typeof`: `y:typeof(z)<x>` -- retype x to z's exact dynamic type

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
