# libConsoleAndOS

Foundation library providing cross-platform console and OS utilities for GrGen.NET.

## Purpose

Abstracts console I/O operations to enable the same code to work in both CLI and GUI environments and in different types of these. All other GrGen components depend on this library for console operations.

## Output

- `libConsoleAndOS.dll` - Library assembly
- `libConsoleAndOS.xml` - XML documentation (Release builds)

## Dependencies

None - this is the foundation library with no project dependencies.

## Key Files

| File | Purpose |
|------|---------|
| `ConsoleUI.cs` | Static console I/O management with `outWriter`, `errorOutWriter`, `inReader` |
| `IDoEventsCaller.cs` | Interface for Windows Forms event processing (non-blocking GUI) |
| `TwinConsoleUI.cs` | Interfaces for dual-panel UIs with separate user-dialog and data-rendering consoles (used by the debugger) (the latter console is for the main work object, typically a sequence) |
| `TypeCreator.cs` | Runtime type creation utilities |
| `WorkaroundManager.cs` | Platform-specific workarounds for Windows/Mono compatibility |

## Key Classes

### ConsoleUI

Static class managing console streams:
- `outWriter` - TextWriter for stdout (defaults to Console.Out)
- `errorOutWriter` - TextWriter for stderr (defaults to Console.Error)
- `inReader` - TextReader for stdin
- `consoleOut` - IConsoleOutput for highlighted output
- `consoleIn` - IConsoleInput for key reading

### IConsoleOutput / IConsoleInput

Interfaces for extended console operations:
- `PrintHighlighted(text, mode)` - Output with syntax highlighting
- `ReadKey()` / `ReadKeyWithControlCAsInput()` - Key reading
- `KeyAvailable` - Check for pending input

### HighlightingMode

Flags enum for console highlighting:
- `Focus`, `FocusSucces`, `LastSuccess`, `LastFail`
- `Breakpoint`, `Choicepoint`, `SequenceStart`
- File type indicators: `GrsFile`, `GrgFile`, `GmFile`, etc.

## Usage Pattern

Other components redirect console I/O by setting ConsoleUI properties:
```csharp
ConsoleUI.outWriter = myCustomWriter;
ConsoleUI.consoleOut = myHighlightingOutput;
```

This enables GUI shells (GGrShell) to capture and display console output in GUI controls.
