# Indices

### Built-in Indices
- **Type indices**: nodes/edges by type (O(1) access, O(k) iteration where k = elements of that type)
- **Neighbourhood indices**: outgoing/incoming edge lists per node (O(1) access, O(k) iteration); both directions followed for undirected/arbitrary edges
- These are always present and implicitly used by the search planner; they cannot be disabled

### Attribute Index
- Model: `index IndexName { Type.Attribute }`
- Pattern (exact): `n:N{IndexName==val}`
- Pattern (range, ascending — smallest matching value first): `n:N{ascending(IndexName>=lo, IndexName<hi)}` (bounds optional)
- Pattern (range, descending — largest matching value first): `n:N{descending(IndexName<=hi, IndexName>lo)}` (bounds optional)
- If retrieved element type is incompatible, matching continues with next candidate

#### Index Function Family (nodes; same pattern for edges with `edgesFromIndex*`)
Functions are available in rule language expressions and sequence computations:
- **By-value**: `nodesFromIndex(idx)`, `nodesFromIndexSame(idx,val)`, `nodesFromIndexFrom(idx,lo)`, `nodesFromIndexFromExclusive(idx,lo)`, `nodesFromIndexTo(idx,hi)`, `nodesFromIndexToExclusive(idx,hi)`, `nodesFromIndexFromTo(idx,lo,hi)`, plus `FromExclusiveTo`, `FromToExclusive`, `FromExclusiveToExclusive` variants — all return `set<Node>`
- **Ordered array**: append `AsArrayAscending` or `AsArrayDescending` to any name above (e.g. `nodesFromIndexFromToAsArrayAscending(idx,lo,hi)`) — returns `array<Node>`; ascending: lower bound first, descending: upper bound first post-index argument
- **For-loop (rule statements)**: append `Ascending`/`Descending` to loop function name (e.g. `for(n:N in nodesFromIndexFromExclusiveToDescending(idx,hi,lo)) {...}`; ascending: lower bound first, descending: upper bound first)
- **For-loop (sequences)**: `for{n:N in nodesFromIndexFromDescending(idx,hi); seq}`
- **Count**: prepend `count` and capitalize first letter, e.g. `countNodesFromIndexFromTo(idx,lo,hi):int`; use `indexSize(idx):int` for full index size (O(1))
- **Boolean predicate**: prepend `isIn` and capitalize, e.g. `isInNodesFromIndexFromTo(node,idx,lo,hi):boolean` (more efficient than `in` on result set)
- **Min/max**: `minNodeFromIndex(idx):Node`, `maxNodeFromIndex(idx):Node` (null if empty); same for edges

### Incidence Count Index
- Counts incident edges of each node; allows matching nodes ordered by their degree
- Model: `index Idx { countIncident(NodeType [, EdgeType [, NodeType]]) }` (also `countIncoming`, `countOutgoing`)
- Pattern: **same syntax as attribute index** — `n:N{ascending(Idx)}`, `n:N{ascending(Idx>=lo)}`, `n:N{descending(Idx<=hi)}`, `n:N{Idx==val}` — binds nodes ordered by incidence count
- `countFromIndex(idx, node):int` — fetch incidence count for a specific node from the index (available in rule language and sequences)

### Multiple Index Queries and Spatial Queries
- Pattern element must satisfy bounds for all specified indices (multi-index join / result set intersection):
  `n:N{multiple(idx1>=lo1, idx1<=hi1, idx2>=lo2, idx2<=hi2, ...)}` — always requires both lower and upper bound per index; use `minNodeFromIndex`/`maxNodeFromIndex` to emulate open-ended bounds
- Functions: `nodesFromIndexMultipleFromTo(idx1,lo1,hi1[,idx2,lo2,hi2]...):set<Node>`, same for edges
- For-loop forms available for rule statements and sequences
- **Spatial queries**: use one attribute index per dimension to emulate a spatial index; match nodes in 2D/3D bounding box even if unconnected:
  ```
  neighbor:Point3dNode{multiple(ix >= n.x - delta, ix <= n.x + delta,
                                iy >= n.y - delta, iy <= n.y + delta)}
  ```
  Or use `nodesFromIndexMultipleFromTo` in an `if` condition to filter connected pattern components by spatial proximity.

### Name Index (with named graphs)
- No declaration needed; active whenever using named graphs (always in GrShell)
- Pattern: `n:N{@("name")}` or `n:N{@(stringExpr)}` (expression may reference at most one other pattern element)
- `nameof(elem):string` — get name of node/edge (or subgraph; no arg = host graph)
- `nodeByName(name [, NodeType]):Node` — lookup by name; null if not found; optional type for typed result
- `edgeByName(name [, EdgeType]):AEdge` — lookup by name; null if not found
- `nameof(elem) = expr` — assign new name (in eval-block or procedure; must be unique, else runtime exception)

### Uniqueness Index
- Model: `node edge unique;` (uniqueness constraint — assigns unique ids) + `index unique;` (uniqueness index — allows lookup by id)
- Pattern: `n:N{unique[intExpr]}` (expression may reference at most one pattern element)
- `uniqueof(elem):int` — get unique id of node/edge/subgraph
- `nodeByUnique(id [, NodeType]):Node` — lookup by unique id; null if not found
- `edgeByUnique(id [, EdgeType]):AEdge` — lookup by unique id; null if not found
