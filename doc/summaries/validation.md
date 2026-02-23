# Validation and Inspection

### Show Commands
- `show node|edge types` -- list types
- `show node|edge super|sub types T` -- inheritance hierarchy
- `show node|edge attributes [only] [Type]` -- attribute types
- `show [num] nodes [only NodeType]` / `show [num] edges [only EdgeType]` -- instances or count
- `show node|edge Entity` -- attribute values
- `show Entity.AttributeName` -- specific attribute
- `show var Variable` -- variable content (recorded by test framework)

### Graphical Show Commands
Graphical output via the configured graph viewer (yComp or MSAGL); not suited for test automation.
- `gshow model` -- show the graph model as a UML-like inheritance diagram
- `gshow patterns` -- show the patterns of the current actions as a graph (edges rendered as nodes so that pattern nesting is visible)

### Validation
- `validate [exitonfailure] [strict [only specified]]` -- connection assertion check
- `validate [exitonfailure] exec Sequence` -- validate via sequence (on cloned graph)
