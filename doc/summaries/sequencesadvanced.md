# Advanced Sequences

### Sequence Definitions
- Shell: `def seqName(params) : (returns) { body }`
- Compiled: `sequence seqName(params) : (returns) { body }` (in .grg)
- Recursive calls supported; shell sequences can be overwritten with identical signature (enables mutual recursion; sequence must be defined before use)
- Output variables assigned only if the sequence succeeds; sequence call behaves like a rule call

### Transactions
- `<s>` -- if s fails, roll back all graph changes
- Nested transactions supported; committed nested transactions are also rolled back if an enclosing transaction fails

### Backtracking
- `<<r ;; s>>` -- compute all matches of r; for each: rewrite, run s; if s fails rollback and try next; succeeds (commits) on first s that succeeds, fails if all matches exhausted

### Pause Insertions
- `/s/` -- changes from s bypass the transaction undo log and are NOT rolled back by enclosing transaction

### State Space Enumeration
- `<<r ;; t ;> false>>` -- force full enumeration (visits all matches even after success; useful for collecting states)
- Combining recursion (depth) with backtracking (breadth) → depth-first state space search
- use `/s/` inside `<<r;;t>>` to materialize states that exist in time into space so that they survive rollback
- Symmetry reduction: `==` checks for graph isomorphy; prune states already seen (costly when graphs are actually isomorphic)
- Debugger caveat: rollback may corrupt online graph visualization; press `p` to dump `.vcg` if display looks suspicious
- Performance note: undo log is maintained while a transaction is pending; avoid unless needed

### Multi-Rule Backtracking
- `<<[[r1,r2]];;s>>` -- try matches of r1 and r2 until s succeeds; match class filter `\mc.f` controls cross-rule ordering ([[...]]\mc.f); without filter: all matches of r1 first, then all matches of r2 (in order of appearance)
- `<<[[for{r1;s1},for{r2;s2}]]>>` -- each rule has its own per-match sequence; match class filter controls ordering; without filter: same default order

### For Loops
- `for{v:Type in nodes(Type); s}` / `edges(Type)` -- iterate graph elements of compatible type
- Incident/adjacent: `incoming(n)` / `outgoing(n)` / `incident(n)` (binds edge); `adjacentIncoming(n)` / `adjacentOutgoing(n)` / `adjacent(n)` (binds node)
- Reachable nodes: `reachable(n)` / `reachableIncoming(n)` / `reachableOutgoing(n)` and bounded variants
- Reachable edges: `reachableEdges(n)` / `reachableEdgesIncoming(n)` / `reachableEdgesOutgoing(n)` and bounded variants
- Parameters: source node alone; or source + incident edge type; or source + incident edge type + adjacent node type
- Unlike set-returning functions: reflexive edges and multi-edges may be enumerated multiple times
- `for{v:int in [left:right]; s}` -- integer range (ascending or descending, inclusive)
- `for{v in container; s}` / `for{v->w in map; s}` -- container iteration (also see sequences.md)
- `for{v:N in indexFunc(...); s}` -- index iteration
- All for loops: all body iterations are always carried out (even after a failure); the loop fails if any body iteration failed (i.e. yields false), succeeds otherwise

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
- `var g:graph` -- graph variable (usable as node/edge attribute, local variable, or parameter)
- `in g {s}` -- execute s on subgraph g
- `(...)=g.r(...)` -- execute rule on subgraph
- `this` -- current graph (usable as expression; inside a method denotes the containing node/edge object)
