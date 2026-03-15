# Filters

### Match Types
- `match<r>` -- match of rule r
- `match<r.it>` -- match of iterated within r
- `match<class mc>` -- match class match
- `.member` -- access pattern entity from match
- `clone(match<T>)` -- clone a match value
- `[?r]` yields `array<match<r>>`; `[?it]` yields `array<match<r.it>>`; `[?[t]\<class mc>]` yields `array<match<class mc>>`

### User-Defined Filters
- `filter F<RuleName>(params) { ... }` -- body operates on `this : array<match<r>>`; may iterate/modify matches; modifying match members allowed
- `filter F<class MC>(params) { ... }` -- match class variant; body operates on `this : array<match<class mc>>`
- Setting entries to `null` removes them (efficient in-place removal)
- Must be side-effect free (no graph changes)
- External filters can be declared and implemented in a C# accompanying file

### Auto-Generated Filters (declared: `\filterName<var>` or `\auto`)
- `orderAscendingBy<v[,v2]>` / `orderDescendingBy<v[,v2]>` -- sort (multi-key); v must be numeric/string/enum/boolean
- `groupBy<v>` -- group matches of equal v as neighbours; v may be any equality-comparable type incl. node/edge
- `keepSameAsFirst<v>` / `keepSameAsLast<v>` -- keep only matches whose v equals first/last match's v
- `keepOneForEach<v>` -- deduplicate by value; v may be any equality-comparable type incl. node/edge
- `keepOneForEach<v>Accumulate<w>By<m>` -- GROUP BY v, accumulate w (must be numeric) with array method m
- `auto` -- remove automorphic matches (symmetric matches covering same spot with permuted elements)

### Auto-Supplied Filters (no declaration needed)
- `keepFirst(n)`, `keepLast(n)`, `keepFirstFraction(f)`, `keepLastFraction(f)`
- `removeFirst(n)`, `removeLast(n)`, `removeFirstFraction(f)`, `removeLastFraction(f)`

### Filter Calls
- `r\f` / `r\f(args)` / `r\filterName<var>` -- single filter
- `r\f1\f2\f3` -- chained (left to right)
- `[[r1,r2]\mc.f]` -- match class filter on multi-rule

### Lambda Expression Filters
- `\assign<member>{elem:match<r> -> expr}` -- assign to member for each match
- `\removeIf{elem:match<r> -> predicate}` -- remove matches where predicate is true
- `\assign<member>StartWith{init}AccumulateBy{acc, elem:match<r> -> expr}` -- accumulating assignment
- Optional index: `{index:int -> elem:match<r> -> expr}` -- adds array index variable
- Optional array self-reference: `{this_:array<match<T>>; elem:match<r> -> expr}` -- access full matches array (e.g. to compare against aggregate)
- Match class variants: `\mc.assign<member>{elem:match<class mc> -> expr}`, `\mc.removeIf{...}`, `\mc.assign<member>StartWith{...}AccumulateBy{...}`
- Iterated variants: `\assign<member>{elem:match<r.it> -> ...}` etc. (use rule language expressions)
