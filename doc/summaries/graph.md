# Graph Type and Operations

### Graph Manipulation
- `add(NodeType)`, `addCopy(node)`, `addClone(node)` -- create node
- `add(EdgeType, src, tgt)`, `addCopy(edge, src, tgt)`, `addClone(edge, src, tgt)` -- create edge
- `rem(nodeOrEdge)` -- remove (node removal cascades to edges)
- `retype(elem, Type)` -- retype (common attributes preserved)
- `merge(target, source)` -- merge source into target (edges redirected)
- `redirectSource(edge, newSrc)`, `redirectTarget(edge, newTgt)`, `redirectSourceAndTarget(edge, newSrc, newTgt)`
- `clear()` -- clear entire graph

### Neighbourhood Functions (set, count, boolean variants)
- Direct: `adjacent/adjacentIncoming/adjacentOutgoing(node [,EdgeType [,NodeType]])`
- Edges: `incident/incoming/outgoing(node [,EdgeType [,NodeType]])`
- Transitive: `reachable/reachableIncoming/reachableOutgoing(...)`, `reachableEdges/...`
- Bounded: `boundedReachable/...(node, depth [,EdgeType [,NodeType]])`
- `boundedReachableWithRemainingDepth(node, depth) : map<Node, int>` -- remaining depth per node
- Count: `countAdjacent/countIncident/countReachable/countBoundedReachable/...`
- Boolean: `isAdjacent/isIncident/isReachable/isBoundedReachable/...`

### Subgraph Operations
- `inducedSubgraph(set<Node>)`, `definedSubgraph(set<Edge>)` -- create subgraph
- `copy(graph)`, `insert(graph)` (move semantics), `insertCopy(graph, anchor)`, `insertInduced(set<Node>, anchor)`, `insertDefined(set<Edge>, anchor)`
- `equalsAny(graph, set<graph>)`, `equalsAnyStructurally(...)` -- isomorphy check
- `getEquivalent(graph, set<graph>)`, `getEquivalentStructurally(...)` -- find isomorphic
- `getEquivalentOrAdd(graph, array<graph>)` -- find or add (thread-safe)

### Visited Flags
- `elem.visited[flagId]` -- read (boolean)
- `elem.visited[flagId] = val` -- write
- `valloc()` -> int, `vfree(id)`, `vreset(id)`, `vfreenonreset(id)` (O(1) but dangerous)

### Graph Comparison
- `g1 == g2` -- isomorphic (with attributes)
- `g1 ~~ g2` -- structurally equal (ignoring attributes)
