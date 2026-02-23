# Array Accumulation and Queries

### Accumulation Methods (on `array<numeric>`)
- `.sum()`, `.prod()`, `.min()`, `.max()`, `.avg()`, `.med()`, `.medUnordered()`, `.var()`, `.dev()`, `.and()`, `.or()`

### By-Member Methods (on arrays of graph elements or match types)
- `.extract<attr>()` -- project member to `array<S>`
- `.indexOfBy<attr>(val)`, `.lastIndexOfBy<attr>(val)`, `.indexOfOrderedBy<attr>(val)`
- `.orderAscendingBy<attr>()`, `.orderDescendingBy<attr>()`
- `.groupBy<attr>()`, `.keepOneForEach<attr>()`

### Lambda Expression Array Methods
- `.map<S>{elem -> expr}` -- transform each element
- `.removeIf{elem -> predicate}` -- filter
- `.map<S>StartWith{init}AccumulateBy{acc, elem -> expr}` -- map with accumulation
- All support optional index and array self-reference

### Rule Queries
- `[?r]` -- all matches as `array<match<r>>` (no rewrite)
- `[?[r1,r2]\mc.f\<class mc>]` -- multi-rule query
- `for{v:match<r> in [?r]; s}` -- iterate matches
- `count[r]=>v` -- rewrite all, assign count
- `count[?r]=>v` -- count only (no rewrite)
