# Developing GrGen (Internals)

### Building
- Frontend: `cd frontend && make` (produces `grgen.jar`)
- Backend: `cd engine-net-2 && dotnet build GrGen.sln`
- Parsers: `cd engine-net-2 && ./genparsers.sh` (CSharpCC-based, run if grammar changed)
- Full build: `./make_linux.sh` or `./make_cygwin_windows.sh`

### Testing
- Frontend tests: `cd frontend/test && ./test.sh` (should_pass/should_fail/should_warn directories)
- Frontend acceptance tests (JUnit): `cd frontend && ./unittest/make_unittest.sh` (also builds frontend and tests)
- Backend tests: `cd engine-net-2/tests && ./test.sh` (compares output against `.grs.data` files)
- Examples: `cd engine-net-2/examples && ./test.sh`
- NUnit: `cd engine-net-2/unittests/GraphAndModelTests && dotnet test`

### Generated Code Structure
- **Type ringlists**: each type has a doubly-linked list of all instances for fast traversal
- **Incidence ringlists**: each node has linked lists of incoming/outgoing edges
- **Search programs**: pattern matching compiled into nested loops with type-filtered iteration

### Pushdown Machine (Nested/Subpattern Matching)
- 2+n pushdown machine: main stack + pattern candidate stack + n open-task stacks
- Handles alternatives, iterateds, optionals, subpatterns via task-based execution
- Each nesting level pushes tasks; matching proceeds depth-first

### Search Planning (Code Generation)
- Plan graph: nodes = pattern elements, edges = possible lookup operations with cost
- Spanning arborescence: minimum-cost tree determines search order
- Schedule: linearized plan becomes nested loop structure

### Code Generator Architecture
- **Frontend**: ANTLR parser -> AST -> IR (intermediate representation) -> C# code generation: model classes (`FooModel.cs`) + intermediate actions (`FooActions_intermediate.cs`: pattern specs, embedded sequences, rewrite code)
- **Backend** (3 phases):
  1. Inlining + search planning: inline subpatterns, compute search plans, linearize into schedules (`ScheduledSearchPlan`)
  2. Schedule merging: integrate schedules of negative/independent pattern graphs into the enclosing schedule (`ScheduleIncludingNegativesAndIndependents`)
  3. Code generation: build `SearchProgramOperation` tree, emit C# source
- Key classes: `SearchPlanGraph`, `SearchPlanNode`, `SearchProgramOperation`
