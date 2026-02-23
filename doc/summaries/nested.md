# Nested Patterns

### Negative Application Condition (NAC)
- `negative { PatternStatements }` -- forbids match if pattern present
- Own scope; identifiers from outer scope accessible but not redefinable
- Elements inside `negative` are `hom(everything outside)` by default; referencing outer element forces isomorphic matching
- Multiple `negative` blocks: ALL must be absent
- Nested negatives: double negation = "except if"

### Positive Application Condition (PAC)
- `independent { PatternStatements }` -- requires pattern to be present
- Same scoping rules as NAC
- Elements matched separately from main pattern (no contribution to match combinations)

### Pattern Cardinality
- `iterated { NestedBody }` -- match 0..N times (greedy); always succeeds
- `multiple { NestedBody }` -- match 1..N times; fails if 0 matches
- `optional { NestedBody }` -- match 0..1 times; always succeeds

### Iteration Breaking
- `break negative { Y }` inside iterated -- if Y matches, entire iterated (and enclosing pattern) fails
- `break independent { Y }` inside iterated -- if Y does NOT match, entire iterated fails
- Enables "for all X, property Y must hold" patterns

### Alternative Patterns
- `alternative { Case1 { ... } Case2 { ... } }` -- exactly one case must match
- Case selection order is **unspecified** (not textual); use negatives for exclusivity
- Type refinement in cases via cast: `v:Variable<f>`

### Named Nested Patterns
- `negative name { ... }`, `iterated name { ... }`, etc. -- required for filtering, yield, order specs

### Regular Expression Syntax
- `(P)*` = iterated, `(P)+` = multiple, `(P)?` = optional
- `(P1|P2)` = alternative, `~(P)` = negative, `&(P)` = independent
- `(P)[k]` = exactly k, `(P)[k:l]` = k..l times, `(P)[k:*]` = at least k
