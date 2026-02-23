# Indices

### Built-in Indices
- **Type indices**: nodes/edges by type (O(1) access, O(k) iteration)
- **Neighbourhood indices**: outgoing/incoming edge lists per node

### Attribute Index
- Model: `index IndexName { Type.Attribute }`
- Pattern: `n:N{attr==val}`, `n:N{ascending(attr>=lo, attr<hi)}`, `n:N{descending(attr>=lo)}`
- Functions: `nodesFromIndex*(idx, ...)`, `edgesFromIndex*(idx, ...)`, `countNodesFromIndex*(idx, ...)`, `isInNodesFromIndex*(...)`, `minNodeFromIndex(idx)`, `maxNodeFromIndex(idx)`, `indexSize(idx)`

### Incidence Count Index
- Model: `index Idx { countIncident(NodeType [, EdgeType [, NodeType]]) }` (also `countIncoming`, `countOutgoing`)
- `countFromIndex(idx, node)` -- query incidence count

### Name Index (with named graphs)
- Pattern: `n:N{@("name")}`, `n:N{@(stringExpr)}`
- `nameof(elem)`, `nodeByName(name)`, `edgeByName(name)`

### Uniqueness Index
- Model: `node edge unique;` + `index unique;`
- Pattern: `n:N{unique[intExpr]}`
- `uniqueof(elem)`, `nodeByUnique(id)`, `edgeByUnique(id)`

### Multiple Index Queries
- Pattern: `n:N{multiple(idx1>=lo1, idx1<=hi1, idx2>=lo2, idx2<=hi2)}`
- `nodesFromIndexMultipleFromTo(idx1, lo1, hi1, idx2, lo2, hi2, ...)`
