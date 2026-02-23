# Nested and Subpattern Yielding

### Separator `---`
- Splits pattern/rewrite into declarative (above) and imperative (below) sections

### LHS `def` Variables (pattern output)
- `def var v:Type = init_expr = yield(yield_expr)` -- output entity with optional init and yield
- `yield { yield target = expr; }` -- executed bottom-up during match tree construction
- Iterated accumulation: `yield sum = sum + n.a;` across iterated instances
- `count(iteratedName)` -- number of matches of a named iterated
- `[?iteratedName]` -- returns `array<match<r.it>>` (iterated query)

### RHS `def` Variables (rewrite output)
- `def var v:Type` in rewrite part -- output during rewriting
- `eval { yield target = expr; }` in rewrite part
- Subpattern output: `pattern P(params) modify(--- def out:Type)` + `p(args --- yield defVar);`

### Ordered Evaluation
- Default execution order: extract match, create nodes, subpattern rewrites, iterated rewrites, alternative rewrites, redirect edges, retype nodes, create edges, retype edges, create subpatterns, eval, remove edges, remove nodes, remove subpatterns, emit/exec, return
- `evalhere { ... }` -- local eval at syntactic position
- `pattern p;` / `iterated it;` / `alternative alt;` -- order specification for when nested rewrites execute
- All order specs below `---` separator execute in syntactic order
