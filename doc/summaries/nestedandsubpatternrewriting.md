# Nested and Subpattern Rewriting

### Nested Pattern Rewriting
- `modify { ... }` or `replace { ... }` inside nested scope
- Every matched instance of iterated has its rewrite applied
- Alternative: rewrite per matched case
- `return` NOT available in nested rewrites

### Restrictions
- Deletion/retyping of outer elements only in `alternative`, `optional`, subpatterns (not `iterated`/`multiple`)

### Subpattern Rewriting
- `p(args);` in rewrite part -- triggers subpattern's rewrite
- Omitting = subpattern kept untouched
- `delete(p);` -- deletes subpattern's matched elements
- Rewrite parameters: `pattern P(match_params) modify(rewrite_params) { ... }` -- pass elements created at rewrite time
