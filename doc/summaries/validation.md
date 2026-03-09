# Validation and Inspection

### Show Commands
- `show node|edge|object|transient object types` -- list types of the given kind
- `show node|edge|object|transient object super|sub types T` -- inheritance hierarchy for a type
- `show node|edge|object|transient object attributes [[only] Type]` -- attribute types (all, or for given type; `only` excludes inherited)
- `show [num] nodes [[only] NodeType]` / `show [num] edges [[only] EdgeType]` -- instances or count (nodes/edges only)
- `show node|edge|object|transient object Entity` -- attribute types and values of a specific entity
- `show Entity.AttributeName` -- value of a specific attribute
- `node|edge|object|transient object type T1 is T2` -- check whether T1 is type-compatible with T2
- `show var Variable` -- variable content (recorded by test framework)

### Graphical Show Commands
Graphical output via the configured graph viewer (yComp or MSAGL); not suited for test automation.
- `gshow model` -- show the graph model as a UML-like inheritance diagram
- `gshow patterns` -- show the patterns of the current actions as a graph (edges rendered as nodes so that pattern nesting is visible)

### Validation
- `validate [exitonfailure] [strict [only specified]]` -- connection assertion check
- `validate [exitonfailure] exec Sequence` -- validate via sequence (on cloned graph)
