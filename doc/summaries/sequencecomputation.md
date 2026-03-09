# Sequence Computations

### Computation Blocks
- `{computation}` in sequence position -- always true unless final expression evaluates to default
- `{c1; c2; ...; cn}` -- compound; value is last computation's
- `{{expr}}` -- return expression result as sequence boolean

### Assignments
- `v = expr`, `elem.attr = expr`, `v[index] = expr`, `elem.visited[flagId] = val`
- `yield v = expr` -- write to def variable in enclosing pattern

### Expressions
- Operators (ascending precedence): `? :` → `||` → `&&` → `|` → `^` → `&` → `\` → `==,!=,~~` → `<,<=,>,>=,in` → `<<,>>,>>>` → `+,-` → `*,/,%` → `~,!,-,+`
- Note: `|`, `^`, `&` serve double duty as boolean strict/bitwise and as container union/xor/intersection; `\` is set/map difference; `+` is also container concatenation and string concatenation
- `def(v1,v2)` -- true iff all variables non-null
- `@("name")` -- element by persistent name
- `[?r]` -- rule query returning `array<match<r>>`

### Storages (Container Variables in Sequences)
- Storages: set/map/array/deque typed variables used to decouple processing phases
- Indexed access: `v=m[k]` (read), `a[i]=v` (write) -- for map, array, deque
- For loop over set/map: `for{v in setVar; seq}`, `for{k->v in mapVar; seq}`
- For loop over array/deque: `for{v in arrayVar; seq}`, `for{k->v in arrayVar; seq}` (k is int index)
- Warning: do not modify the container being iterated; use `clone(c)` first if needed

### Call Syntax
- `f(args)` -- function call (expression, returns value)
- `(outs)=p(args)` -- procedure call (statement, zero or more outputs)
- `v.fm(args)` -- function method call (expression, chainable)
- `(outs)=v.pm(args)` -- procedure method call (statement)

### Built-in Functions
- Graph: `nodes(Type)`, `edges(Type)`, `source(e)`, `target(e)`, `opposite(e,n)`
- Neighbourhood: `adjacent*`, `incident*`, `reachable*`, `boundedReachable*` (set, count, boolean variants)
- Subgraph: `inducedSubgraph(set<Node>)`, `definedSubgraph(set<Edge>)`, `equalsAny`, `getEquivalent`
- Identity/lookup: `nameof`, `uniqueof`, `nodeByName`, `edgeByName`, `nodeByUnique`, `edgeByUnique`
- Other: `random`, `typeof(v)` (type name as string; cf. rule language where it returns a type object)
- Packages: `File::*`, `Time::now()`, `Math::*`

### Built-in Procedures
- Graph manipulation: `add(Type)`, `rem(elem)`, `retype(elem,Type)`, `merge(target,source)`, `redirectSource/Target/SourceAndTarget`
- Subgraph: `insert(graph)`, `insertCopy(graph,anchor)`, `insertInduced(nodes,anchor)`
- Visited: `valloc()`, `vfree(id)`, `vreset(id)` -- `elem.visited[id]` as expression/assignment target
- Output: `emit(...)`, `emitdebug(...)`, `record(...)`, `canonize(g)`
- Packages: `File::*`, `Transaction::*`, `Debug::*` (for sequences in rule execs)

### Built-in Function Methods (container/string)
- Common: `size()`, `empty()`, `peek()`
- Array: `indexOf(v)`, `indexOf(v,i)`, `lastIndexOf(v)`, `orderAscendingBy<attr>()`, `orderDescendingBy<attr>()`, `keepOneForEach<attr>()`
- Map: `domain()`, `range()`, `asArray()`; Set: `asArray()`
- Deque: `subdeque(i,len)`, `asArray()`, `asSet()`, `indexOf(v)`, `lastIndexOf(v)`
- String methods from typexpr chapter

### Built-in Procedure Methods (container)
- `add(elem)`, `rem(elem)`, `clear()` -- mutators for set/map/array/deque (return the changed container)
- Note: change assignments (from rule language) are NOT available in sequence computations
