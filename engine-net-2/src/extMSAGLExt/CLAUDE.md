# extMSAGLExt

External graph viewer application for GrGen.NET, built around the MSAGLExt graph viewer control, with vcg import and a live graph changes server mode.

## Purpose

Windows Forms application that displays graphs with the MSAGL graph layout library, in the manner of the (closed source, Java-based) yComp: i) it is a viewer for vcg files as dumped by GrGen.NET, ii) it can be remote controlled by the GrGen debugger over a socket, speaking the same text protocol as yComp (the yComp-compatible protocol implemented by `GraphViewerApplicationProxy`/`ExtMSAGLExtClient` on the sending side). Only what GrGen.NET writes/needs is supported, not the full vcg specification and not the full yComp functionality.
The graph view itself is the `MSAGLExtClient` control from `graphViewerAndSequenceDebuggerWindowsForms` (the same control that is used for the internal graph viewer of the GUI debugger), the application adds the import and the server mode around it.
The specifications are in `specifications/` (extMSAGLExt.txt, VcgImport.txt, LiveGraphChangesProtocol.txt).

## Output

- `extMSAGLExt.exe` - Windows Forms executable (WinExe), built into `engine-net-2/bin`

## Dependencies

- `graphViewerAndSequenceDebuggerWindowsForms` - the `MSAGLExtClient` control (and transitively `graphViewerAndSequenceDebugger` with `IBasicGraphViewerClient`, `libGr`, `libConsoleAndOS`)
- `Antlr3.Runtime.dll` (from `frontend/antlr-dotnet-csharpbootstrap-3.5.0.2`) - runtime of the generated vcg parser
- `Microsoft.Msagl.GraphViewerGDI` (NuGet)

## Key Files

| File | Purpose |
|------|---------|
| `Program.cs` | Entry point, command line argument handling |
| `MainForm.cs` / `MainForm.Designer.cs` | Main window: application states with status bar, menu, idle placeholder (drop target), startup of import or server mode, replacement of the graph viewer control upon import, watching/reloading of the shown file |
| `Vcg.g` | ANTLR 3.5 grammar of the vcg format; builds the AST and fills the definition table (the node/subgraph names) while parsing |
| `VcgParserEnvironment.cs` | Environment of the vcg parser: definition table `NodeOrSubgraphByName` (`env` in the grammar) and the helpers of the grammar actions filling it |
| `VcgLexer.cs`, `VcgParser.cs`, `Vcg.tokens` | Generated from `Vcg.g` by `genparser.sh`/`genparser.bat` (do not edit) |
| `VcgAst.cs` | AST classes (`Graph`, `Subgraph`, `Node`, `Edge`; attributes are kept as strings) in the namespace `VcgAst` |
| `VcgImporter.cs` | Second pass of the vcg import: constraint checking (incl. the known attributes) and building of the graph with calls to an `IBasicGraphViewerClient` |
| `GraphCommandReceiver.cs` | Network part of the server mode: single loopback connection, line-wise reception |
| `CommandLineParser.cs` | Tokenizer for a protocol command line, argument conversions, `ProtocolSyntaxException` |
| `CommandLineInterpreter.cs` | Executes a tokenized protocol command on the `IBasicGraphViewerClient` |
| `genparser.sh`, `genparser.bat` | Run ANTLR on `Vcg.g` (to be run in this folder) |

## Command Line

```
extMSAGLExt [-console] [[-autoreload] <vcg-file>]
extMSAGLExt [-console] -p <port>
```

- `<vcg-file>`: import and display the file (an error dialog is shown when it can't be read or has syntax errors)
- `-autoreload`: watch the file, reload it when it changed (only together with a file)
- `-p <port>`: server mode, listen on the loopback address (the port must be in the range 4242-4251; an error dialog is shown if it can't be opened)
- `-console`: show the error log console from the start
- no arguments: idle, use File > Open... or drop a .vcg file onto the placeholder

The File menu offers Open... (stops watching, closes a running debugging session, then imports the chosen vcg file), Reload file on changes (toggles watching, enabled only when a file is shown), Close (closes the shown file, back to idle), Show error log console, and Exit.
The error log console allocates a console for the stdout/stderr output of the application (`IWorkaround.AllocTheConsole`; a terminal emulator window under Linux, see `LinuxConsoleAllocator` in libConsoleAndOS). Errors and warnings are emitted to the console, only syntax errors of the vcg import and the fatal errors of the program start are shown as message box.

## Application States

`MainForm` keeps an explicit `ApplicationState`, from which the status bar text and the menu enabling are derived (`UpdateStateDisplay`); the action underway (opening/reopening a file) is shown at the right of the status bar while the UI thread is blocked by the import (`statusStrip.Refresh()`).
- `Idle`: no graph viewer, the centered placeholder label is shown instead (the only drop target for a single `.vcg` file)
- `ShowingFile`: with the substate watching (`watcher != null`) or not watching
- `ServerWaiting` -> `ServerConnected` (`GraphCommandReceiver.ClientConnected`) -> `ServerEnded` (after `RunAsync` returned because the connection broke, the graph stays open for inspection; when it returned because of the `exit` command of the client, the application is closed); the server mode can only be entered at startup, opening a file leaves it for good

Watching: a `FileSystemWatcher` (events are passed to the UI thread with `BeginInvoke`) (re)starts the `reloadTimer` (500ms), so the reload only happens after the file modifications ended. Upon the tick, a vanished file (deleted, moved away/renamed) switches to not watching; an `IOException` while reading is retried a few times. The reload builds the graph in a fresh `MSAGLExtClient` that replaces the shown one only on success, so the previous graph stays open in case of a syntax error (reported to the console, no message box). Only existing files can be watched.

## Vcg Import

Two passes:
1. The ANTLR parser (`Vcg.g`) builds the `VcgAst`. It over-accepts: any attribute name with any value is entered into the AST, as written (string literals with their quotes, so the kind of the value can be checked later). The only checks in the grammar actions are the ones needed for filling the definition table (with the help of the `VcgParserEnvironment`): a node or subgraph lacking the mandatory `title` attribute (must be a string literal) or with a duplicate title is reported by an error and skipped; otherwise it is entered into `NodeOrSubgraphByName`, so edges referencing nodes declared later in the file can be resolved without a third pass.
2. `VcgImporter` walks the AST: reports unknown attributes by a warning, skips edges lacking the mandatory `sourcename`/`targetname` (error), checks the attribute values (error and default value on violations), registers a node/edge realizer for each distinct style (`nr0`, `er0`, ...), adds the nodes and subgraphs (nesting by `MoveNode` after the children were added), applies the attributes contained in `info1` (nodes) or the label after `\nAttributes:` (edges) with `SetNodeAttribute`/`SetEdgeAttribute`, adds the edges (also the ones nested in subgraphs), and lays out the graph with the MSAGL layout algorithm given by the `layoutalgorithm` graph attribute (as written by the `VCGBaseDumper`; other algorithms, e.g. the `normal` of the yComp-targeted `VCGDumper`, cause a warning, the default `SugiyamaScheme` is used then).

Files exceeding 1GB are refused (`VcgImporter.CheckFileSize`, used by `Import` and the reload of a watched file; the file is read into memory as a whole).
`ImportFromText` is the testable entry point (syntax errors are returned, constraint violations are reported to the console and the import continues).
String literals contain newlines verbatim; quotation marks and backslashes are escaped with a backslash (as written by `VCGBaseDumper`, in contrast to the `VCGDumper` aiming at yComp with its `&quot;`).
Dumps of the `VCGBaseDumper` must be imported without warnings, dumps of the `VCGDumper` must not fail (but may cause warnings).
The parser regeneration needs Mono: `./genparser.sh` (Linux), `genparser.bat` (Windows).

## Server Mode (Live Graph Changes Protocol)

The protocol is the one used between GrGen and yComp: one command per line, `keyword "arg1" "arg2" ...`, quotes, backslashes and newlines in the arguments escaped by a backslash; the commands are a 1:1 mapping of the `IBasicGraphViewerClient` operations.
Two bugfix deviations from the yComp version: the escaping (yComp uses partly XML escaping), and all arguments are quoted (yComp expects the bool of `waitForElement` unquoted).
Three classes with distinct roles:
1. `GraphCommandReceiver` - `TcpListener` on the loopback address, accepts exactly one connection (and none after it ended), reads bytes (ASCII) from the `NetworkStream` and splits them into lines (limit 64MB per line, the connection is closed when exceeded)
2. `CommandLineParser` - tokenizer (a single, strict regular expression matching the whole line, plus a scan and unescape loop for the arguments) that splits a line into the keyword and the unescaped arguments, plus helpers converting arguments (colors, shapes, line styles, integers, attribute keys)
3. `CommandLineInterpreter` - calls the `MSAGLExtClient` according to the command, writes the responses (e.g. to `sync` or `getLayoutOptions`) back to the stream, implements `waitForElement` by polling `CommandAvailable`

Threading: there is no background thread. `MainForm_Load` (`async void`) awaits the receiver on the UI thread, so the WinForms synchronization context is captured and all continuations run on the UI thread - no `Invoke` is needed, `ConfigureAwait(false)` and `Task.Run` must NOT be used in the receiving chain.
Error handling: let-it-crash. Syntax errors in the protocol (unknown command, wrong number of arguments, unknown color name, bad escape sequence, ...) throw a `ProtocolSyntaxException`, semantic errors (e.g. an unknown element or realizer) are reported by exceptions of the `MSAGLExtClient` - both are not caught, so the crash dialog appears (the errors are symptoms of bugs in the sending client and must not go undetected in a debugger).
The interpreter does not keep any state to detect semantic errors.

## Notes

- Importing a vcg file builds the graph in a fresh `MSAGLExtClient` that replaces the graph viewer control (a cleared one would still contain the registered realizers, which can't be registered twice) and stops the receiver, which closes a connected debugger.
- The graph viewer client interface is `IBasicGraphViewerClient` (graphViewerAndSequenceDebugger); the proxies on the sending side are `YCompClient` and `ExtMSAGLExtClient` in `GraphViewerApplicationProxy.cs`, which also contain the encoding of the strings.
- The graph element names written by the dumpers are `n<hash>` for nodes, edges get generated names (`e<count>`) in the importer.
- Unit tests: `engine-net-2/unittests/extMSAGLExtTests` (NUnit): tokenizer, interpreter with a recording `IBasicGraphViewerClient` mock, vcg import with example and bogus files (including `examples/JavaProgramGraphs-GraBaTs08/MoveMethodsExampleGraph.vcg`). The network and GUI parts are not unit tested.

## Platform Support

Windows Forms, works on:
- **Windows**: Native .NET Framework
- **Linux**: Via Mono runtime

## Running

**Windows:**
```
extMSAGLExt.exe [-console] [[-autoreload] file.vcg | -p 4242]
```

**Linux:**
```bash
mono extMSAGLExt.exe [-console] [[-autoreload] file.vcg | -p 4242]
```

Normally it is started by the GrGen debugger (`debug with extMSAGLExt` for configuration followed by the real `debug` command), which picks a free port from the range 4242-4251 and connects to it. Or from the GrShell `show graph extMSAGLExt`.
