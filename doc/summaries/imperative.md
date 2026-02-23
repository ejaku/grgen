# Embedded Sequences and Output

### exec in Rules
- `exec(rewriteSequence);` -- in modify/replace block, runs after declarative rewrite
- Has access to LHS and RHS pattern elements
- `exec(s ;> yield outerVar=innerVar)` -- yield values back to def variables

### emit in Rules
- `emit(stringExpr, ...);` -- print to output stream
- `emitdebug(...)` -- always to console
- `emithere(...)` / `emitheredebug(...)` -- ordered output (interleaved with subpattern rewrites)
- Execution order: emithere first (in syntactic order), then emit, then exec

### Deferred exec
- `exec` in nested/subpattern rewrites: deferred until top-level rule finishes
- Cannot use `yield` (containing rule already done)

### scan/tryscan
- `scan<Type>(string)` -- parse string to type (crashes on failure)
- `tryscan<Type>(string)` -- test parsability (returns boolean)
