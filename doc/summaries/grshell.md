# GrShell Language

### Script Structure
- Commands separated by newlines or `;;`
- Comments: `#` to end of line
- Line continuation: `\` at end of line

### Graph Creation
- `new graph "path\to\model"` -- create graph (compiles actions if needed)
- `new new graph "..."` -- force recompilation
- `new [var] [:NodeType [($=name, attr=val)]]` -- create node
- `new src -[var][:EdgeType]-> tgt` -- create directed edge
- `new src -[var][:EdgeType]- tgt` -- create undirected edge

### Sequence Execution
- `exec Sequence` -- execute graph rewrite sequence (modern form)
- `xgrs Sequence` -- (outdated synonym for exec)
- `eval Expression` -- evaluate sequence expression
- `def seqName(params):(returns) { body }` -- define runtime sequence

### Variables and References
- `var = value` -- assign (implicit declaration)
- `show var Variable` -- print variable (recorded for tests)
- `@("name")` -- reference element by persistent name
- `@@("uniqueId")` -- reference by unique ID

### Element Manipulation
- `Entity.attr = value` -- set attribute
- `Entity.attr[index] = value` -- indexed assignment
- `Entity.attr.add(v)` / `.rem(v)` -- container manipulation
- `retype node <Type>` / `retype -edge<Type>->` -- retype
- `redirect edge source|target node` -- redirect
- `delete node n` / `delete edge e` -- delete

### Configuration
- `silence on|off` -- toggle creation/deletion messages
- `silence exec on|off` -- toggle match statistics
- `randomseed N|time` -- set random seed
- `redirect emit Filename` / `redirect emit -` -- redirect emit output
- `custom graph analyze` + `custom actions gensearchplan` -- performance optimization
- `custom actions enableassertions true|false` -- enable rule assertions
- `custom actions setmaxmatches N` -- limit matches for `[Rule]`

