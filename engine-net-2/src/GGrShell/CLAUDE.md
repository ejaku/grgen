# GGrShell

GUI shell for GrGen.NET with a Windows Forms interface employing a GUI console control.

## Purpose

Windows Forms version of GrShell. Offers the same graph manipulation and rule/sequence execution features, but not usable for automatized testing with the bash test framework (due to its GUI nature). Both yComp and MSAGL graph viewers are available; when using MSAGL, sequence debugging runs in a dedicated Windows Forms/MSAGL debugger window, whereas with yComp it runs on the console as in GrShell. MSAGL is a better fit here since the Windows Forms message loop prevents the GUI freezes that occur when using MSAGL from GrShell.

## Output

- `GGrShell.exe` - Windows Forms executable (WinExe)

## Dependencies

- `libConsoleAndOS` - Console abstraction
- `libConsoleAndOSWindowsForms` - GUI console adapters
- `libGr` - Core graph interfaces
- `libGrShell` - Shell parser and interpreter
- `graphViewerAndSequenceDebugger` - Debugger logic
- `graphViewerAndSequenceDebuggerWindowsForms` - GUI debugger with MSAGL

## Key Files

| File | Size | Purpose |
|------|------|---------|
| `Program.cs` | 2KB | WinForms entry point |
| `GGrShellForm.cs` | 3.5KB | Main window form |
| `GGrShellForm.Designer.cs` | 5KB | Form designer generated code |
| `GGrShellForm.resx` | 103KB | Form resources |
| `ggrshell.ico` | 64KB | Application icon |
| `App.config` | 1KB | Application configuration |

## Architecture

The entry point (`Program.cs`) bridges the GUI with the libGrShell:

1. Creates `GGrShellForm` (main window)
2. Wraps GUI console in adapters:
   - `GuiConsoleControlAsTextReader` for input
   - `GuiConsoleControlAsTextWriter` for output
3. Redirects `ConsoleUI` streams to GUI:
   ```csharp
   ConsoleUI.inReader = inReader;
   ConsoleUI.outWriter = outWriter;
   ConsoleUI.errorOutWriter = outWriter;
   ```
4. Calls `GrShellMainHelper.ConstructShell()` - same as CLI shell
5. Runs Windows Forms message loop with `Application.Run(shell)`

## GGrShellForm

Main window containing:
- Console control for text input/output
- Menu bar (currently only offers Close; intended to be extended with shell commands)
- Integration points for debugger windows

The form stores references to:
- `shellConfig` - Shell configuration state
- `shellComponents` - Shell runtime components
- `reader` / `writer` - GUI console adapters

## Features

Offers the same features the GrShell offers, presented in a Windows Forms GUI, thus lacking the testability with the bash scripts, a few noteworthy points are:
- **Integrated console** - GUI-based console I/O (replaces terminal)
- **Graph viewers** - Both yComp and MSAGL; MSAGL preferable here (no GUI freeze)
- **Sequence debugger** - With MSAGL: dedicated Windows Forms/MSAGL debugger window; with yComp: console-based (as in GrShell)
- **Menu** - Currently only Close; to be extended

## Platform Support

This executable uses Windows Forms, which works on:
- **Windows**: Native .NET Framework
- **Linux**: Via Mono runtime (Windows Forms is supported by Mono)

## Running

**Windows:**
```
GGrShell.exe [script.grs] [args...]
```

**Linux:**
```bash
mono GGrShell.exe [script.grs] [args...]
```

Or launch without arguments for interactive mode.
