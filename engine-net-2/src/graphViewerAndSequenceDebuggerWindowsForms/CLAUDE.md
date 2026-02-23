# graphViewerAndSequenceDebuggerWindowsForms

Windows Forms UI layer for the GrGen.NET debugger using MSAGL graph visualization.

## Purpose

Provides the Windows Forms-based GUI for MSAGL-based interactive debugging. This DLL is loaded dynamically by the (G)GrShell when debug with MSAGL is configured and the debugger is opened (the default is debug with yComp, which requires only `graphViewerAndSequenceDebugger.dll`). Two different debugger windows (host forms) are offered, the used one depends on the configuration; plus an extra graph viewer window (host form) for the (host) graph.
(Integrates/synchronizes the user queries (and action/pattern matched events the debugger is based on) with the GUI message loop.)

## Output

- `graphViewerAndSequenceDebuggerWindowsForms.dll` - Library assembly
- `graphViewerAndSequenceDebuggerWindowsForms.xml` - XML documentation (Release builds)

## Dependencies

- `graphViewerAndSequenceDebugger` - Platform-independent debugger logic
- `lgspBackend` - Pattern matching backend
- `libConsoleAndOS` - Console abstraction
- `libConsoleAndOSWindowsForms` - GUI console adapters
- `libGr` - Core graph interfaces
- `Microsoft.Msagl.GraphViewerGDI` (NuGet 1.1.7) - Graph layout and rendering

## Key Files

### Forms

| File | Size | Purpose |
|------|------|---------|
| `GuiDebuggerHost.cs` | 24KB | Main debugger window |
| `GuiDebuggerHost.Designer.cs` | 41KB | Form designer generated code |
| `GuiDebuggerHost.resx` | 51KB | Form resources |
| `GuiConsoleDebuggerHost.cs` | 2KB | GUI console-based debugger host window (gui false) |
| `GuiConsoleDebuggerHost.Designer.cs` | 4KB | Designer generated code |
| `GuiConsoleDebuggerHost.resx` | 6KB | Form resources |

### Graph Visualization

| File | Purpose |
|------|---------|
| `MSAGLClient.cs` (24KB) | MSAGL graph rendering client |
| `BasicGraphViewerClientHost.cs` | Graph viewer panel host |
| `BasicGraphViewerClientCreator.cs` | Factory for creating graph viewers |

### Factory

| File | Purpose |
|------|---------|
| `HostCreator.cs` | Factory for creating GUI hosts |

### Resources

The `IconImages/` directory contains toolbar icons:
- `Abort.png`, `BackAbort.png` - Abort execution
- `Step.png`, `StepOut.png`, `StepUp.png`, `DetailedStep.png` - Stepping
- `Continue.png`, `ContinueDialog.png`, `Run.png` - Continue execution
- `NextMatch.png`, `SkipSingleMatches.png` - Match navigation
- `Breakpoints.png`, `ToggleBreakpoint.png` - Breakpoints
- `Choicepoints.png`, `ToggleChoicepoint.png` - Choicepoints
- `Watchpoints.png`, `AddWatchpoint.png`, `EditWatchpoint.png`, `DeleteWatchpoint.png`, `ToggleWatchpoint.png` - Watchpoints
- `Variables.png`, `Graph.png`, `Objects.png`, `Stacktrace.png`, `FullState.png`, `Highlight.png` - View options

## Key Classes

### GuiDebuggerHost

Main GUI debugger window (implements `IGuiDebuggerHost`), used when `debug set option gui true`:
- Two GUI consoles: one for the main work object (typically the sequence being debugged; switchable to graph form (real graphical display), in which case MSAGL renders it there), and one for input/output, and log printing
- Toolbar with debugging controls
- Variable inspection, breakpoint/watchpoint management
- Host graph display always shown in a separate window (`BasicGraphViewerClientHost`)

### GuiConsoleDebuggerHost

Console-based debugger host (implements `IGuiConsoleDebuggerHost`), used when debug with MSAGL and `debug set option gui false`:
- **Two-pane mode** (default, `debug set option twopane true`): two GUI consoles — one optional console for the main work object, one regular console for I/O and log printing
- **Single-pane mode** (`debug set option twopane false`): single GUI console for all output, similar to yComp mode but in a separate window rather than reusing the shell's own console

### MSAGLClient

MSAGL graph rendering client:
- Implements `IBasicGraphViewerClient`
- Maps GrGen graph elements to MSAGL graph objects for rendering
- Handles node/edge styling and layout
- Supports highlighting for matches

### BasicGraphViewerClientHost

Implements `IBasicGraphViewerClientHost`: the extra window in which the graph is rendered during MSAGL debugging (separate from the main debugger window with consoles).

### BasicGraphViewerClientCreator

Factory implementing `IBasicGraphViewerClientCreator`:
- Creates MSAGLClient instances
- Configures MSAGL graph viewer control

### HostCreator

Factory implementing `IHostCreator`:
- Creates GuiDebuggerHost or GuiConsoleDebuggerHost
- Used by shell to create appropriate host type

## MSAGL Integration

Uses Microsoft Automatic Graph Layout (MSAGL) for:
- Automatic graph layout algorithms
- Node and edge rendering
- Pan and zoom controls
- Selection and highlighting

## Platform Support

This library uses Windows Forms, which works on:
- **Windows**: Native .NET Framework
- **Linux**: Via Mono runtime (Windows Forms is supported by Mono)

