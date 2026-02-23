# Advanced Sequences

### Sequence Definitions
- Shell: `def seqName(params) : (returns) { body }`
- Compiled: `sequence seqName(params) : (returns) { body }` (in .grg)
- Recursive calls supported

### Transactions
- `<s>` -- if s fails, roll back all graph changes
- Nested transactions supported

### Backtracking
- `<<r ;; s>>` -- for each match of r: rewrite, run s; if s fails, rollback, try next match
- `<<r ;; t ;> false>>` -- force full enumeration

### Pause Insertions
- `/s/` -- changes from s NOT rolled back by enclosing transaction

### Multi-Rule Backtracking
- `<<[[r1,r2]];;s>>` -- try matches of r1 and r2 until s succeeds; match class filter `\mc.f` controls cross-rule ordering
- `<<[[for{r1;s1},for{r2;s2}]]>>` -- each rule has its own per-match sequence; match class filter controls ordering

### For Loops
- `for{v:Type in nodes(Type); s}` / `edges(Type)` / `incident(n)` / `adjacent(n)` / `reachable(n)` / `boundedReachable(n,depth)` etc.
- `for{v:int in [left:right]; s}` -- integer range (ascending or descending)
- `for{v in container; s}` / `for{v->w in map; s}` -- container iteration (also see sequences.md)
- `for{v:N in indexFunc(...); s}` -- index iteration
- All for loops: body always executes fully; result is false if any body iteration failed

### Mapping Clause
- `[:for{r;s}:]` -- returns array of graphs; for each match of r, clones host graph, applies r, runs s; includes result if s succeeded
- `[:for{r1;s1},for{r2;s2}:]` -- array concatenation of multiple clauses
- Host graph is **not** modified; `this` refers to the cloned graph during execution
- Expensive (one clone per match); intended for state space enumeration

### Persistence Provider Transactions
- `<: s :>` -- execute s within a database transaction (commit at closing angle; rollback only on crash)
- `>:<` -- commit-and-restart: commits current DB transaction, starts a new one immediately
- Distinct from in-memory transactions (`<>`): no rollback on failure, nested transactions not supported
- Without explicit DB transactions, every graph change is its own transaction (correct but very slow)

### Graph Nesting
- `in g {s}` -- execute s on subgraph g
- `(...)=g.r(...)` -- execute rule on subgraph
- `this` -- current graph (usable as expression; inside a method denotes the containing node/edge object)
