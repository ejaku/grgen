# Sequence Computations

### Computation Blocks
- `{computation}` in sequence position -- always true unless final expression evaluates to default
- `{c1; c2; ...; cn}` -- compound; value is last computation's
- `{{expr}}` -- return expression result as sequence boolean

### Assignments
- `v = expr`, `elem.attr = expr`, `v[index] = expr`, `elem.visited[flagId] = val`
- `yield v = expr` -- write to def variable in enclosing pattern

### Expressions
- All operators from typexpr chapter available
- `def(v1,v2)` -- true iff all variables non-null
- `@("name")` -- element by persistent name
- `[?r]` -- rule query returning `array<match<r>>`
- Container operations: `|` (union), `&` (intersection), `\` (difference), `+` (concatenation)

### Built-in Functions and Procedures
- Graph queries: `nodes(Type)`, `edges(Type)`, `source(e)`, `target(e)`, `opposite(e,n)`
- Neighbourhood: `adjacent*`, `incident*`, `reachable*`, `boundedReachable*` (set, count, boolean variants)
- Graph manipulation: `add(Type)`, `rem(elem)`, `retype(elem,Type)`, `merge(target,source)`, `redirectSource/Target/SourceAndTarget`
- Subgraph: `inducedSubgraph(set<Node>)`, `definedSubgraph(set<Edge>)`, `insert(graph)`, `insertCopy(graph,anchor)`
- File: `File::export(path)`, `File::import(path)`, `File::exists(path)`, `File::delete(path)`
- Visited: `valloc()`, `vfree(id)`, `vreset(id)`, `elem.visited[id]`
- Transaction: `Transaction::start/pause/resume/commit/rollback`
- Output: `emit(...)`, `emitdebug(...)`, `record(...)`
- Identity: `nameof`, `uniqueof`, `nodeByName`, `edgeByName`, `nodeByUnique`, `edgeByUnique`
- `typeof(v)` -- type name as string (in sequences; in rule language returns type object)
- `canonize(graph)` -- canonical string
