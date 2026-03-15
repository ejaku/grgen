# Multi-Rule Calls and Match Classes

### Rule Prefixed Sequence
- `[for{r;s}]` -- for each match of r: rewrite, execute s; result `true` iff at least one s yielded `true`
- Return values assigned as scalar per rewrite (not arrays); input params and per-rule filters supported

### Multi-Rule Execution
- `[[r1,r2]]` -- find all matches of all rules, rewrite all; succeeds iff at least one match found
- `[[r1,r2]\mc.filter]` -- with match class filter applied across all matches of all rules
- Return values: one array per return parameter, for each rule (length = number of successful applications)
- User's responsibility to ensure matches of different rules do not conflict

### Multi-Rule Prefixed Sequence
- `[[for{r1;s1},for{r2;s2}]]` -- find all matches of all rules; rule-by-rule in syntactic order: for each match rewrite and execute corresponding sequence; succeeds iff at least one sequence yielded `true`
- `[[for{r1;s1},for{r2;s2}]\mc.f]` -- with match class filter: rewrites executed in order of the globally filtered matches list (overrides syntactic rule order)

### Indeterministic Choice
- `$|(s1,s2,...)` / `$&(...)` / `$||(...)` / `$&&(...)` -- random order evaluation
- `$.(0.5 s1, 0.3 s2, 0.2 s3)` -- weighted random
- `{<r1,[r2]>}` -- some-of-set: match all, execute all matched
- `${<r1,[r2]>}` -- one-of-set: match all, execute one randomly

### Match Classes
- `match class MC { def var score:double; def n:Node; } \filter1, filter2`
- Declarations: `def` node/edge/variable (primary filter targets); normal node/edge/variable also allowed (but unlinked only)
- Rules/tests `implement` match classes
- Match class constructor: `match<class MC>()` -- members initialized to default values
- Filters defined on a match class can be applied to the matches of all implementing rules

### Match Class Filter Calls
- Syntax at multi-rule calls: `\mc.filterName`, `\mc.filterName(args)`, `\mc.filterName<attr>`
- Match class identifier required as prefix before filter name
- Multi-rule query result typing: `[?[r]\<class MC>]` yields `array<match<class MC>>`
