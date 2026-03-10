# Nested and Subpattern Yielding

### Execution Model

Two distinct passes separated by the match object tree:

| Pass | Direction | Purpose |
|------|-----------|---------|
| Matching | top-down (outside → nested inside) | pattern matching; input parameters passed down |
| Match tree assembly | bottom-up (inside → containing outside) | `yield` blocks executed; output def parameters synthesized |
| Rewriting | top-down (outside → nested inside) | graph changes applied; rewrite input parameters passed down |
| Rewrite output | bottom-up | RHS `yield` assignments executed; rewrite def parameters synthesized |

The `---` separator splits each pattern/rewrite into declarative (above, top-down) and imperative/output (below, bottom-up) sections.
Matching order of pattern parts cannot be controlled (the runtime chooses); rewriting order can be controlled with order specifications.

### LHS `def` Variables (pattern output, yielded bottom-up after matching)

- `def var v:Type [= init_expr [= yield(yield_expr)]];` -- output entity; init evaluated top-down (before nested matching), yield_expr evaluated bottom-up (after nested matching); iterated match collections (`count`, `[?it]`) are only available in the yield_expr, not in the init_expr
- `def ref v:Type`, `def n:NodeType`, `def -e:EdgeType->` -- analogous for containers/objects/nodes/edges
- `yield { yield target = expr; }` block -- constrained eval block in pattern part; may not assign to non-def variables or change the graph; executed bottom-up during match tree construction
- Subpattern def output parameter: declared as `def` in subpattern header; passed back with `yield` at call site: `p(args --- yield defVar);`
- A def entity from the pattern part cannot be yielded to from the rewrite part (constant after matching)

### Iterated Accumulation (LHS)

- `yield { yield sum = sum + n.a; }` inside iterated -- accumulate across all iterated instances into an outer def variable
- `for(i in iteratedName) { yield sum = sum + i; }` -- for-loop over named iterated's def variables (outside the iterated)
- `count(iteratedName)` -- count of matches of a named iterated (only in yield/eval block)
- `[?iteratedName]` -- iterated query: returns `array<match<r.it>>` of all iterated matches (during yielding); analogous to rule query in sequences

### Iterated Filtering (LHS)

- `iterated it \ filterCall \ ...;` -- apply filters to named iterated's match array (reorders or removes matches); only top-level iterateds supported
- Must appear below `---` in the pattern part; same filter syntax as for rules

### RHS `def` Variables (rewrite output, yielded bottom-up during rewriting)

- `def var v:Type [= init_expr];` in rewrite part -- output during rewriting; also useful as temporary variable
- `eval { yield target = expr; }` in rewrite part -- assigns to outer def variable
- Subpattern rewrite output: `pattern P(...) modify(--- def out:Type)` + `p(rwArgs --- yield defVar);`
- `yield` prefix required on any assignment to a def variable from an outer scope

### Ordered Evaluation (RHS)

Default rewriting execution order (when no order specifications given):

1. Extract elements from match
2. Create new nodes
3. Call rewrite of subpatterns *(+ evalhere, emithere, alternative Name, iterated Name if specified)*
4. Call rewrite of nested iterateds
5. Call rewrite of nested alternatives
6. Redirect edges
7. Retype (and merge) nodes
8. Create new edges
9. Retype edges
10. Create subpatterns
11. Attribute reevaluation (`eval`)
12. Remove edges
13. Remove nodes
14. Remove subpatterns
15. Emit / Exec
16. Return

To control order, place order specifications below `---` in the rewrite part; they execute in syntactic order:

- `evalhere { yield target = expr; }` -- local eval at syntactic position (before/between nested rewrites)
- `emithere` -- emit at syntactic position
- `iterated iteratedName;` / `multiple iteratedName;` / `optional iteratedName;` -- execute named iterated/multiple/optional rewrite here
- `alternative altName;` -- execute named alternative rewrite here
- `pattern subpatternUsageName;` -- execute named subpattern rewrite here (distinct from subpattern rewrite application which also passes arguments)
