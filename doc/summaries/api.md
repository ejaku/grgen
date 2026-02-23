# C# API Reference

### Graph Interface
- `IGraph` -- base graph interface; `INamedGraph` -- adds persistent naming
- `INode` / `IEdge` -- graph element interfaces with typed variants (`INode_Foo`)
- Generated classes: `FooGraph`, `FooNamedGraph` (from model), `FooActions` (from rules)
- Create elements: `graph.AddNode(nodeType)`, `graph.AddEdge(edgeType, src, tgt)`
- Access: `graph.GetCompatibleNodes(type)`, `graph.GetCompatibleEdges(type)`
- Named access: `namedGraph.GetNode("name")`, `@("name")` in scripts

### Rule Interface
- `IAction_RuleName` -- generated per rule
- `action.Match(procEnv, maxMatches, params)` -- find matches
- `action.Modify(procEnv, match)` -- apply rewrite to a match
- `action.Apply(procEnv, params)` -- match + modify in one call
- `action.ApplyAll(maxMatches, procEnv, params)` -- apply to all matches
- Match objects: `IMatch_RuleName` with typed accessors for matched elements

### Graph Processing Environment
- `LGSPGraphProcessingEnvironment` -- main runtime context
- `procEnv.ApplyGraphRewriteSequence(seq)` -- execute parsed sequence
- `SequenceParser.ParseSequence(str, ...)` -- parse sequence from string
- Transaction support: `procEnv.TransactionManager.Start()` / `.Commit()` / `.Rollback()`

### Graph I/O
- `Porter.Export(graph, filename)` -- export to `.grs` format
- `Porter.Import(filename, backend, model)` -- import from `.grs`
- GXL and XMI import/export also available

### External Extensions (from API side)
- Override `*ExternalFunctions.cs` partial class methods
- `IExternalObject` -- interface for external attribute types
- External emit/parse (`external emit class;` in model): GrGen generates `*ModelExternalFunctions.cs` with methods to implement in `*ModelExternalFunctionsImpl.cs`:
  - `Parse(TextReader, AttributeType, IGraph)` -- deserialize external type from .grs import
  - `Serialize(object, AttributeType, IGraph)` -- serialize external type for .grs export (must be parseable by `Parse`)
  - `Emit(object, AttributeType, IGraph)` -- human-readable string for debugger display and emit writing
  - `External(string line, IGraph)` -- called when `external CommandLine` is seen by shell/importer; used for fine-grain replay of external attribute changes (recorded via `IRecorder.External()`)

### Graph Events
- `graph.OnNodeAdded`, `OnEdgeAdded`, `OnRemovingNode`, `OnRemovingEdge`
- `graph.OnRetypingNode`, `OnRetypingEdge`
- `graph.OnChangingNodeAttribute`, `OnChangingEdgeAttribute`
- Action events: `actions.OnMatched`, `actions.OnFinished`, `actions.OnRewritingNextMatch`

### Debugger API
- `DebuggerGraphProcessingEnvironment` -- wraps procEnv with debugging
- Supports programmatic stepping, breakpoints, graph visualization
