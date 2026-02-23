# Multi-Rule Calls and Match Classes

### Rule Prefixed Sequence
- `[for{r;s}]` -- for each match of r: rewrite, execute s

### Multi-Rule Execution
- `[[r1,r2]]` -- find all matches of all rules, rewrite all
- `[[r1,r2]\mc.filter]` -- with match class filter

### Multi-Rule Prefixed Sequence
- `[[for{r1;s1},for{r2;s2}]\mc.f]` -- per-rule sequences with global filtering

### Indeterministic Choice
- `$|(s1,s2,...)` / `$&(...)` / `$||(...)` / `$&&(...)` -- random order evaluation
- `$.(0.5 s1, 0.3 s2, 0.2 s3)` -- weighted random
- `{<r1,[r2]>}` -- some-of-set: match all, execute all matched
- `${<r1,[r2]>}` -- one-of-set: match all, execute one randomly

### Match Classes
- `match class MC { def var score:double; def n:Node; } \filter1, filter2`
- Rules/tests `implement` match classes
- Match class constructor: `match<class MC>()`
- Filters on match classes operate across multi-rule matches
