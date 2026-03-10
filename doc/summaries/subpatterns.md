# Subpatterns

### Declaration and Usage

- `pattern P(params) { ... }` -- subpattern declaration; defines a reusable named shape in the global namespace
- `p:P(args);` -- subpattern entity declaration (named usage); must match for enclosing pattern to succeed
- `:P(args);` -- anonymous usage; when the entity doesn't need to be referenced for rewriting or yielding
- Parameters are *context* elements (handed in, not part of the subpattern's own matched set)
- No return types (unlike rules/tests); use `def` entities + `yield` to bring values outwards
- Rewrite parameters (elements available only at rewrite time): `pattern P(match_params) modify(rewrite_params) { ... }`

### Recursive Patterns

- Subpatterns may reference themselves directly or indirectly
- A self-referencing subpattern alone would never match (infinite pattern, finite graph); must combine with `alternative`, `optional`, or `iterated` to provide a base case
- Common patterns:
  - Iterated path: `optional { prev --> next:Node; :P(next); }` (or `alternative` with Empty/Further cases)
  - Bounded path: pass `var depth:int`, decrement each step, guard with `if{ depth >= 1; }`
  - Spanning tree (breadth + depth): `iterated { root <-:extending- next:Class; :SpanningTree(next); }`
- Analogy to formal grammars: rules ≈ axioms, subpatterns ≈ nonterminals, graphlets ≈ terminals, nested patterns ≈ EBNF operators, rewrite part ≈ semantic actions

### Regular Expression Syntax

Lightweight alternative to keyword syntax; combinable with subpatterns to yield EBNF-grammar-like specifications (valuable for the analogy, use sparingly):

| Keyword syntax | Regexp syntax | Meaning |
|----------------|---------------|---------|
| `iterated { P }` | `(P)*` | zero or more |
| `multiple { P }` | `(P)+` | one or more |
| `optional { P }` | `(P)?` | zero or one |
| `alternative { l1 { P1 } .. lk { Pk } }` | `(P1\|..\|Pk)` | exactly one case |
| `negative { P }` | `~(P)` | negative application condition |
| `independent { P }` | `&(P)` | positive application condition |
| `modify { R }` | `{+ R }` | modify rewrite |
| `replace { R }` | `{- R }` | replace rewrite |
| *(no keyword equivalent)* | `(P)[k]` / `(P)[k:l]` / `(P)[k:*]` | bounded iteration (exact / range / at-least) |

### Pattern Element Locking

**Isomorphy locking**: by default all matched elements across the whole pattern (including subpatterns) are isomorphy-locked against each other. This prevents re-matching an element already matched higher up — which is usually desired, but not when matching back-references (e.g. uses of a definition already matched).

- `independent(n)` in hom spec — lifts the isomorphy lock on `n`, allows it to be re-matched in a nested subpattern; needed when the set of such elements is dynamic (so hom with explicit listing is not applicable)

**Patternpath locking** (for NAC/PAC): by default negative/independent patterns match homomorphically to all already-matched elements; explicit locking adds isomorphy constraints:

- `pattern;` inside NAC/PAC — isomorphy-locks all elements from the directly enclosing pattern
- `patternpath;` inside NAC/PAC — isomorphy-locks all elements from all patterns on the dynamic containment path leading to this NAC/PAC (useful e.g. to require a second chain not to cross the first)
