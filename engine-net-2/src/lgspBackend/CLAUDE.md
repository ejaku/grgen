# lgspBackend

LGSP (libGr Search Plan) backend - the pattern matching engine and code generator for GrGen.NET.

## Purpose

Implements libGr interfaces for graph, processing environment, pattern matching, and backend. Key interfaces implemented directly: `IBackend`, `IGraph`/`INamedGraph`, `INode`/`IEdge`/`IObject`, `IGraphModel`, `IActionExecutionEnvironment`, `ITransactionManager`. Also provides base classes for patterns and matches that the generated code extends. Converts .grg rule files (parsed by Java frontend) into executable C# code with optimized search plans;
this also contains the code generation for the compiled embedded sequences.

## Output

- `lgspBackend.dll` - Library assembly
- `lgspBackend.xml` - XML documentation (Release builds)

## Dependencies

- `libConsoleAndOS` - Console abstraction
- `libGr` - Core graph interfaces

## Directory Structure

```
lgspBackend/
├── Actions/              # Action execution wrappers
├── ExpressionOrYielding/ # Expression evaluation and yield handling
├── Graph/                # LGSP graph implementation
├── GraphComparison/      # Graph comparison (full graph isomorphisms)
├── GraphProcessingEnvironments/  # LGSP graph processing environments
├── MatcherGenerator/     # Pattern matcher code generation
├── SearchProgramBuilder/ # Search program construction
├── SearchProgramOperations/  # Individual search operations
├── SequenceGenerator/    # Sequence code generation
└── *.cs                  # Core files at root level
```

## Subdirectories

### Actions/

Action and pattern infrastructure:
- `lgspActions.cs` - Base action classes
- `lgspMatches.cs` - Match and match list classes
- `lgspPattern.cs` - Pattern graph and matching pattern classes
- `lgspPatternElements.cs` - Pattern nodes, edges, variables, embeddings (of subpatterns), alternatives, iterateds

### ExpressionOrYielding/

Expression evaluation and yield handling:
- `Expression.cs` - Expression base and scalar expression classes
- `ContainerExpression.cs` - Container-typed expression classes
- `GraphExpression.cs` - Graph-element expression classes
- `Yielding.cs` - Yield assignment and accumulation classes

### GraphProcessingEnvironments/

LGSP execution environments:
- `lgspGraphProcessingEnvironment.cs` - Main processing environment
- `lgspActionExecutionEnvironment.cs` - Action execution context
- `lgspGlobalVariables.cs` - Global variable storage
- `lgspSubactionAndOutputAdditionEnvironment.cs` - Subaction/output environment
- `lgspDeferredSequencesManager.cs` - Deferred sequence scheduling
- `lgspTransactionManager.cs` - Transaction management
- `lgspTransactionManagerUndoItems.cs` - Undo item types

### MatcherGenerator/

Pattern matcher code generation and optimization:
- `lgspMatcherGenerator.cs` - Main matcher generator
- `PatternGraphAnalyzer.cs` - Pattern graph optimization analysis
- `PlanGraph.cs` - Plan graph data structure
- `PlanGraphGenerator.cs` - Plan graph construction
- `SearchPlanGraph.cs` - Search plan graph data structure
- `SearchPlanGraphGeneratorAndScheduler.cs` - Search plan graph generation and scheduling
- `ScheduledSearchPlan.cs` - Scheduled (executable) search plan
- `ScheduleEnricher.cs` - Schedule enrichment
- `ScheduleExplainer.cs` - Schedule explanation/debugging
- `ScheduleDumper.cs` - Schedule output
- `SearchOperationType.cs` - Search operation type enumeration
- `FilterGenerator.cs` - Filter code generation

### SearchProgramBuilder/

Builds executable search program structure:
- `SearchProgramBuilder.cs` - Main builder
- `SearchProgramBodyBuilder.cs` - Body generation
- `SearchProgramBodyBuilderHelper.cs` - Builder utilities
- `SearchProgramCompleter.cs` - Finalization

### SearchProgramOperations/

Individual search operations (15+ files; esp. element candidate getting, candidate checking, candidate accepting/abandoning):
- Node/edge lookup and matching operations
- Attribute checking operations
- Negative/independent pattern checking
- Type checking and casting operations
- Incidence and adjacency operations

### SequenceGenerator/

Sequence code generation (20+ files):
- `lgspSequenceGenerator.cs` - Main sequence generator
- `SequenceRuleCallMatcherGenerator.cs` - Rule invocation
- `SequenceComputationGenerator.cs` - Computation statements
- `SequenceExpressionGenerator.cs` - Expression evaluation
- `SequenceBacktrackGenerator.cs` - Backtracking support
- `NeededEntitiesEmitter.cs` - Variable management

### Graph/

LGSP graph implementation:
- `lgspGraphElements.cs` - Node and edge classes with ringlist incidence
- `lgspGraphElementFlags.cs` - Flags for graph elements (visited, iso-checking, etc.)
- `lgspObject.cs` - Internal class object implementation
- `lgspTransientObject.cs` - Transient object implementation
- `lgspGraph.cs` - Graph implementation with type ringlists for fast traversal by type
- `lgspNamedGraph.cs` - Named graph (adds name-to-element lookup)
- `lgspPersistentNamedGraph.cs` - Named graph with persistence provider support
- `lgspGraphModel.cs` - Graph model implementation
- `lgspGraphStatistics.cs` - Graph statistics for search plan cost estimation
- `GraphStatisticsParserSerializer.cs` - Statistics serialization
- `lgspUniquenessEnsurer.cs` - Ensures unique element ids

### GraphComparison/

Graph comparison / full graph isomorphism checking:
- Canonical form computation
- Graph equality testing with an interpretation plan (created by an InterpretationPlanBuilder)

## Key Root Files

| File | Size | Purpose |
|------|------|---------|
| `lgspBackend.cs` | 60KB | Core backend implementation (implements `IBackend`) |
| `lgspGrGen.cs` | 95KB | Frontend that generates C# from .grg rules |
| `NamesOfEntities.cs` | 15KB | Entity naming conventions |
| `SourceBuilder.cs` | 3KB | C# source code builder |
| `ThreadPool.cs` | 6KB | Thread pool for parallel matching |
| `WorkerPool.cs` | 6KB | Worker pool management |

## Key Classes

### LGSPBackend

Singleton implementing `IBackend`:
- `Instance` - Static singleton accessor
- `CreateGraph()` / `CreateNamedGraph()` - Graph creation
- `ProcessSpecification()` - Compile .grg files
- Dynamically loads generated action assemblies

### LGSPGrGen

Code generator invoked during compilation:
- Compiles the intermediate C# action files from the Java frontend into a temporary assembly, then accesses rule and pattern data via reflection
- Generates optimized C# matcher code
- Produces search plans based on graph statistics if provided, otherwise falls back to static plan generation (`GenerateStaticPlanGraph()`)

## Code Generation Pipeline (of rules)

1. Java frontend parses .grg, produces intermediate C# action files (containing the code implementing the rewrite part of the rules)
2. `LGSPGrGen.ProcessSpecification()` compiles the intermediate files into a temporary assembly and accesses rule/pattern data via reflection
3. `PatternGraphAnalyzer` analyzes patterns for optimization
4. `PlanGraphGenerator.GeneratePlanGraph()` builds a plan graph from graph statistics (or `GenerateStaticPlanGraph()` if no statistics are available), then `MarkMinimumSpanningArborescence()` selects the optimal traversal order
5. `SearchPlanGraphGeneratorAndScheduler.GenerateSearchPlanGraph()` converts the plan graph to a search plan graph, then `ScheduleSearchPlan()` produces the scheduled search plan; `ScheduleEnricher` appends homomorphy information — all done recursively for negatives, independents, alternatives, and iterateds via `GenerateScheduledSearchPlans()`
6. `SearchProgramBuilder` generates search program code from the scheduled search plans
7. `SequenceGenerator` generates sequence execution code
8. Generated C# is compiled into action assembly

## Key Design Patterns

- **Search Plans**: Graph patterns compiled to nested loop search programs
- **Type Ringlists**: Fast element iteration by type
- **Ringlist Incidence**: Efficient edge lookup per node (or node by edge)
