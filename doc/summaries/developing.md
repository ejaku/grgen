# Developing GrGen (Internals)

### Building
- Frontend: `cd frontend && make` (or make -f Makefile_Cygwin on Windows); yields `grgen.jar`
  - IDE alternative: add `de/` subfolder + `jars/` to Eclipse; ANTLR parser generation must be done manually as pre-build step
- Backend: VisualStudio 2022 solution in `engine-net-2/`; VisualStudio 2017 requires uncommenting binding redirects in `app.config` files
  - Before building: run `./genparsers.sh` (Linux) or `./genparsers.bat` (Windows) to generate CSharpCC parsers for sequences, constant literals, and shell
- Full build Linux: `./make_linux.sh` (uses `dotnet build`; mdtool/MonoDevelop no longer works since SDK-style projects)
  - Development: VSCode + C# extension + Mono Debug extension; `dotnet` for build, `mono` still required for running/debugging (.NET Framework target)
- Full build Windows: `./make_cygwin_windows.sh` (uses `dotnet`; the official releases are built with VisualStudio/`msbuild`)
  - API examples: run `./genlibs.bat`
- Build configs: `x64`, `x86`, `Any CPU`; release ships x64 only (changed at v8.0 due to SQLite native binaries requirement)
- Documentation: `doc/build grgen` (Linux) or `doc/build_cygwin.sh grgen`
- Syntax highlighting: `syntaxhighlighting/` — Notepad++, vim, Emacs specs for `.gm`/`.grg`/`.grs` files

### Testing
- Frontend tests: `cd frontend/test && ./test.sh` (should_pass/should_fail/should_warn)
  - Known failures: 3 math tests on Mono/Linux due to minor numeric differences
- Frontend unit tests (JUnit4): `cd frontend && ./unittest/make_unittest.sh` (also builds frontend)
- Backend semantic tests: `cd engine-net-2/tests && ./test.sh` (compares output against `.grs.data` files)
- Backend NUnit3 unit tests: `cd engine-net-2/unittests/GraphAndModelTests && dotnet test`
- Examples smoke tests: `cd engine-net-2/examples && ./test.sh`
- Thorough parallelization/profiling coverage: manually force parallelization (search `uncomment` in `Unit.java`) or profiling (search `uncomment` in `GrGen.cs`)

### Internal Graph Representation
- **Type ringlists**: doubly-linked per-type list of all node/edge instances; head stored in array indexed by type; `typeNext`/`typePrev` fields in graph elements
- **Incidence ringlists**: per-node `inHead`/`outHead` with `inNext`/`inPrev`/`outNext`/`outPrev` fields in edges; edges also have `source`/`target` fields
- **Attribute indices**: AA-tree (balanced BST) per declared index; maintained by event handlers on element creation/deletion/attribute assignment
- **Naming/uniqueness**: `LGSPNamedGraph` for names, `LGSPUniquenessEnsurer`/`LGSPUniquenessIndex` for uniqueness; `LGSPGraphStatistics` for graph analysis results (element counts, V-struct statistics)

### Pattern Matching and Search Programs
- Backtracking algorithm: bind pattern elements one-by-one to graph element candidates; nested loops with condition checks
- Non-leaf type lookup: additional outer loop iterating all subtypes
- Parameterized patterns: start from parameters instead of type-based lookup
- Multiple connected components: multiple lookup roots needed
- Undirected edges: searched in both incoming and outgoing ringlists
- Other operations: storage access, storage attribute access, storage mapping, attribute index access, name/unique index access
- Rewriting order: (i) create new nodes, (ii) create new edges, (iii) evaluate/assign attributes, (iv) delete edges, (v) delete nodes, (vi) execute embedded sequences
- **Search state space stepping**: after match found, move type/in/out list heads to matched positions (`MoveHeadAfter` etc.) so next iteration of `r*` resumes from that point

### Pushdown Machine (Nested/Subpattern Matching)
- 2+n pushdown machine: call stack (bound elements in local vars of per-pattern search program/subpattern instances) + open tasks stack + n result building stacks
- Task: pattern to match + parameters; after match, nested subpatterns pushed to open tasks stack
- n=1 for normal rule; unbounded for all-bracketed `[r]`
- Alternatives: like subpattern, with multiple patterns tried in order, first match accepted
- Iterateds: task not removed on match; removed only on failure or max reached; min count checked on failure to determine outcome
- Rewriting: depth-first traversal of resulting match object tree; create new elements before descending, eval attributes and delete before ascending
- Compiler-like attribute grammar computation: inherited (subpattern params from matched elements/call expressions during matching), synthesized (LHS yield after match found), left-attributed (RHS eval during rewriting)

### Search Planning
- Plan graph: plan nodes = pattern graph elements (nodes + edges); plan edges = operations with cost weights; root plan node with lookup plan edges to all plan graph nodes
  - node→edge plan edges: `out`/`in` operations; edge→node plan edges: `src`/`tgt` operations
  - Lookup costs from: element counts per type; V-structure statistics = triple (node type, edge type, node type) for splitting factor estimation
- Minimum spanning arborescence of plan graph → schedule (ordered list of search operations) → search program tree (syntax tree level) → emitting of C# code
- Isomorphy checking: flags in graph elements (set on bind, reset on unbind); one flag per negative/independent nesting level up to implementation limit, then list of dictionaries; iso-check scheduling pass avoids redundant checks when types already rule out identity

### Code Generator Architecture — Frontend
Frontend directories: `parser` (ANTLR grammar `Grgen.g`/`EmbeddedExec.g` + symbol table/scopes), `ast` (AST node classes with base `BaseNode`), `ir` (IR classes), `be` (backends), `util`, entry point `Main.java`

**3-pass AST processing:**
1. `resolve`/`resolveLocal` (preorder): replace identifier nodes with declaration nodes
2. `check`/`checkLocal` (postorder): type and semantic checking
3. `getIR`/`constructIR`: build IR from AST

**IR classes** (`ir/`): `Rule` (rules, alternative cases, iterateds), `PatternGraphLhs` (all LHS patterns incl. negatives/independents; contains data flow analyses), `PatternGraphRhs`; noteworthy `ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern` from `Unit.java`

**Frontend backends:**
- `be/C`: C backend for IPD C compiler (FIRM-based); not used by GrGen.NET
- `be/Csharp`: generates `FooModel.cs` (model) and `FooActions_intermediate.cs` (pattern specs, embedded sequences, rewrite code) — does NOT generate matcher code (that is done by `grgen.exe` utilizing `lgspBackend`)

**Model generation** (`ModelGen`): enums → node classes → node model → edge classes → edge model → graph model; 3 classes per node/edge type: interface (user-visible attributes), implementation (inherits `LGSPNode`/`LGSPEdge`), type-representation (inherits `NodeType`/`EdgeType`); plus `ModelIndexGen`, `ModelExternalGen`

**Rule representation pass** (`ActionsGen`): generates `MatchingPattern`/`RulePattern`/`PatternGraph` C# classes/object building code describing the subpatterns/rules/patterns; homomorphy tables (local + global); match classes (`ActionsMatchGen`); embedded sequences as `LGSPEmbeddedSequenceInfo`; closure classes (`LGSPEmbeddedSequenceClosure`) for sequences in alternatives/iterateds/subpatterns (`ActionsExecGen`); condition expressions and yield statements via `ActionsExpressionOrYieldingGen`
- Elements defined in a nesting pattern carry a `PointOfDefinition` member recording the first `PatternGraph` in which they appear
- All elements are created flatly in `MatchingPattern.initialize()`; `pathPrefix` parameter prevents name clashes across nesting levels

**Rewrite code pass** (`ModifyGen`): local rewrite methods per pattern piece with roles Modify/Create/Delete; `ModifyGenerationState`/`ModifyGenerationTask`; eval blocks (`ModifyEvalGen`), exec calls (`ModifyExecGen`)

### Code Generator Architecture — Backend (lgspBackend)
Main driver: `lgspMatcherGenerator.cs`; operates on `PatternGraph` nesting in `RulePattern`/`MatchingPattern` objects;
generates matcher code from rule and pattern representations

**Step 1** — Search planning:
- Subpattern inlining (one level; `PatternGraphAnalyzer` for optimization hints)
- `PlanGraph` construction → minimum spanning arborescence → `SearchPlanGraph` → `ScheduledSearchPlan` (stored in `PatternGraph`); `AppendHomomorphyInformation` adds isomorphy checking information

**Step 2** — Schedule enrichment (`ScheduleEnricher`):
- `MergeNegativeAndIndependentSchedulesIntoEnclosingSchedules`: integrates negative/independent schedules into enclosing schedule recursively; result in `ScheduleIncludingNegativesAndIndependents`

**Step 3** — Code generation (`GenerateActionAndMatcher`):
- Generates `Action`/`SubpatternAction`/`AlternativeAction`/`IteratedAction` classes
- `SearchProgramBuilder` + `SearchProgramBodyBuilder`: builds `SearchProgram` tree from schedule (`BuildScheduledSearchPlanOperationIntoSearchProgram`)
- `SearchProgramCompleter`: fills in continue-at locations and undo code for rollback
- `Emit` methods output C#; `Dump` methods produce readable debug form
- `lgspBackend/ExpressionOrYielding/`: code for condition expressions and yield statements

**Nested patterns in backend:**
- `SubpatternAction` classes do not only contain the matcher code but are simultaneously the pushdown machine tasks (hold parameters as members)
- Key search program operations: `InitializeSubpatternMatching` at start, `FinalizeSubpatternMatching` at end; during processing, at the end of the schedule `buildMatchComplete` is called, which in turn calls the methods inserting the pushdown machine control code: `insertPushSubpatternTasks` → `insertPopSubpatternTasks` → `insertCheckForSubpatternsFound` → `insertMatchObjectBuilding`

### libGr and lgspBackend Internals
- Sequence parser generated from `SequenceParser.csc`; builds sequence AST from `Sequence.cs`, `SequenceComputation.cs`, `SequenceExpression.cs` classes using `SequenceParserEnvironment` and `SymbolTable`; `SequenceCheckingEnvironment` for type checking (interpreted vs compiled variants)
- Interpreted sequences: `ApplyImpl(IGraphProcessingEnvironment)` method on sequence AST objects
- Compiled sequences: `lgspSequenceGenerator` (driver) employs `SequenceGenerator` for rule-controlling sequences, which in turn employs `SequenceComputationGenerator` and `SequenceExpressionGenerator`
- Graph events: delegates defined in `IGraph.cs`; recording in `Recorder.cs` (replay = normal `.grs` execution); transactions in `lgspTransactionManager.cs` (list of undo items; purged on commit, executed on rollback; backtracking = nested transactions)
- Helper files: `ContainerHelper.cs`, `DictionaryHelper.cs`, `SequentialTypeHelper.cs`; `GraphHelper.cs` (graph queries/manipulation), `GraphHelperParallel.cs` (parallel matcher safe version); emit/type helpers
- Importers/exporters in `src/libGr/IO`
- **Graph comparison**: `InterpretationPlanBuilder` builds `InterpretationPlan` from a pattern; `Execute` runs it; after N uses auto-compiles to speed up (see `lgspBackend/GraphComparison/`)
- Filter implementations: `FilterGenerator.cs` for auto-generated filters, `SymmetryChecker.cs` for automorph filter; auto-supplied filters in `libGr/matchesHelper.cs` and `lgspBackend/Actions/lgspMatches.cs`
- `src/GrGen/`: driver of `grgen.exe` compiler (calls Java frontend, extends and compiles generated C# code)

### GrShell and Debugger Internals
- `src/GrShell` and `src/libGrShell`: uses sequence interpretation + generic libGr interface (works with arbitrary models/actions at runtime)
- Parser from `GrShell.csc`; shell implementation in `GrShellImpl.cs`; entry point in `GrShellMain.cs`; sequence execution in `GrShellSequenceApplierAndDebugger.cs`
- Debugger: `Debugger.cs` + `YCompClient.cs` (controls yComp via TCP to localhost) + `GraphViewer.cs`
