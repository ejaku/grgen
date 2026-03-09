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

### Query Result Combination — auto-generated (preliminary, use sparingly)
These constructs live in the **rules language** (function/match class bodies), not in sequences directly.
They are questionable regarding the core graph rewriting concepts and should be used with care.
- `auto(join<natural>(leftArray, rightArray))` -- natural join: cartesian product filtered by equality of equally-named members; result type is `array<match<class MC>>`
- `auto(join<cartesian>(leftArray, rightArray))` -- cartesian product (no common-name merging; add name prefixes to avoid collision)
- `auto(match<A> | match<B>)` -- auto-generated match class body: union of members under name merging (hampers readability)
- `auto(target.keepOneForEach<X>Accumulate<Y>By<Z>)` -- function body: GROUP BY X, accumulate Y using method Z; only for arrays of matches (not node/edge arrays); can be applied after matches array return, unlike the filter variant
- `\MC.keepOneForEach<X>Accumulate<Y>By<Z>` -- equivalent as a filter on match class queries (usable from sequences)
