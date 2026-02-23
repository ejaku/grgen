# Visualization and Debugging

### Graph Viewers
- `show graph ycomp` — dump graph to VCG/DOT file and open in yComp (default viewer, academic license only)
- `show graph msagl` — show graph in built-in MSAGL viewer (MIT license, embeddable in own .NET app)
- `show graph dot|neato|...` — dump to dot file, render with graphviz, open as PNG
- `dump graph Filename` — dump current graph to VCG file
- `debug with yComp|MSAGL` — switch active graph viewer

### Display Configuration (dump commands)
- `dump set node/edge Type color Color` — set element colors
- `dump set node/edge Type textcolor|bordercolor Color` — set text/border colors
- `dump set node/edge Type shape Shape` — set shape (box, circle, ellipse, rhomb, hexagon, ...)
- `dump set node/edge Type labels on|off|Text` — toggle/override type name labels
- `dump set edge Type linestyle continuous|dotted|dashed` — line style
- `dump set edge Type thickness N` — line thickness (1–5)
- `dump add node/edge Type infotag Attribute` — show attribute as label (Name=Value)
- `dump add node/edge Type shortinfotag Attribute` — compact label (Value only)
- `dump add node/edge Type exclude` — hide elements of a type from output
- `dump add node Type group by GroupMode [EdgeType [with NodeType]]` — nested layout: group contained nodes visually inside group node; GroupMode: no/incoming/outgoing/any
- `dump add graph exclude` / `dump set graph exclude option Depth` — show only match + context (for large graphs)
- `dump reset` — reset all dump style options

### Debug Mode Commands
- `debug enable|disable` — enable/disable debug mode (tracks all graph changes live in viewer)
- `debug exec Sequence` — execute sequence under debugger
- `debug eval SequenceExpression` — evaluate sequence expression under debugger
- `debug set layout Text` — set layout algorithm
  - yComp: Random, Hierarchic, Organic, Orthogonal, Circular, Tree, Compilergraph, ...
  - MSAGL: SugiyamaScheme, MDS, Ranking, IncrementalLayout
- `debug set layout option Name Value` — set layout option
- `debug layout` — force re-layout in yComp
- `debug set node|edge mode Text DumpContinuation` — configure visual debug state colors (matched/created/deleted/retyped/redirected)
- `debug set match mode pre|post enable|disable` — pre-match: show all matches before filtering; post-match: show exec-embedded matches in detail
- `debug set option twopane true|false` — MSAGL: two-pane mode (data pane + dialog pane)
- `debug set option gui true|false` — MSAGL: GUI mode with menu bar and toolbar
- `debug get options` — list available debug options
- `debug get layout` / `debug get layout options` — query current layout

### Debug Stepping Commands (during debugging)
- `s` (step) — execute current rule, show result
- `d` (detailed step) — three-phase: highlight match → highlight changes → carry out rewrite; also steps into embedded exec sequences
- `n` (next) — go to next rule that matches
- `r` (run) — continue until end or breakpoint
- `a` (abort) — cancel execution
- `o` (step out) — run to end of current loop or called sequence
- `u` (step up) — ascend one level in the sequence tree
- `b` (breakpoint) — toggle breakpoint at a breakpointable location
- `c` (choicepoint) — toggle choicepoint at a choicepointable location
- `w` (watchpoints) — interactively edit watchpoints
- `v` (variables) — print global and local variables plus visited flags
- `t` (trace) — print sequence call stack trace
- `f` (full dump) — stack trace plus all local variables per frame
- `p` (dump graph) — dump current graph to .vcg and show in yComp
- `g` (as-graph) — show external type or graph value as graph
- `h` (highlight) — highlight graph elements by visited flag or variable
- `j` (show object) — crawl objects/transient objects by id or variable

### Breakpoints and Choicepoints
- `%` before a rule/predicate/constant — acts as breakpoint (halts and waits)
- `$%` modifier — turns a random decision into an interactive user choice (choicepoint)
- Breakpoints/choicepoints can be toggled at runtime via `b`/`c` commands

### Watchpoints (shell commands)
- `debug on add|rem|emit MessageFilter break` — break on subrule debug message
- `debug on halt|highlight MessageFilter continue` — suppress halt on debug message
- `debug on match RuleName break|continue [if SequenceExpr]` — break/skip on action match (conditional)
- `debug on new|delete|retype|set attributes TypeNameSpec break [if SequenceExpr]` — break on graph change event (data breakpoint)
  - MessageFilter: `equals|startsWith|endsWith|contains(StringConstant)`

### Subrule Debugging (embedded in rule/procedure code)
- `Debug::add(message[, object]*)` — push entry onto debug message stack (call on enter)
- `Debug::rem(message[, object]*)` — pop entry from debug message stack (call on exit, must match add)
- `Debug::emit(message[, object]*)` — add transient message to stack
- `Debug::halt(message[, object]*)` — break execution in debugger, show message
- `Debug::highlight(message[, entity, string]*)` — break and highlight nodes/edges/containers/visited-flag elements in graph
