# Graph Type and Operations

### Graph Manipulation
- `add(NodeType)`, `addCopy(node)`, `addClone(node)` -- create node
- `add(EdgeType, src, tgt)`, `addCopy(edge, src, tgt)`, `addClone(edge, src, tgt)` -- create edge
- `rem(nodeOrEdge)` -- remove (node removal cascades to edges)
- `retype(elem, Type)` -- retype (common attributes preserved)
- `merge(target, source)` -- merge source into target (edges redirected)
- `redirectSource(edge, newSrc)`, `redirectTarget(edge, newTgt)`, `redirectSourceAndTarget(edge, newSrc, newTgt)`
- Note: on unnamed graphs, supply extra string arg(s) for the name of the old element
- `clear()` -- clear entire graph

### Graph Query by Types
- `nodes(NodeType)` / `nodes()` -- returns `set<Node>` (always `set<Node>`, not `set<N>` even if N specified)
- `edges(EdgeType)` / `edges()` -- returns `set<Edge>`, `set<UEdge>`, or `set<AEdge>` depending on edge kind
- For-loop form (no set built): `for(n:N in nodes(N)) {...}`, `for(e:E in edges(E)) {...}`

### Neighbourhood Functions
Each available as: set function `f(node)`, for-loop `for(x:T in f(node)) {...}`, count `countF(node)`, boolean predicate `isF(node, target)`
- Direct edges: `incident/incoming/outgoing(node [,EdgeType [,NodeType]])`
- Direct nodes: `adjacent/adjacentIncoming/adjacentOutgoing(node [,EdgeType [,NodeType]])`
- Transitive nodes: `reachable/reachableIncoming/reachableOutgoing(node [,EdgeType [,NodeType]])`
- Transitive edges: `reachableEdges/reachableEdgesIncoming/reachableEdgesOutgoing(...)`
- Bounded: `boundedReachable/...(node, depth [,EdgeType [,NodeType]])`
- `boundedReachableWithRemainingDepth/Incoming/Outgoing(node, depth) : map<Node, int>` -- remaining depth per node (no for-loop/count/boolean variants)
- Note: for-loop may visit reflexive/multi-edges multiple times; set form removes duplicates

### Subgraph Operations
- `inducedSubgraph(set<Node>)`, `definedSubgraph(set<Edge>)` -- compute subgraph (function)
- `copy(graph)` -- clone subgraph (function)
- `insert(graph)` -- insert into host graph, move semantics (procedure)
- `insertCopy(graph, anchor)` -- insert copy, returns anchor clone (procedure)
- `insertInduced(set<Node>, anchor)`, `insertDefined(set<Edge>, anchor)` -- compute+insert clone, returns anchor clone (procedure)
- `equalsAny(graph, set<graph>)`, `equalsAnyStructurally(...)` -- isomorphy check (parallelizable)
- `getEquivalent(graph, set<graph>)`, `getEquivalentStructurally(...)` -- find isomorphic graph
- `getEquivalentOrAdd(graph, array<graph>)`, `getEquivalentStructurallyOrAdd(...)` -- find or add (thread-safe)
- `this` -- currently processed graph (host graph by default; bound to subgraph when processing relocated)

### File Operations
- `File::import(path)` -- load subgraph from .grs file, returns `graph` (must be compatible with current model)
- `File::exists(path)` -- check file existence, returns `boolean`
- `File::export(path)` -- export host graph to file (procedure)
- `File::export(subgraph, path)` -- export specific subgraph to file (procedure)
- `File::delete(path)` -- delete file (procedure)

### Visited Flags
- `elem.visited[flagId]` -- read (boolean); `elem.visited` without index uses flag 0
- `elem.visited[flagId] = val` -- write
- `valloc()` -> int (allocate), `vfree(id)` (free + reset), `vreset(id)` (reset all elements), `vfreenonreset(id)` (free without reset, O(1) but dangerous)

### Graph Comparison
- `g1 == g2` -- isomorphic (with attributes); `g1 != g2` -- not isomorphic
- `g1 ~~ g2` -- structurally equal (ignoring attributes)
- `canonize(g:graph):string` -- canonical string (SMILES-based equitable partition; not full canonization)
- Note: `==` is expensive; early-out on node/edge counts per type, then V-structures, then full matching

### Transaction Handling
- `Transaction::start()` -> int (tx id) -- begin transaction; nestable
- `Transaction::pause()` / `Transaction::resume()` -- suspend/resume undo logging
- `Transaction::commit(id)` -- keep changes, discard undo log
- `Transaction::rollback(id)` -- revert changes via undo log

### Assertions
- `assert(cond [, msg, ...])` -- fires when false and assertions are enabled (rule language: default file/line message if no msg given)
- `assertAlways(cond [, msg, ...])` -- always fires when false
- Firing aborts execution by default; in debugger: abort / continue / external debug

### Output Procedures
- `emit(str, ...)` -- write to stdout or redirected file
- `emitdebug(str, ...)` -- always write to stdout (ignores redirect)
- `record(str)` -- write label to ongoing graph change recording (for replay)
