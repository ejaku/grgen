# Performance Optimization

### Search Plans
- GrGen generates nested loops for pattern matching; loop order (search plan) matters greatly
- Searching: first a lookup into the graph binds a pattern element by type, then neighbouring elements are traversed following graph structure
- Initial static search plan: an unoptimized pseudo-random order (not taking type frequencies into account)
- Better: runtime statistics via `custom graph analyze` + `custom actions gen_searchplans Rule`
- Or supply a statistics file at compile time: `GrGen.exe -statistics file.txt spec.grg`
- Display current search plan: `custom actions explain Rule`
- Export statistics: `custom graph export_statistics file.txt`; load in shell: `new set statistics file.txt`

### Find, Don't Search (Rule Design Tips)
- Use specific types (not generic `Node`/`Edge`) to narrow candidate sets for lookups
- Split attribute conditions into parts depending on fewer pattern elements — each part is checked as early as its required elements are bound, pruning branches earlier
- Prefer directed edges over undirected (undirected doubles the search space and reduces statistics quality)
- Keep patterns connected; disconnected components cause cartesian product explosion
- Take care when using nested and subpatterns — they introduce matching boundaries that disconnect otherwise connected components (use `explain` to check; inlining helps at depth one)
- Prefer rooted matching: pass element locations from previous rules as parameters instead of re-searching

### Indices for Optimization
- Type indices: automatically used by search planning — fine-grained types narrow the candidate set for lookups and improve statistics quality
- Neighbourhood (incident element) indices: automatically used by search planning — traversal of incident edges/nodes avoids full graph lookups
- Attribute indices: explicit fast lookup by attribute value (B-tree, O(log n)); must be used explicitly in pattern or if-expression — not applied automatically
- Incidence count indices: fast lookup by count of incident edges
- Name/unique indices: O(1) lookup by name or unique ID
- Multi-index spatial queries: combine multiple attribute indices for spatial querying (no dedicated spatial index type)

### Profiling
- GrShell prints execution time after each sequence
- Enable search step counting: `set profile on` (or `-profile` compiler option)
- After sequence execution: number of search steps printed in addition to time
- Detailed per-action profile: `show profile` — includes parallelization potential estimate

### Parallelize
- `[parallelize:N]` annotation — parallelize pattern matcher of a rule with N threads
- Parallel isomorphy checking: dedicated operation
- Explicit parallel execution in sequences
- All-bracketing `[r]` preferred over `r*` loops for exhaustive application; even more so for parallel matchers

### Compilation and Static Knowledge
- Compile with saved statistics: `GrGen.exe -statistics file.txt` — push analysis cost to compile time
- Compiled sequences (from `.grg`) execute faster than interpreted sequences (from `.grs`)
- Pre-compile dlls with `ngen` (.NET) or `--aot` (mono) to reduce JIT overhead for short-running tasks

### Miscellaneous Tips
- Visited flags are most efficient for marking large numbers of elements (stored in graph element flags); but don't allow direct lookup of marked elements — use storages for that
- All-bracketing `[r]` > sequence loops `r*` > iterated patterns (performance order)
- Prefer `reachable*()` predicate over subpattern recursion for reachability (tighter implementation, no pushdown overhead)
- For intersection-like queries on densely connected nodes, hashset-based helper functions beat nested loop matching (O(n) vs O(n²))
- Consider helper C# external functions for non-graph-native tasks
