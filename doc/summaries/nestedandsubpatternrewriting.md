# Nested and Subpattern Rewriting

### Nested Pattern Rewriting

- `modify { ... }` or `replace { ... }` nested directly inside each nested pattern scope
- Every matched instance of an iterated/multiple has its rewrite applied; zero matches = no rewrite triggered, but enclosing pattern's rewrite is still applied
- Alternative: only the rewrite of the matched case is applied
- `return` NOT available in nested rewrites (only at top-level rule)
- `exec` statements behave slightly differently in nested rewrites (deferred — see imperative chapter)
- Empty top-level `modify` may be omitted when all rewriting is done in alternatives/iterateds; this shorthand is only valid for the top-level pattern — omitting a rewrite from a nested pattern makes the entire nested pattern match-only (like a `test`; must be consistent across all nested patterns)

### Restrictions on Deletion and Retyping

- Deletion or retyping of elements from an *outer* (containing) pattern is only allowed inside: `alternative` cases, `optional` patterns, subpatterns
- NOT allowed inside `iterated` or `multiple` rewrites (ambiguous — could apply multiple times)

### Subpattern Rewriting

- `p(args);` in rewrite part — triggers the subpattern's rewrite with the given rewrite arguments
- Omitting the application entirely — subpattern is kept untouched (in modify mode)

**Deletion and preservation of subpatterns:**

In `modify` mode (keep by default):
- `delete(p);` — explicitly delete all elements matched by subpattern `p`

In `replace` mode (delete by default):
- `p;` — preserve subpattern `p` (occurrence notation, analogous to nodes/edges)

**Rewrite parameters** — for passing elements created at rewrite time (not available at match time):
- Declaration: `pattern P(match_params) modify(rewrite_params) { ... }`
- Application: `p(rewrite_args);`

**Creating subpatterns in the rewrite part:**
- `:PatternName(args);` — instantiates the subpattern (creates its graph structure); use with caution: alternatives (which case?) and abstract types (which concrete type?) raise issues
