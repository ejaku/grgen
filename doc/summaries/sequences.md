# Sequences (Rule Application Control)

### Variables and Constants
- Graph-global: `::v` (untyped, auto-nulled on element deletion; saved/restored with graph)
- Sequence-local: `v:Type` (typed, declared explicitly; type errors caught at compile/parse time)
- `v:Type` as standalone atom -- declares a local variable, always succeeds
- `v = w` / `v:Type = w` -- assign variable or literal to variable (always succeeds); general expressions require `{ v = expr }` in computation context
- `v = $(42)` -- random integer in [0, 42); `v = $(1.0)` -- random double in [0.0, 1.0)
- `v = $%(string)` / `v = $%(Node)` -- user input query (GrShell only)
- `true` / `false` -- boolean constants acting as always-succeeding / always-failing atoms
- `v` -- boolean variable used as predicate atom (succeeds iff `v` is true)
- `def(v)` -- succeeds iff `v` is non-null (useful to check if a graph-global was nulled by deletion)

### Rule Application
- `r(args)` -- apply rule with arguments, rewrite first match; fails if no match
- `(u,v) = r(x,y)` -- with input args `x`,`y` and return values assigned to `u`,`v`; parentheses always required even for one return value
- `(b,c) = R(x,y,z) => a` -- return values to `b`,`c` and boolean success to `a` (recommended form)
- `?r` -- test modifier: match only, no rewrite
- `%r` -- multi-purpose flag: dumps matched elements in GrShell; acts as break point in debug mode
- `[r(x,y)]` -- all-bracketing: collect ALL matches, then rewrite each; returns arrays
- `(u:array<Node>, v:array<int>) = [r(x,y)]` -- all-bracketing with typed result arrays (one per return param)
- `$[r]` -- find all matches, pick one randomly, rewrite it (returns arrays like all-bracketing)
- `$%[r]` -- choice point: user selects which match to rewrite (debugger)
- `$u[r]` -- find all, rewrite at most `u` randomly chosen matches
- `$l,u[r]` -- require at least `l` matches (else fail), rewrite at most `u`; `u` may be `*` for unlimited
- `r\filter` -- apply filter to matches before rewriting
- Note: `[r]` collects all matches *first*, then rewrites — different from `r*` which re-matches after each step; all-bracketing is unsafe if matches share elements

### Connectives (ascending precedence)
- `s1 <; s2` / `s1 ;> s2` -- then-left / then-right (evaluate both, return result of left / right)
- `s1 || s2` -- lazy or (skip `s2` if `s1` succeeds)
- `s1 && s2` -- lazy and (skip `s2` if `s1` fails)
- `s1 | s2` -- strict or (always evaluate both)
- `s1 ^ s2` -- strict xor
- `s1 & s2` -- strict and (always evaluate both)
- `!s` -- negation
- `$` prefix: random operand order (`$&&`, `$|`, `$&`, etc.); `$%` for user-chosen order

### Loops and Decisions
- `if{Cond; True; False}` -- conditional; `if{Cond; True}` = `if{Cond; True; true}` (lazy implication)
- `s*` -- repeat until failure, always returns true
- `s+` -- equivalent to `s && s*`
- `s[n]` -- at most n repetitions; `s[m:n]` -- fail if fewer than m; `s[m:*]` -- fail if fewer than m, no upper bound

### Result Assignment
- `s => v` -- assign boolean result of `s` to `v`
- `s &> v` / `s |> v` -- accumulate with and / or into `v`
