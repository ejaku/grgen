# Util (Utilities)

The `util/` package contains 48 Java files providing foundational infrastructure for the entire frontend compiler: AST traversal, code generation helpers, error reporting, graph visualization, and data structures. Note that several of them are unused.

## Tree Traversal (Visitor/Walker Framework)

- **`Visitor`** - Interface: `void visit(Walkable n)`.
- **`Walkable`** - Interface: exposes children via `getWalkableChildren()`. All `BaseNode` subclasses implement this.
- **`Walker`** - Interface: `walk(Walkable)` and `reset()`.
- **`PrePostWalker`** - Calls visitors before (pre) and after (post) descending into children. Tracks visited nodes to prevent cycles.
- **`PreWalker` / `PostWalker`** - Convenience for pre-order or post-order only.
- **`ConstraintWalker`** - Post-walker that filters nodes by type before visiting.
- **`ResultVisitor<RT>`** - Visitor that computes and returns a result.
- **`BooleanResultVisitor`** - Specialized for boolean results.
- **`ParamVisitor`** - Abstract visitor with parameter passing.

## Code Generation

- **`SourceBuilder`** - StringBuilder with automatic indentation management. Methods: `indent()`, `unindent()`, `appendFront()`, `appendFrontIndented()`. Used extensively by the C# backend generators.
- **`Formatter`** - Formats IR elements for C# output: operator mapping, expression formatting, attribute access.

## Error Reporting

- **`Location`** - Interface for source file locations.
- **`ErrorReporter`** - Channels: ERROR, WARNING, NOTE. Static counters: `getErrorCount()`, `getWarnCount()`. Helpers: `error()`, `warning()`, `note()`.
- **`Reporter`** - Base reporting class with channel-based masking and handler registry.
- **Handlers**: `StreamHandler` (to PrintStream), `TableHandler` (table format), `TreeHandler` (tree format), `NullReporter` (silent).
- **`DebugReporter`** - Specialized for debug output.

## Graph Visualization

- **`GraphDumpable`** - Interface for objects that can be visualized (provides node ID, color, shape, label, edge labels).
- **`GraphDumper`** - Interface for graph output (nodes, edges, subgraphs with styling).
- **`VCGDumper`** - Outputs VCG format for graph visualization tools. Supports shapes (BOX, RHOMB, ELLIPSE, TRIANGLE) and line styles (SOLID, DASHED, DOTTED).
- **`GraphDumpVisitor`** - Visitor that traverses AST and emits graph visualization.
- **`GraphDumpableProxy`** - Proxy wrapper for non-GraphDumpable objects.

## XML Output

- **`XMLDumpable`** - Interface for XML serialization: `addFields(Map)`, `getTagName()`, `getXMLId()`.
- **`XMLDumper`** - Converts XMLDumpable objects to formatted XML with cycle detection.

## Annotations

- **`Annotated`** - Interface: `getAnnotations()`.
- **`Annotations`** - Interface of map-like metadata storage with type checks: `isInteger()`, `isBoolean()`, `isString()`, `isFlagSet()`.
- **`DefaultAnnotations`** - HashMap-based implementation.
- **`EmptyAnnotations`** - No-op implementation.

## Data Structures

- **`Pair<T, S>`** - Generic tuple with proper `equals()`/`hashCode()`.
- **`Mutable<T>`** - Mutable value wrapper.
- **`Direction`** - Enum: INCIDENT, INCOMING, OUTGOING, INVALID.

## I/O and Stream Utilities

- **`Util`** - Static helpers: path/suffix manipulation, file I/O (`writeFile`, `openFile`, `copyFile`), reflection helpers, string list formatting.
- **`MultiplexOutputStream`** - Writes to multiple streams simultaneously (up to 32).
- **`NullOutputStream`** - Silent output stream.

## Base Infrastructure

- **`Base`** - Base class with automatic unique ID assignment and static reporter access. Used by walkers and dumpers.
- **`Id`** - Interface guaranteeing unique IDs for program lifetime.
- **`Retyped`** - Interface for entities that change type during rewriting.

## Preferences

- **`MyPreferences`** / **`MyPreferencesFactory`** - Custom `Preferences` implementation using HashMap backing store instead of OS registry.
