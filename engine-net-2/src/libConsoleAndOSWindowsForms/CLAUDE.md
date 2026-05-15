# libConsoleAndOSWindowsForms

Windows Forms extensions for GUI console integration.

## Purpose

Bridges Windows Forms GUI with the console abstraction layer, allowing the shell as well as the debugger to run in both text console and GUI console modes through the same underlying code. Provides GUI controls that implement the console interfaces from libConsoleAndOS.

## Output

- `libConsoleAndOSWindowsForms.dll` - Library assembly
- `libConsoleAndOSWindowsForms.xml` - XML documentation (Release builds)

## Dependencies

- `libConsoleAndOS` - Console abstraction interfaces

## Key Files

| File | Purpose |
|------|---------|
| `GuiConsoleControl.cs` | User control for embedding console in Windows Forms |
| `GuiConsoleControl.Designer.cs` | Form designer generated code |
| `GuiConsoleControlAdapters.cs` | TextReader/TextWriter adapters for GUI console |
| `DoEventsCaller.cs` | Implements IDoEventsCaller for Windows Forms |

## Key Classes

### GuiConsoleControl

A Windows Forms UserControl (built around a `RichTextBox`) that provides a console-like text interface:
- Accepts text input like a terminal; only the last (current input) line is editable — all prior output is read-only; the logical input line may span multiple physical lines due to word-wrap, so editing uses `ReplaceLineInTextBoxAndSetCaret` rather than rewriting the whole control (performance/flicker)
- Displays output with optional highlighting; Mono/Linux workaround: background highlight color not supported, uses foreground color instead
- Supports key interception for debugger commands
- **Single-line clipboard paste** via Ctrl+V (first line only; multi-line paste is handled at GGrShell level via `ClipboardLineSource`); Ctrl+V also detected via `''` char as fallback for Mono/Linux
- Ctrl+X suppressed on Mono/Linux (would print an unwanted character)
- `Application.DoEvents()` called at key points during blocking `ReadLine` to keep the GUI responsive

### GuiConsoleControlAsTextReader

Adapts GuiConsoleControl to `System.IO.TextReader`:
- Blocks until input is available
- Implements `IConsoleInput` for key reading
- Handles Control+C as input

### GuiConsoleControlAsTextWriter

Adapts GuiConsoleControl to `System.IO.TextWriter`:
- Implements `IConsoleOutput` for highlighted output
- Routes Write/WriteLine to GUI control
- Supports highlighting modes (breakpoints, focus, etc.)

### DoEventsCaller

Implements `IDoEventsCaller`:
- Calls `Application.DoEvents()` during long operations
- Prevents GUI freezing during sequence execution

## Usage

GGrShell uses these adapters to redirect console I/O:
```csharp
GuiConsoleControlAsTextReader inReader = new GuiConsoleControlAsTextReader(shell.console);
GuiConsoleControlAsTextWriter outWriter = new GuiConsoleControlAsTextWriter(shell.console);
ConsoleUI.inReader = inReader;
ConsoleUI.outWriter = outWriter;
```
