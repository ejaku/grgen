# Sequences (Rule Application Control)

### Variables
- Graph-global: `::v` (untyped, auto-nulled on element deletion)
- Sequence-local: `v:Type` (typed, scoped to sequence)

### Rule Application
- `r(args)` -- apply rule, rewrite first match
- `(ret1,ret2)=r(args)` -- with return values
- `?r` -- test modifier (match only, no rewrite)
- `[r]` -- all-bracketing: rewrite ALL matches; returns arrays
- `$[r]` -- random: find all, pick one randomly
- `$l,u[r]` -- bounded: require l..u matches, rewrite at most u
- `r\filter` -- apply filter to matches
- `r =>v` -- capture boolean result

### Connectives (ascending precedence)
- `s1 <; s2` / `s1 ;> s2` -- then-left / then-right
- `||` (lazy or), `&&` (lazy and)
- `|` (strict or), `^` (xor), `&` (strict and)
- `!s` -- negation
- `$` prefix: random operand order (`$&&`, `$|`, etc.)

### Loops and Decisions
- `if{Cond; True; False}` -- conditional
- `s*` -- Kleene star (repeat until failure, returns true)
- `s+` -- one or more
- `s[n]` / `s[m:n]` / `s[m:*]` -- bounded repetition

### Result Assignment
- `s =>v` -- assign boolean result
- `s &>v` / `s |>v` -- accumulate with and/or
