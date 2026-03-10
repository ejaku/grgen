# Computations (Attribute Evaluation Statements)

Computations = expressions (side-effect free, see typexpr.md) + statements (side-effect causing).
Abstractions: *functions* (expression drop-in, side-effect free) and *procedures* (statement drop-in, may modify graph).

### Assignments

```
elem.attr = expr;          -- attribute of matched/created graph element
::globalVar.attr = expr;   -- attribute of a global (graph-global) variable
localVar = expr;           -- local variable
::globalVar = expr;        -- global variable
container[i] = expr;       -- indexed position of a local container variable
elem.attrContainer[i] = expr; -- indexed position of a container attribute
elem.visited[flagIdx] = expr; -- visited flag of a graph element
```

Semantics: value semantics for all types (including containers and string); exception: `object` uses reference semantics.
Execution order: sequential, no dependency analysis — programmer is responsible for ordering.
Compound assignments are covered in the container and graph chapters.

### Local Variable Declarations

```
def var v:Type [= expr];       -- elementary (basic/value) variable
def ref v:Type [= expr];       -- container, internal class object, or transient class object variable
def n:NodeType [= expr];       -- node variable
def -e:EdgeType-> [= expr];    -- edge variable
```

Initializing expression is optional. Syntax mirrors the def pattern variables from LHS patterns.

### Control Flow

- `if(expr) { ... } [else if(expr) { ... }]* [else { ... }]` -- braces mandatory
- `switch(expr) { case constExpr { ... } ... [else { ... }] }` -- no fall-through; types: byte/short/int/long/string/boolean/enum
- `while(expr) { ... }`, `do { ... } while(expr)` -- braces mandatory; do-while has no trailing semicolon
- `for(v:int in [lower:upper]) { ... }` -- ascending if lower<=upper, else descending
- `for(v:Type in container) { ... }` -- iterate container values
- `for(k:KT -> v:VT in container) { ... }` -- iterate container key→value pairs (map) / index→value (array)
- `for(v:Type in graphQueryFunc(...)) { ... }` -- graph function iteration (adjacent, reachable, ...)
- `for(v:Type in indexQueryFunc(...)) { ... }` -- index function iteration
- `break;`, `continue;` -- must be inside a loop
- Note: can only iterate directly over a container *variable*, not a container-typed attribute (assign first)

### Embedded Exec

- `exec(sequence);` -- execute a graph rewrite sequence from within a procedure (not allowed in functions)
- Variables defined outside can be read; values can be assigned back via `yield`
- Enables mixing rule-based rewriting with traditional graph programming

### Function Definition and Call

```
function name(params) : ReturnType
{
    ...
    return(expr);   -- mandatory; return type must be compatible
}
```

Parameters: `n:NodeType`, `-e:EdgeType->`, `var v:VarType`, `ref c:ContainerOrObjectType`
Call (expression context): `name(arg, ...)` -- also callable from sequence computations
Functions may not call procedures (no externally visible side effects); local variable assignments are fine.

### Procedure Definition and Call

```
procedure name(params) [: (ReturnType, ...)]
{
    ...
    return [(expr, ...)];   -- mandatory; return types must be compatible; 0..k return values
}
```

Call (statement context): `[(out1, out2, ...) =] name(arg, ...);`
Output assignment is optional (call for side effects only). Assignment targets may be: def variables, global variables, graph element attributes, indexed targets, visited flags — including freshly declared def variables (`(def n:N) = proc(...)`).
Procedures may call other procedures and modify the graph.

### Built-in Functions (global, selected)

| Signature | Description |
|-----------|-------------|
| `nodes([NodeType]):set<Node>`, `edges([EdgeType]):set<Edge>` | all nodes/edges |
| `source(Edge):Node`, `target(Edge):Node`, `opposite(Edge,Node):Node` | edge endpoints |
| `adjacent(Node[,ET[,NT]]):set<Node>`, `incident(Node[,ET[,NT]]):set<Edge>` | neighbourhood |
| `adjacentIncoming/Outgoing`, `incoming/outgoing` | directed variants |
| `reachable(Node[,ET[,NT]]):set<Node>`, `reachableEdges(...):set<Edge>` | reachability |
| `reachableIncoming/Outgoing`, `reachableEdgesIncoming/Outgoing` | directed variants |
| `boundedReachable(Node,int[,ET[,NT]]):set<Node>` | depth-bounded reachability |
| `boundedReachableWithRemainingDepth(Node,int):map<Node,int>` | with remaining depth |
| `countNodes/countEdges([Type]):int` | counts |
| `countAdjacent/countIncident/countReachable/countBoundedReachable(...)` | count variants |
| `isAdjacent/isIncident/isReachable/isBoundedReachable(...)` | boolean check variants |
| `inducedSubgraph(set<Node>):graph`, `definedSubgraph(set<Edge>):graph` | subgraphs |
| `equalsAny/equalsAnyStructurally(graph,set<graph>):boolean` | graph equality |
| `getEquivalent/getEquivalentStructurally(graph,set<graph>):graph` | retrieve equivalent |
| `copy(graph):graph`, `clone/copy(match/container/Object/TransientObject)` | cloning |
| `nameof(Node/Edge/graph):string`, `uniqueof(Node/Edge/graph/Object):int/long` | identity |
| `nodeByName(string[,NT]):Node`, `edgeByName(string[,ET]):Edge` | lookup by name |
| `nodeByUnique(int[,NT]):Node`, `edgeByUnique(int[,ET]):Edge` | lookup by unique id |
| `graphof(Node/Edge):graph` | containing subgraph |
| `scan<T>(string):T`, `tryscan<T>(string):boolean` | parse string to value |
| `random():double`, `random(int):int` | random values |

Package functions: `Math::min/max/abs/ceil/floor/round/truncate/sqr/sqrt/pow/log/sgn/sin/cos/tan/arcsin/arccos/arctan/pi/e/byteMin..doubleMax`, `File::import(string):graph`, `File::exists(string):boolean`, `Time::now():long`

### Built-in Procedures (global, selected)

| Signature | Description |
|-----------|-------------|
| `add(NodeType):(Node)`, `addCopy(Node):(Node)` | create/copy node |
| `add(EdgeType,Node,Node):(Edge)`, `addCopy(Edge,Node,Node):(Edge)` | create/copy edge |
| `rem(Node/Edge)` | remove element |
| `retype(Node,NodeType):(Node)`, `retype(Edge,EdgeType):(Edge)` | change type |
| `clear()` | remove all elements |
| `merge(Node,Node)` | merge two nodes |
| `redirectSource/Target/SourceAndTarget(Edge,Node[,Node])` | redirect edge endpoints |
| `insert(graph)`, `insertCopy(graph,Node):(Node)` | insert (copy of) subgraph |
| `insertInduced(set<Node>,Node):(Node)`, `insertDefined(set<Edge>,Edge):(Edge)` | insert induced/defined |
| `valloc():(int)`, `vfree(int)`, `vreset(int)`, `vfreenonreset(int)` | visited flag management |
| `emit(string[,string]*)`, `emitdebug(string[,string]*)`, `record(string)` | output |
| `getEquivalentOrAdd/getEquivalentStructurallyOrAdd(graph,array<graph>):(graph)` | state space |
| `assert(boolean[,string[,object]*])`, `assertAlways(boolean[,string[,object]*])` | assertions |

Package procedures: `File::export(string)`, `File::export(graph,string)`, `File::delete(string)`,
`Transaction::start():(int)`, `Transaction::pause/resume/commit/rollback(int)`,
`Debug::add/rem/emit/halt/highlight(string[,object]*)`,
`Synchronization::enter/exit(object)`, `Synchronization::tryenter(object):(boolean)`
