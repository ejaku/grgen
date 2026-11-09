# graphViewerAndSequenceDebugger

Core (sequence) debugger engine and graph visualization interface/abstraction/helper code for GrGen.NET (platform-independent).

## Purpose

Implements debugger functionality independent of any UI framework, plus the text-only interface with the Printers. Provides step-by-step execution, breakpoints, watchpoints, and variable inspection (based on graph change and action/pattern matched events).
Graph visualization uses one of four viewers:
- yComp, an external (Java) application communicated with directly from this library over a loopback TCP socket;
- extMSAGLExt, an external (.NET) application (see `src/extMSAGLExt`) communicated with in the same way (it wraps the MSAGLExt viewer control in an own application, also able to display .vcg files);
- MSAGL, an internal viewer based on the MSAGL .NET library that requires loading `graphViewerAndSequenceDebuggerWindowsForms.dll`;
- MSAGLExt, the extended internal MSAGL viewer (same DLL) that adds a left pane with map overview, search, node nesting tree, and attributes pane.

The options are represented by the `GraphViewerTypes` enum (`Undefined`, `YComp`, `MSAGL`, `MSAGLExt`, `ExtMSAGLExt`) defined in `GraphViewerBaseClient.cs` (`Undefined` = not configured yet, the shell warns and defaults to yComp).
This library also contains code to render sequences as graphs, against the base graph visualization interface, but this is only used with the internal MSAGL/MSAGLExt graph viewers (rendered to the main work object view of the GuiDebuggerHost from `graphViewerAndSequenceDebuggerWindowsForms.dll`).
When one of the external applications yComp or extMSAGLExt is used, sequence output is printed to the main console of the shell (the regular console in GrShell, the GUI console in GGrShell).
Graph dumps (.vcg) written for/shown with a viewer depend on the configured viewer: the `VCGDumper` for yComp, the `VCGBaseDumper` for extMSAGLExt (both from libGr).
The graph visualization and the debugger can be used also from API level (without libGrShell or derived applications; the same holds for the WindowsForms extension).

## Output

- `graphViewerAndSequenceDebugger.dll` - Library assembly
- `graphViewerAndSequenceDebugger.xml` - XML documentation (Release builds)

## Dependencies

- `libConsoleAndOS` - Console abstraction
- `libGr` - Core graph interfaces
- `lgspBackend` - Pattern matching backend

## Key Files

### Debugger Core

| File | Size | Purpose |
|------|------|---------|
| `Debugger.cs` | 112KB | Main debugger engine (implements `IUserProxyForSequenceExecution`): manages `DebuggerTask` stack for parallel execution, drives step-by-step execution via user query loop (`QueryUser`), handles choicepoints (direction/sequence/match selection), controls step/detailed/record modes, coordinates graph viewer updates (which is contained in the GraphViewerClient graphViewerClient member) and sequence display |
| `DebuggerEnvironment.cs` | 31KB | `IDebuggerEnvironment` interface + implementation: UI abstraction for debugger interaction (key/choice input, pause dialogs, element lookup, graph viewer launch, TwoPane/Gui flags), wiring together I/O console and data rendering console/GUI |
| `DebuggerGraphProcessingEnvironment.cs` | 4KB | Bundles procEnv with debugger configuration: `DumpInfo`, subrule debug config, VCG flags, subgraph map, object namers/indexers; converts unnamed graphs to named (required by debugger) |
| `DebuggerTask.cs` | 13KB | Stores procEnv, sequence call stack, entered subrule computations (some kind of per-thread-state for sequences executed in parallel) |
| `SubruleDebugging.cs` | 20KB | Subrule-specific debugging features |

### Sequence Display

| File | Size | Purpose |
|------|------|---------|
| `SequencePrinter.cs` | 70KB | Sequence step printing (text output) |
| `SequenceComputationPrinter.cs` | 17KB | Sequence computation printing |
| `SequenceExpressionPrinter.cs` | 52KB | Sequence expression printing |
| `SequenceRenderer.cs` | 85KB | Sequence step rendering (visual) |
| `SequenceComputationRenderer.cs` | 37KB | Sequence computation rendering |
| `SequenceExpressionRenderer.cs` | 120KB | Sequence expression rendering |
| `DisplaySequenceContext.cs` | 2KB | Context for sequence display |

### Graph Visualization

| File | Size | Purpose |
|------|------|---------|
| `GraphViewer.cs` | 14KB | High-level helper: manages `GraphViewerClient` lifecycle (`ShowGraph`/`EndShowGraph`), registers/unregisters graph change events (add/remove/retype/attribute), static `DumpAndShowGraph` utility |
| `GraphViewerBaseClient.cs` | 8KB | `GraphViewerTypes` enum; base class creating the basic graph viewer client: starts yComp/extMSAGLExt and connects the proxy to it, or creates the internal MSAGL/MSAGLExt client (via the dynamically loaded Windows Forms DLL) |
| `GraphViewerClient.cs` | 35KB | Core graph viewer protocol layer (extends `GraphViewerBaseClient`): uses the yComp/extMSAGLExt proxy or the internal MSAGL/MSAGLExt client (which is contained in the IBasicGraphViewerClient basicClient member), manages presentation state (dirty flags, hidden edges, included/excluded elements, realizer overrides), handles `DumpInfo` appearance change events |
| `ElementRealizers.cs` | 14KB | Defines `NodeRealizer`/`EdgeRealizer` visual styles (color, shape/line, text color) and `ElementMode` (normal, matched, created, deleted, retyped, redirected); manages the per-mode realizer sets registered with the graph viewer |
| `GraphAnnotationAndChangesRecorder.cs` | 9KB | Records changes for highlighting |
| `MatchMarkerAndAnnotator.cs` | 12KB | Match visualization and annotation |
| `GraphViewerApplicationProxy.cs` | 26KB | External graph viewer applications: starters (`YCompStarter`, `ExtMSAGLExtStarter`, free port search in 4242-4251), socket stream, and the proxies `YCompClient`/`ExtMSAGLExtClient` (common base `GraphViewerApplicationProxy` implementing `IBasicGraphViewerClient` by sending live graph changes protocol commands) |

### Output and Highlighting

| File | Size | Purpose |
|------|------|---------|
| `Displayer.cs` | 7KB | Display abstraction |
| `Printer.cs` | 9KB | Text output utilities |
| `Renderer.cs` | 15KB | Visual rendering utilities |
| `Highlighter.cs` | 13KB | Syntax highlighting and selection |

### User Interaction

| File | Size | Purpose |
|------|------|---------|
| `BreakpointAndChoicepointEditor.cs` | 11KB | Breakpoint management UI |
| `WatchpointEditor.cs` | 27KB | Watchpoint management UI |
| `UserChoiceMenu.cs` | 14KB | User input handling |
| `UserProxyChoiceMenu.cs` | 15KB | Proxy for user choices during execution |
| `VariableOrAttributeAccessParserAndValueFetcher.cs` | 7KB | Variable inspection |

### Parser (ConstantParser)

Parses constant values in debugger expressions:
- `ConstantParser.csc` - Grammar (CSharpCC input)
- `ConstantParser.cs` - Generated parser (do not edit)
- `ConstantParserConstants.cs`, `ConstantParserTokenManager.cs` - Lexer
- `ConstantParserHelper.cs` - Parser utilities

**Regenerate with:** `./genparser.sh` (Linux) or `genparser.bat` (Windows)

### Interfaces

| File | Purpose |
|------|---------|
| `IBasicGraphViewerClient.cs` | Graph viewer client abstraction (the interface to yComp, extMSAGLExt, MSAGL, and MSAGLExt) |
| `IBasicGraphViewerClientCreator.cs` | Factory for graph viewer clients |
| `IBasicGraphViewerClientHost.cs` | Host for graph viewer |
| `IDisplayer.cs` | Display interface |
| `ISequenceDisplayer.cs` | Sequence display interface |
| `IGuiDebuggerHost.cs` | GUI debugger host interface (one text console plus one graph renderer or text console) |
| `IGuiConsoleDebuggerHost.cs` | GUI console debugger host interface (one or two text consoles) |
| `IHostCreator.cs` | Factory for creating hosts |

## Key Classes

### Debugger

Main debugger engine:
- Step-by-step sequence execution
- Breakpoints on rules and sequences
- Choicepoints for match selection
- Watchpoints on graph changes
- Stack trace and variable inspection

### DebuggerEnvironment

Implements `IDebuggerEnvironment`: UI abstraction between the debugger engine and the actual console/GUI. Wires together the I/O console and the data rendering console/GUI. Provides key and choice input, pause dialogs, element lookup, graph viewer launch, and exposes `TwoPane`/`Gui` mode flags.

### GraphViewer

Graph visualization manager:
- Coordinates with graph viewer client
- Handles element highlighting
- Manages layout updates

### GraphViewerApplicationProxy (YCompClient, ExtMSAGLExtClient)

Integration with the external graph viewer applications yComp and extMSAGLExt:
- Starts the application as local server on a free port, then connects to it
- Sends graph updates as commands of the live graph changes protocol (see `specifications/LiveGraphChangesProtocol.txt`)
- Receives elements selected by the user when they are awaited (`ElementAvailable`/`GetAwaitedElement`), and notices a lost connection
- The subclasses differ in the application-specific details (e.g. extMSAGLExt expects quoted arguments and MSAGL layout names)

## Debugger Features

- **Breakpoints**: Stop at specific rules or sequences
- **Choicepoints**: Pause to let user select among matches
- **Watchpoints**: Trigger on graph element changes (data breakpoints)
- **Stepping**: Step into, step over, step out
- **Variable inspection**: View and modify variables
- **Stack trace**: View call stack of nested sequences
- **Match highlighting**: Visual indication of pattern matches
