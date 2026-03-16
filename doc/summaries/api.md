# C# API Reference

### Graph and Model Interface
- `IGraph` -- base graph interface; `INamedGraph` -- adds persistent naming (~2x memory of unnamed graph)
- `INode` / `IEdge` -- graph element interfaces; also `IDEdge` (directed), `IUEdge` (undirected)
- Generated from `Foo.grg`: `FooGraph` (extends `LGSPGraph`) / `FooNamedGraph` (extends `LGSPNamedGraph`) in `FooModel.cs`; `FooActions` in `FooActions.cs`
- Generated typed interfaces: `IBar` per node/edge class `Bar`, inheriting as declared in model
- Exact (type-safe) interface: `graph.CreateNodeBar()` returns `IBar`
- Generic interface: `model.GetType("Bar")` → `NodeType`; `graph.AddNode(nodeType)` → `INode`
- Access: `graph.GetCompatibleNodes(type)`, `graph.GetCompatibleEdges(type)`
- Named access: `namedGraph.GetNode("name")`, `@("name")` in scripts
- Runtime matcher regeneration: `graph.Custom(...)` (graph commands), `actions.Custom(...)` (action commands) -- same parameters as GrShell `custom` commands

### Actions/Rule Interface
- `FooActions` has one typed member per rule: `actions.bar` is of type `IAction_bar` (exact interface); alternatively `Action_bar.Instance`
- Generic interface: `actions.GetAction("bar")` → `IAction`
- `action.Match(graph, maxMatches, params)` -- returns `IMatchesExact<Rule_bar.IMatch_bar>` extending the generic IMatches interface
- `action.Modify(graph, match)` -- apply rewrite to a specific match (with typed out-params)
- `action.Apply(graph, params)` -- match + modify in one call (with typed in/ref-params)
- Note: C# requires `out` parameter variables to be of the exact declared type (a supertype is not allowed)
- Match objects: `IMatch_bar` with typed members per matched node/edge/variable (prefixed `node_`/`edge_`) extending the generic IMatch interface
- the generic predefined-classes and object/string based interface is used by the GrShell (entry point is the IBackend interface), the generated interface offering exact types can be used in model/actions-specific user applications (offering higher development convenience at the price of being bound)

### Example of exactly typed interface usage
```csharp
// exact (type-safe) interface example (Foo.grg defines rule bar(Bar x):(Bar))
FooGraph graph = new FooGraph(new LGSPGlobalVariables());
FooActions actions = new FooActions(graph);
Bar b = graph.CreateNodeBar();
actions.bar.Apply(graph, b, ref b);  // typed in/out params
// or: match first, inspect, then rewrite
IAction_bar bar = Action_bar.Instance;
IMatchesExact<Rule_bar.IMatch_bar> matches = bar.Match(graph, 0, b);
bar.Modify(graph, matches.First);    // matches.First.node_x etc. for typed access
```

### Graph Processing Environment
- `IGraphProcessingEnvironment` implemented by `LGSPGraphProcessingEnvironment(graph, actions)`
- `procEnv.ApplyGraphRewriteSequence(seqString)` -- parse and execute sequence
- `SequenceParser.ParseSequence(str, ...)` -- parse sequence from string for repeated execution
- Transaction manager: `procEnv.TransactionManager` implements `ITransactionManager`:
  - `Start()` → transactionID; nested transactions supported
  - `Pause()` / `Resume()` -- temporarily stop recording undo log
  - `Commit(id)` / `Rollback(id)` -- finalize or undo transaction
  - `ExternalTypeChanged(IUndoItem)` -- register external attribute change for rollback
- `procEnv.PersistenceProviderTransactionManager` -- database transaction access (when using persistent graph)

### Graph I/O
- `Porter.Export(graph, ...)` / `Porter.Import(...)` -- export/import graph; format determined by file extension
- Supported formats: GRS/GRSI (recommended, expects `.gm` model), GXL, ECORE/XMI (`.ecore` + optional `.xmi`/`.grg`), GRG (export only)
- Named graph memory note: cast to `LGSPNamedGraph`, copy-construct `LGSPGraph`, then drop named graph reference to save memory

### Graph Viewer and Debugger
- `GraphViewer` class (from `graphViewerAndSequenceDebugger.dll`):
  - `ShowGraph` / `EndShowGraph` / `UpdateDisplayAndSync` -- live graph display adapting to changes
  - `DumpAndShowGraph` -- dump to `.vcg` file and render
- `Debugger` class allows to execute sequence under debugger control
  - Requires `DebuggerEnvironment` and `DebuggerGraphProcessingEnvironment` configuration
  - `GraphViewerTypes.YComp` (stdout console and external app) or `GraphViewerTypes.MSAGL` (WindowsForms, internal)
- `libGrShell`: include GrShell functionality (execute `.grs` scripts) in own application
- Examples: `examples-api/YCompExample`, `examples-api/DebuggerExample`, `examples-api/ShellExample`, `examples-api/ApplicationExample`

### Persistent Graph
- Create: `LGSPPersistentNamedGraph(persistenceProviderDllName, connectionParameters, ...)`
- Only supported provider: `libGrPersistenceProviderSQLite.dll`
- Thin extension of `LGSPNamedGraph`; custom persistence providers can implement `IPersistenceProvider`
- `procEnv.PersistenceProviderTransactionManager` -- access to `IPersistenceProviderTransactionManager` for database transactions

### External Extensions (Implementation Side)
- General scheme for model and actions extensions: GrGen generates partial classes in `*ExternalFunctions.cs` (do not edit), the user has to implement them accordingly in `*ExternalFunctionsImpl.cs` (in another part of the partial classes)
- For model `Foo` with external classes/functions/procedures: GrGen generates `FooModelExternalFunctions.cs`; implement in `FooModelExternalFunctionsImpl.cs` (see `examples-api/ExternalAttributeEvaluationExample`):
  - Partial classes for each external class (in model namespace, with declared function and procedure method signatures)
  - `ExternalFunctions` partial class with declared function signatures (in `de.unika.ipd.grGen.expression` namespace)
  - `ExternalProcedures` partial class with declared procedure signatures
- For actions `Bar` with external filters/sequences: GrGen generates `BarActionsExternalFunctions.cs`; implement in `BarActionsExternalFunctionsImpl.cs` (see `examples-api/ExternalFiltersAndSequencesExample`):
  - `MatchFilters` partial class with filter functions (convert matches to `IList` for reordering)
  - `Sequence_foo` partial class with `ApplyXGRS_foo` function per external sequence
- `external emit class;` generates methods to implement in `FooModelExternalFunctionsImpl.cs`:
  - `Parse(TextReader, AttributeType, IGraph)` -- deserialize from .grs import
  - `Serialize(object, AttributeType, IGraph)` -- serialize for .grs export (parseable by `Parse`)
  - `Emit(object, AttributeType, IGraph)` -- human-readable string for debugger display and emit
  - `External(string line, IGraph)` -- called when `external CommandLine` seen; fine-grain replay of external attribute changes (recorded via `IRecorder.External()`)
- `external emit graph class;` additionally generates:
  - `AsGraph(object, AttributeType, IGraph)` -- return `INamedGraph` for visual debugger inspection of external type
- `external copy class;` / `external ~~ class;` / `external < class;` generates `AttributeTypeObjectCopierComparer` partial class in `FooModelExternalFunctions.cs`; implement in `FooModelExternalFunctionsImpl.cs`:
  - `Copy(object, IGraph, IDictionary<object,object>)` -- `null` dict = clone (top-level only), non-null = deep copy; pull from/add to dict to handle shared refs and cycles
  - `IsEqual(object, object, IDictionary<object,object>)` -- for `~~` and graph isomorphy; insert into visitedObjectsMap on entry, remove on exit to detect cycles
  - `IsLower(object, object, IDictionary<object,object>)` -- for relational ordering; requires `~~ class` to be specified first

### Graph Events
The debugger, graph viewers, transaction handler, graph change recorder, and persistent graph all implement their functionality by listening to these events.
Fired automatically on structural changes; fire attribute change events yourself when modifying attributes via API — otherwise changes would go unnoticed:
- `OnNodeAdded`, `OnEdgeAdded`, `OnObjectCreated` (after creation)
- `OnRemovingNode`, `OnRemovingEdge`, `OnRemovingEdges` (all edges of a node), `OnClearingGraph`
- `OnRetypingNode`, `OnRetypingEdge`, `OnRedirectingEdge`
- `OnChangingNodeAttribute`, `OnChangingEdgeAttribute`, `OnChangingObjectAttribute` (before change; esp. used for single element change notification for container types)
- `OnChangedNodeAttribute`, `OnChangedEdgeAttribute` (after change; used by debugger watchpoints)
- `OnSettingAddedNodeNames`, `OnSettingAddedEdgeNames` (before rewrite step; names of elements about to be added)
- `OnVisitedAlloc`, `OnVisitedFree`, `OnSettingVisited`

### Action Events
Bracketing structure: outer events once per rule application construct, inner events per match:
- Outer opening: `OnBeginExecution`
- `OnMatchedBefore` (all matches found, before filtering -- full lookahead on all matches)
- `OnMatchedAfter` (after filtering -- matches that will actually be applied)
- Inner per match: `OnMatchSelected`, `OnRewritingSelectedMatch`, `OnSelectedMatchRewritten`, `OnFinishedSelectedMatch`
- Outer closing: `OnFinished`, `OnEndExecution`
- Debug/subrule events: `OnDebugEnter`, `OnDebugExit`, `OnDebugEmit`, `OnDebugHalt`, `OnDebugHighlight`
- Subgraph events: `OnSwitchingToSubgraph`, `OnReturnedFromSubgraph`
- Sequence events: `OnEntereringSequence`, `OnExitingSequence`, `OnEndOfIteration`
- Parallel events: `OnSpawnSequences`, `OnJoinSequences`
