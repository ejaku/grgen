# Filters

### Match Types
- `match<r>` -- match of rule r
- `match<r.it>` -- match of iterated within r
- `match<class mc>` -- match class match
- `.member` -- access pattern entity from match

### User-Defined Filters
- `filter F<RuleName>(params) { ... }` -- body operates on `this : array<match<r>>`
- Setting entries to `null` removes them
- Must be side-effect free (no graph changes)

### Auto-Generated Filters (declared: `\filterName<var>` or `\auto`)
- `orderAscendingBy<v[,v2]>` / `orderDescendingBy<v[,v2]>` -- sort (multi-key)
- `groupBy<v>` -- group by value
- `keepSameAsFirst<v>` / `keepSameAsLast<v>` -- keep matching first/last
- `keepOneForEach<v>` -- deduplicate by value
- `keepOneForEach<v>Accumulate<w>By<m>` -- GROUP BY v, accumulate w with method m
- `auto` -- remove automorphic matches

### Auto-Supplied Filters (no declaration needed)
- `keepFirst(n)`, `keepLast(n)`, `keepFirstFraction(f)`, `keepLastFraction(f)`
- `removeFirst(n)`, `removeLast(n)`, `removeFirstFraction(f)`, `removeLastFraction(f)`

### Filter Calls
- `r\f` / `r\f(args)` / `r\filterName<var>` -- single filter
- `r\f1\f2\f3` -- chained (left to right)
- `[[r1,r2]\mc.f]` -- match class filter on multi-rule

### Lambda Expression Filters
- `\assign<member>{elem -> expr}` -- assign to member for each match
- `\removeIf{elem -> predicate}` -- remove matches
- `\assign<member>StartWith{init}AccumulateBy{acc, elem -> expr}` -- accumulating
