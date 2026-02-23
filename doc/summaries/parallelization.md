# Parallelization

### Action Parallelization
- Rule annotation: `[parallelize=k]` (k = worker threads)
- Model prerequisite: `for function[parallelize=true];`
- Only parallelizes matching, not rewriting

### Sequence Parallelization (experimental)
- `parallel in g1,v1 {s}, in g2,v2 {t}` -- parallel on separate graphs
- `parallel array in ga,va {s}` -- parallel over array of graphs
- Model: `for sequence[parallelize=N];`

### Locking
- `lock(expr) { ... }` in sequences and computations
- `Synchronization::enter/tryenter/exit(obj)`
