# Development Goals and Design Decisions

- GrGen.NET is the successor of GrGen (ICGT 2006), reimplemented in C# for .NET/Mono; open source (LGPL3)
- Evolution: from fixed-shape patterns with limited control sequences to flexibly-shaped patterns (nested and subpatterns) and high programmability (sequences, integrated imperative language), retaining performance
- Development at the abstraction level of graph representations: dedicated syntax and static type checking, carried through to development support (debugger showing graph and processing effects at that level; search plan explanation, profiling instrumentation, and search step statistics)
- Main goals: productivity through expressive declarative pattern-based rules; high-performance matching and rewriting; general-purpose processing through programmability and customizability; debuggability (esp. visual)
- Guideline for new features: can it be made more *general*, while staying *understandable* and *efficiently* implementable?

### Expressiveness
- Declarative pattern and rewrite languages with rich single-element operations, on top of a rich graph model language
- Nested and subpatterns combine patterns functionally: substructures of arbitrary depth and breadth processed with a single rule application (beyond VIATRA2 and GROOVE)
- Sequences combine rules imperatively: the most advanced strategy language among graph rewriting tools (variables of elementary and container types, storages, sequential/logical/conditional/iterative control, sequence definitions); sequence computations for small changes without entering the rule layer; transactions and backtracking for search space crawling and state space enumeration
- General and orthogonal constructs: iterated patterns subsume multinodes, recursive patterns subsume regular path expressions; generic containers (also as attributes); graph type usable as attribute type; OO-like type system
- Price: increased learning effort (the biggest graph rewriting toolbox); pays off for non-small tasks through concise solutions and short development/maintenance
- Fallback to imperative and object-oriented programming where pattern-based rules don't fit (multi-paradigm languages), unlike purely rule-based tools (Henshin, AGG)

### General-Purpose Graph Rewriting
- Requires programmability, genericity, and customizability, in contrast to domain-specific tools (XL for biology, GROOVE for verification)
- Example state space enumeration: no built-in fixed-function enumerator (as in GROOVE/Henshin), but language devices to program one (backtracking, subgraph extraction/comparison/storage); separating backtracking from state materialization gives control over what is a state and lets you unfold deeper with the same memory
- Programmability wins over coarse-grain fixed-function units, as long as it happens on a suitable level of abstraction; languages instead of pre-coded solutions, also for visualization and debugger configuration
- Abstraction and encapsulation units with parameters: rules/tests, subpatterns, functions/procedures (incl. methods), filter functions, sequence definitions
- Built for external use from the start (old GrGen was an internal compiler construction tool for the FIRM IR); uses in computer linguistics, engineering/computational design synthesis, biology, software engineering, model transformation; consistently among the best rated tools at transformation tool contests

### Performance
- Optimized model implementation: type and incidence ringlists; optional attribute and incidence count indices; modest memory (unlike incremental match engines storing all matches, e.g. VIATRA2)
- Compilation of rules into nested-loop matchers (plus pushdown machine for pattern combination), whole-rule-set optimizations like subpattern inlining; cost: slower test-debug cycle than an interpreter, less runtime flexibility
- Types speed up matching (besides modeling help and compile-time error detection)
- Explicit control via sequences and rooted matching from preset parameters (in contrast to implicit graph-grammar control); explicit control also possible from C# etc. via the API
- Host graph sensitive search plans with a cost model; fully automatic (in contrast to Fujaba, no hand-tuning)
- Strength reduction and inlining optimizations; search state space stepping (continue matching where the previous loop iteration stopped) — at least one order of magnitude faster than other tools on Varró's benchmark; parallelization of search-intensive tasks
- Balanced with simplicity: AA-trees for indices, general multi-index queries instead of dedicated spatial indices

### Understandability and Learnability
- Language constructs chosen among several options, preferring ones known from imperative/OO languages and parser generators — to the mind of a programmer (unlike formal-specification-style languages such as GReQL, or functional languages discarding state and overusing higher-order programming); conceptual integrity
- Emphasis on the user manual, as expressiveness and programmability inevitably increase learning effort
- Heavyweight but easily learnable; afterwards a one-day specification change may amount to a man-week or man-month change in a traditional language

### Development Convenience
- Interactive and graphical debugging: matched pattern and rewrite changes visualized in the graph, current rule highlighted in the sequence
- Behaviour as deterministic as possible, so issues can be reproduced
- Typed API of the generated code (named, statically typed entities, IDE code completion), plus a generic name-string based interface for runtime-changing rules (as used by GrShell)
- Graph-oriented programming: lookup of elements by type, incident elements in both directions, unbounded number of incident edges
- No visual rule language/editor: graphical debugging is a must-have, a graphical editor only nice-to-have; textual languages can be generated (also by agentic coding tools like Claude Code), versioned, and diffed, and are cheaper to implement (cf. AGG); an editor could be built on top of the textual format

### Well Founded Semantics
- Formal semantics of a subset specified in the dissertation of Rubino Geiß (2008; graph homomorphisms, denotational evaluation, category theory); rewrite step based on SPO; recursive rules via pair star graph grammars; development has outpaced the formal semantics
- Convenience of using the language prioritized over convenience of formal reasoning (cf. GP for the latter; DPO preferred by theorists but convoluted for simple node deletion)
- Errors caught early: well-founded semantics, static type checking, validation/testing means

### Platform Independence
- C# (and to a small degree Java) compiled to byte code for VMs with large standardized libraries (avoiding the fate of PROGRES)
- Java only needed to build GrGen itself (some parsers) and to run yComp; code generator, generated code, and libraries are entirely .NET; native integration via .NET interop
- Own model layer instead of EMF, optimized for pattern matching performance (ringlists, bidirectionally navigable edges, cheap isomorphy checks and visited flags); MDA considered an illusion except for large program families or infrastructure tasks (now with a new twist due to AI code generators)
- Strong background in formal language processing (ANTLR-based frontend)
- No advanced language-specific features (e.g. LINQ), easing reliable automatic porting (e.g. with converters by Tangible Software Solutions); no single-platform frameworks (WindowsForms also runs on Linux, WPF does not)

### General-Purpose Graph Transformation
- In contrast to model transformation: rich, automatically high-performance pattern matching (EMF is an obstacle) instead of mapping single elements batch-wise offline (ATL, Epsilon); still well suited for model transformation tasks
- In contrast to graph databases (e.g. Neo4J): pattern-based matching with rewrites tightly coupled to the matches, fine-grain spot-wise changes with transactions; in-memory, single user (serialize accesses for multi-user scenarios)
- Usable as application-embedded database: online modifications, persistence via embedded SQLite or change log recording and replay
- Limited by main memory: ~500 million nodes plus 1 billion edges (naked) in 128GB; GRS import at some ten thousand elements per second; scales well to millions of elements, but no dedicated optimizations for huge datasets
- Built for achieving goals in practice: not a triple graph grammar/bidirectional transformation tool, not a showcase for executable theory or visual programming, but a graph processing language with visual debugging environment
