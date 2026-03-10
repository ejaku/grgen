# Rule Language

### File Structure
- `#using "Model.gm"` -- reference model files
- `#include "other.grg"` -- include rule files
- Global variables: `::x:NodeType;`, `-::e:EdgeType->;`, `var ::v:int;`, `ref ::r:set<int>;`

### Rules and Tests
- `test TestName(params) : (ReturnTypes) { Pattern }` -- match-only; `return(...)` goes in the pattern part
- `rule RuleName(params) : (ReturnTypes) { Pattern Rewrite }` -- match + rewrite; `return(...)` goes in the rewrite part
- `rule R(...) implements MatchClass1, MatchClass2 { ... }` -- implement match classes (see sequencesmultirulecalls chapter)

### Parameters
- `x:NodeType` -- node parameter (pre-bound pattern element)
- `-e:EdgeType->` -- edge parameter
- `var x:int` -- value parameter (primitive types; passed by value; assignment in eval has undefined effect)
- `ref x:set<int>` -- reference parameter (container/object types; by reference; container add/remove allowed; assignment in eval has undefined effect)
- `x:NodeType<null>` -- nullable: if null at call site, element is searched in graph (doubles generated matchers)
- `x:NodeType<SuperType>` -- wider interface type; match fails if concrete type not compatible

### Graphlet Syntax
- Nodes: `x:NodeType`, `:NodeType` (anonymous), `.` (anonymous `Node`)
- Directed edges: `-->`, `<--`, `-e:EdgeType->`, `<-e:EdgeType-`
- Undirected: `--`, `-e:UE-`
- Arbitrary direction: `?--?`, `?-e:AE-?`
- Either direction: `<-->` (directed edge with unspecified direction; matches if `-->` or `<--` matches — not both required)
- Graphlets split across statements via shared names; cyclic via name reuse
- **Rewrite part is inner scope of pattern** — pattern names visible in rewrite; rewrite names not visible in pattern
- Same name in multiple graphlets = same element; anonymous elements in rewrite always create new elements

### Isomorphic vs Homomorphic Matching
- Default: **isomorphic** (injective) -- distinct pattern elements bind to distinct host elements
- `hom(a, b)` -- allow `a` and `b` to match same element; requires common subtype
- `hom` is **non-transitive**: `hom(a,b)` + `hom(b,c)` does NOT imply `hom(a,c)`
- `independent(x)` -- `x` homomorphic to all others (use as last resort; potentially dangerous)
- `independent(x \ (a+b))` -- homomorphic to all except `a` and `b` (iso constraint)

### Pattern Statements
- Graphlets, `hom(...)`, `independent(...)`, `exact(...)`, `induced(...)`, `if { BoolExpr; }` conditions
- Nested patterns (`negative`, `independent`, `iterated`, `optional`, `multiple`, `alternative`)
- Subpattern entity declarations
- Pattern modifiers (`exact`, `induced`) and rule-only modifiers (`dpo`, `dangling`, `identification`) -- see rulelangadvanced

### Rewrite Part
- `replace { ... }` -- unmentioned LHS elements deleted (SPO semantics); anonymous LHS elements also deleted
- `modify { ... }` -- all LHS elements kept (including anonymous); explicit `delete(elem, ...)` for removal
- `eval { ... }` -- attribute/computation statements; multiple eval blocks concatenated in order
- `delete(e1, e2, ...)` -- only in modify mode; executes after all other rewrite operations
- **SPO (single-pushout) semantics**: (1) deleting a node also deletes all incident edges; (2) in case of conflict between deletion and preservation (possible with hom-matched elements), deletion is prioritized
- Evaluation order: create new elements → eval → delete elements
