# Embedded Sequences and Output

### exec in Rules
- `exec(rewriteSequence);` -- in modify/replace block, runs after declarative rewrite
- Has access to LHS and RHS pattern elements
- `exec(s ;> yield outerVar=innerVar)` -- yield values back to def variables

### emit in Rules
- `emit(stringExpr, ...);` -- print to output stream
- `emitdebug(...)` -- always to console

### Exec and Emit in Nested/Subpatterns
- `emit`/`emitdebug` available as in rules
- `emithere(...)` / `emitheredebug(...)` -- only in nested/subpatterns; ordered output interleaved with subpattern rewrites
- Execution order: emithere first (syntactic order), then emit, then exec
- `exec` is deferred: runs after top-level rule finishes; cannot use `yield`

### scan/tryscan
- `scan<Type>(string)` -- parse string to type (crashes on failure)
- `tryscan<Type>(string)` -- test parsability (returns boolean)
