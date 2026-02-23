# Techniques and Design Patterns

### Merge and Split Nodes
- **Merge** (manual): use an `iterated` (or `exec` + all-bracketed rule for multigraphs) to copy edges from source to target with `copy<edge>`, then delete the source node; allows full control over edge type, direction, and retyping
- **Split** (manual): create a new node with `copy<node>`, transfer selected edges using `iterated`/`exec`, delete the transferred edges from the original
- For simple merging without edge retyping, the `retype'n'merge` clause (added later) provides a much more concise alternative
- Example: T1-T2 graph reduction — T1 removes self-loops, T2 merges a node into its unique predecessor, T3 removes resulting multi-edges

### Node Replacement Grammars (edNCE)
- Encode context-free node replacement grammars in GrGen
- Nonterminal nodes replaced by subgraphs with edge embedding rules
- Connection instructions specified via edge redirections in the replacement

### Mapping in a Rewriting Tool
- **Traceability**: link source and target elements during transformation
- Use storage maps (`map<Node,Node>`) to track correspondences
- **Partitioning**: assign elements to groups, process per partition
- Two-pass: first create target structure, then fill in details

### Comparing Structures
- Three steps: (1) collect nodes of interest into a `set<Node>` via iterated subpatterns traversing a spanning tree from a root node, (2) compute an induced subgraph with `inducedSubgraph(nodes)`, (3) compare with `==`/`!=` graph comparison operators
- Cache the induced subgraph in a `graph`-typed attribute on the anchor node if comparisons are repeated
- Use `equalsAny`/`getEquivalent` for parallelizable comparison against a set of known subgraphs

### Copying Structures
- Two-pass copy: (1) copy nodes via iterated subpatterns traversing a spanning tree from a root node, filling a storage map old→new; (2) copy edges using the map (via deferred `exec`)
- `copy<node>` / `copy<edge>` for element-level cloning preserving type and attributes
- Storage map (traceability map) maintains old→new correspondence for edge reconnection

### Data Flow Analysis for Reachability
- Worklist-based iterative data flow analysis encoded as graph rewriting
- Initialize all nodes, propagate values along edges until fixpoint
- Use `set<Node>` attributes as data flow sets, `[rule]` for bulk updates

### State Space Enumeration
- **Modelling**: host graph virtually partitioned via `Graph` anchor nodes + `contains` edges to subgraph nodes; edges between `Graph` nodes encode state successorship
- **Backtracking angles** `<<s>>`: recursively enumerate all rule matches, rolling back graph changes between tries
- **Pause insertions**: protect subgraph insertions from rollback — changes made while backtracking supervision is paused are kept permanently (used to write found states into the host graph)
- **Recursive sequences**: dynamically nest backtracking angles to enumerate the full state space with a sequence implementing just one step
- **Subgraph copying** (`insertInduced`, `adjacent`): copy current state into host graph as a new anchor subgraph
- **Isomorphy pruning**: store induced subgraph in a `graph`-typed anchor attribute; use `equalsAny`/`getEquivalent` for parallelizable comparison; `auto` filters eliminate symmetric matches upfront (more efficient than post-hoc comparison)
- **Alternative**: mapping clause approach (`tests/statespaceMapping`) — maps original graph to result graphs without in-place rollback; supports breadth-first (parallelized) or depth-first enumeration
