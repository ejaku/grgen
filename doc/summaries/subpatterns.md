# Subpatterns

### Declaration and Usage
- `pattern P(params) { ... }` -- subpattern declaration (reusable named pattern)
- `p:P(args);` -- subpattern entity declaration (usage); must match for enclosing to succeed
- Parameters are context (not part of the subpattern's own pattern)

### Recursive Patterns
- Subpatterns can reference themselves (directly or indirectly)
- Must combine with `alternative`/`optional`/`iterated` for a base case
- Enables matching iterated paths, spanning trees, bounded-depth structures
- Bounded path: pass `var depth:int` parameter, decrement each recursion, guard with `if{ depth >= 1; }`

### Pattern Element Locking
- `independent(n)` in hom spec -- lifts isomorphy lock, allows re-matching already-matched elements
- `pattern;` in NAC/PAC -- locks all elements from directly enclosing pattern
- `patternpath;` in NAC/PAC -- locks all elements from all dynamically containing patterns
