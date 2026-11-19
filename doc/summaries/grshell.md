# GrShell Language

### Script Structure
- Commands separated by newlines or `;;`; line continuation: `\` at end of line
- Comments: `#` to end of line (or `§`)
- Case sensitive; keywords cannot be used as identifiers (use quoted text as workaround)
- Text literals: unquoted identifier, `"double quoted"`, or `'single quoted'`
- Commands outside the core chapter: debugger/visualization (Chapter debugger), validation/inspection (Chapter validation), persistent storage (Chapter persistentstorage)

### Common Commands
- `help [Command]` — list all commands, or show detailed help for a specific command
- `quit` / `exit` — quit GrShell (also closes any active graph viewer in debug mode)
- `echo Text` — print text to the console

### Graph Creation
- `new graph Filename [GraphName] [PersistenceSuffix]` — create graph from model/rule file (compiles if needed); graph name optional
- `new new graph Filename ...` — force recompilation regardless of file dates (use with `new set`/`new add`)
- `new [var] [: NodeType [($ = name, attr = val, ...)]]` — create node, assigned to `var` if given; var, type, and constructor all optional (defaults to anonymous `Node`); `$` sets persistent name
- `new src -[var][:EdgeType [(attrs)]]-> tgt` — create directed edge, assigned to `var` if given; src and tgt required; var, type, and constructor optional (also `<--` for reverse, `--` for undirected; type defaults to `Edge`/`UEdge`)
- Constructor attributes: `attr=val` comma-separated; containers: `set<T>{}`, `map<S,T>{}`, `array<T>[]`, `deque<T>[]`
- Uninitialized attributes get defaults (`int`←0, `bool`←false, `string`←`""`, etc.)
- `add new graph Name` — create subgraph of same model; becomes current subgraph
- `in Name` — switch to named subgraph (for import/export of grs files with subgraph attributes)
- `new ObjectType (attrs)` — create internal class object (non-transient); `%` sets unique object id

### Sequence Execution
- `exec Sequence` — execute graph rewrite sequence (`xgrs` is an alias)
- `eval SequenceExpression` — evaluate sequence expression (esp. for rule queries)
- `def seqName(params):(returns) { body }` — define/replace interpreted sequence at runtime (same signature required to replace; recursive sequences need empty-body stub first)
- `show profile [Action]` — show profiling info for one action or all rules/tests

### Variables
- `var = value` — assign variable (implicit declaration); value can be: another variable, graph element by persistent name (`@("name")`), internal class object by unique id name (`*("id")`), or literal
- Shell variables are the same as graph-global variables; accessible in sequences/rule language with `::v` prefix
- `show var Variable` — print variable value (or entity identifier for references)
- `askfor` — wait for user to press enter
- `var = askfor Type` — prompt user to enter a value of the given type; for value types: keyboard input; for graph element types: double-click in yComp (requires debug mode)

### Element Manipulation
- `Entity.attr = value` — set attribute
- `Entity.attr[index] = value` — overwrite array/deque/map entry at position/key
- `Entity.attr.add(val)` / `Entity.attr.add(key, val)` — add to set/array/deque (append or insert at index) or map
- `Entity.attr.rem([val])` — remove from set/map by value/key; from array/deque by index; no arg removes last (array) or first (deque)
- `retype Node <Type>` — retype node (common attributes and incident edges kept)
- `retype -Edge<Type>->` or `-Edge<Type>-` — retype directed or undirected edge
- `redirect Edge source|target Node` — redirect edge to new source or target node
- `delete node Node` — delete node (incident edges deleted too)
- `delete edge Edge` — delete edge
- `clear graph [Graph]` — delete all elements of current or named graph

### Inclusion and Conditional Execution
- `include Filename[.gz]` — execute another GrShell script (may be gzipped); parser errors stop execution
- `if SequenceExpression / Commands / [else / Commands] / endif` — conditional execution; nesting supported (each `endif` closes the innermost open `if`)

### File System Commands
- `pwd` — print current working directory
- `ls` — list files in working directory (GrGen-relevant files highlighted)
- `cd Path` — change working directory
- `! CommandLine` — execute arbitrary OS shell command (e.g. `!sh -c "ls | grep stuff"`)

### Shell and Environment Configuration
- `silence on|off` — toggle node/edge created/deleted messages (off = faster bulk creation)
- `silence exec on|off` — toggle match statistics printed during sequence execution (off avoids interference with emit output); not available in GGrShell
- `randomseed N|time` — set random seed for reproducible results (`$`-operator, random match selector)
- `redirect emit Filename` — redirect emit output to file
- `redirect emit -` — redirect emit output back to stdout

### Compilation Configuration
- `new add reference Filename` — add external assembly reference for generated assemblies (maps to `-r` grgen.exe option)
- `new set keepdebug|profile|nodebugevents|noevents|lazynic|noinline on|off` — configure code generation flags; only take effect on next regeneration (use `new new graph` to force); flags:
  - `keepdebug` — keep generated files, add debug symbols and validity checks
  - `profile` — include profiling (prints search steps after each exec)
  - `nodebugevents` — suppress debug event firing (action events)
  - `noevents` — suppress attribute change event firing
  - `lazynic` — evaluate negatives/independents/conditions lazily (at end of matching)
  - `noinline` — never inline subpatterns
- `new set statistics Filename` — use precomputed graph statistics for matcher generation (maps to `-statistics` option)

### Backend, Graph, and Actions Selection
- `show backend` — list parameters supported by current backend
- `select backend Filename [:params]` — select backend assembly (default: LGSPBackend)
- `select graph Graph` — switch current working/host graph
- `show graphs` — list all available graphs
- `delete graph Graph` — delete graph from backend storage
- `custom graph [SpacedParams]` — run backend-specific graph command (no args = list available commands)
- `select actions Filename` — load rule set (assembly `.dll` or source `.cs`); only one at a time
- `show actions` — list all rules/tests with parameters and return values
- `custom actions [SpacedParams]` — run backend-specific actions command (no args = list available commands)

### LGSPBackend Custom Commands

**Graph commands:**
- `custom graph analyze` — analyze current graph to build statistics for search plan generation; data stays valid until rule set unloaded, but becomes outdated after major structural changes
- `custom graph statistics save Filename` — save analysis statistics to file (run `analyze` first); use with `new set statistics` for pre-adapted matchers
- `custom graph optimizereuse true|false` — allow (default) or prevent reuse of deleted elements (false = safer for identity-based data structures)
- `custom graph optimizereusepoolsize N` — set pool size for reusable deleted elements (default: 10)

**Actions commands:**
- `custom actions gensearchplan [Action ...]` — generate optimized search plan(s) from graph analysis data; specify multiple rules in one call for efficiency; use after `custom graph analyze`
- `custom actions explain Action` — display current search plan for a rule (like SQL `EXPLAIN`); shows matching order and helps diagnose performance issues
- `custom actions dumpsourcecode true|false` — dump C# files for newly generated search plans (default: false)
- `custom actions setmaxmatches N` — limit max matches for `[Rule]` expression (0 or negative = no limit)
- `custom actions adaptvariables true|false` — auto-null graph-global variables when their element is deleted/retyped (default: true; false improves performance but risks zombie references)
- `custom actions enableassertions true|false` — enable rule assertions (default: false)
