# Parallelization

### Action Parallelization
- Model prerequisite: `for function[parallelize=true];` in `.gm` file
- Rule/test annotation: `[parallelize=k]` — match with up to k worker threads (capped at available cores; max 64)
- Only the pattern matcher is parallelized, not the rewrite; helps only for search-intensive rules

### equalsAny Parallelization
- Model clause: `for equalsAny[parallelize=N];` — N worker threads for graph isomorphy checking
- Speeds up `equalsAny`/`getEquivalent` calls in state space enumeration

### Sequence Parallelization (experimental)
- Only available in interpreted sequences, not compiled; no debugger support; global variable access not synchronized
- Model clause: `for sequence[parallelize=N];` — N worker threads in the sequences thread pool
- `parallel [(out,...) =] in g1,v1 {s}, in g2,v2 {t}, ...` -- execute sequences in parallel on separate subgraphs; `value` is bound to the value expression in each branch
- `parallel array [(out,...) =] in graphArray,valueArray {s}` -- execute s in parallel once per entry in the subgraph array; `value` bound to corresponding value array entry

### Locking (experimental)
- `lock(expr) { seq }` -- in sequences: acquires lock on expr before entering, releases on exit
- `lock(expr) { stmts }` -- in rule language statements: same semantics
- `Synchronization::enter(obj)` -- enter lock (re-entrant per thread, blocks if held by another)
- `Synchronization::tryenter(obj):(boolean)` -- try to enter, returns false if already held by another
- `Synchronization::exit(obj)` -- release lock

### Thread-safe Graph Isomorphy Comparison
- `getEquivalentOrAdd(graph, array<graph>):(graph)` -- returns isomorphic graph from array or null; if null, adds the candidate to the array; safe to call from parallel worker threads
- `getEquivalentStructurallyOrAdd(graph, array<graph>):(graph)` -- same, but ignoring attributes
