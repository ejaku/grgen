# Rule Language

### File Structure
- `#using "Model.gm"` -- reference model files
- `#include "other.grg"` -- include rule files
- Global variables: `::x:NodeType;`, `-::e:EdgeType->;`, `var ::v:int;`, `ref ::r:set<int>;`

### Rules and Tests
- `test TestName(params) : (ReturnTypes) { Pattern }` -- match-only
- `rule RuleName(params) : (ReturnTypes) { Pattern Rewrite }` -- match + rewrite

### Parameters
- `x:NodeType` -- node parameter (pre-bound pattern element)
- `-e:EdgeType->` -- edge parameter
- `var x:int` -- value parameter
- `ref x:set<int>` -- reference parameter
- `x:NodeType<null>` -- nullable: if null, element searched in graph
- `x:NodeType<SuperType>` -- wider interface type; more specific matching
- Return: `return(expr1, ...);`

### Graphlet Syntax
- Nodes: `x:NodeType`, `:NodeType` (anonymous), `.` (anonymous `Node`)
- Directed edges: `-->`, `<--`, `-e:EdgeType->`, `<-e:EdgeType-`
- Undirected: `--`, `-e:UE-`
- Arbitrary direction: `?--?`, `?-e:AE-?`
- Reverse-lookup direction: `<-->` (matches either direction, not both)
- Graphlets split across statements via shared names; cyclic via name reuse

### Isomorphic vs Homomorphic Matching
- Default: **isomorphic** (injective) -- distinct pattern elements bind to distinct host elements
- `hom(a, b)` -- allow `a` and `b` to match same element; requires common subtype
- `hom` is **non-transitive**: `hom(a,b)` + `hom(b,c)` does NOT imply `hom(a,c)`
- `independent(x)` -- `x` homomorphic to all others (dangerous)

### Pattern Statements
- Graphlets, `hom(...)`, `exact(...)`, `induced(...)`, `if { BoolExpr; }` conditions
- Nested patterns (`negative`, `independent`, `iterated`, `optional`, `multiple`, `alternative`)
- Subpattern entity declarations

### Rewrite Part
- `replace { ... }` -- unmentioned LHS elements deleted (SPO semantics)
- `modify { ... }` -- all LHS elements kept; explicit `delete(elem, ...)` for removal
- `eval { ... }` -- attribute evaluation (executed AFTER creation, BEFORE deletion)
- Evaluation order: create new elements, eval, delete elements, emit, exec, return
