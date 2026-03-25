# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

The CLAUDE.md summaries/instructions for onboarding AI agents/coding bots should be also useful summaries/overviews for human developers.
In case of AI coding take care of the content of AICodingPolicy.txt (the restriction to limited parts of the project and the mandatory human review).

Key pattern of this/these files: progressive disclosure of more in-depth knowledge with hierarchically organized files only to be read as needed.

## Project Overview

GrGen.NET (graph rewrite generator) is a system software for graph rewriting comprising a compiler at its core, an interactive and scriptable shell interpreter, a debugger offering visual-graphical and step-wise debugging, and an embedded database/ORM-mapper.
It is a tool for transforming graph-structured data using declarative pattern matching and rewrite rules, enriched by advanced data modelling, query-result processing, and imperative and object-oriented programming language constructs.
Its features can also by used via an API in end-user .NET-applications.
Typical uses are in compiler construction, model transformation, computer linguistics/knowledge representation, and engineering domains (in which graph representations of data are natural/intuitive).

**Two main components:**
- **Frontend** (`frontend/`): Java compiler using ANTLR 3.4 that parses `.grg` (rules) and `.gm` (model) files, generates C# code
- **Backend** (`engine-net-2/`): C# engine that compiles the generated code into assemblies and executes graph rewriting operations with the GrShell application (that interprets `.grs` graph rewrite script files)

## Build Commands

### Full Build (Linux)
```bash
./make_linux.sh
```

### Full Build (Windows/Cygwin)
```bash
./make_cygwin_windows.sh
```

For more, see frontend/BUILDING.md and engine-net-2/BUILDING.md

## Running Tests

### Full Semantic Tests (in engine-net-2 directory)
```bash
cd engine-net-2/tests && ./test.sh
```

### Full Compiler Tests (in frontend directory)
```bash
cd frontend/test && ./test.sh
```

### Examples (smoke tests)
```bash
cd engine-net-2/examples && ./test.sh
```

For further test instructions, see frontend/TESTING.md and engine-net-2/TESTING.md

## Architecture

### Directory Structure

```
grgen/
├── frontend/           # Java compiler
│   ├── de/             # Java sources
│   └── test/           # Compiler tests (should_pass/, should_fail/, should_warn/)
├── engine-net-2/       # C# compiler backend and runtime libraries, as well as Graph Rewrite Shells including debugging and graph visualization
│   ├── src/            # Source projects
│   ├── bin/            # Build output (and released binaries)
│   ├── tests/          # Semantic tests
│   └── examples/       # Example scripts
└── ...                 # See CLAUDE-AUX.md for further non-code directories
```

### Frontend Compiler Pipeline

Pretty classical compiler pipeline with a parser creating an abstract syntax tree, reference resolving and type checking on the syntax tree, that is then lowered to an intermediate representation, and a code generator (producing C#).
For more see the CLAUDE.md in frontend.

### C# Application Projects

- **GrGen**: Compiler driver - invokes Java frontend, extends and compiles generated C# code (main implementation in lgspBackend)
- **GrShell**: Command-line shell for interactive graph manipulation and scripting (real implementation in libGrShell)
- **GGrShell**: GUI shell/workbench with Windows Forms

### C# Library Projects

- **libGr**: Core graph library - graph interfaces, pattern matching interfaces, sequence parsing and interpretation
- **lgspBackend**: Search plan backend - generates search programs for pattern matching, also graph implementation and sequence code generation
- **graphViewerAndSequenceDebugger**: sequence execution debugger (text-console) with graph visualization functionality
- **graphViewerAndSequenceDebuggerWindowsForms**: GUI/visual debugger and MSAGL-graph-viewer

Further projects contain cross-platform consoles, a library with the GrShell main functionality, and the SQLite-based persistent graph storage.
These libraries may be all included in end-user applications (used on API level).
For more see the CLAUDE.md in engine-net-2.

### Key Design Pattern

- **Formal language processing**: Generated recursive-descent (potentially backtracking) parsers, nested symbol tables and type checking, syntax directed interpretation/translation
- Backend/Runtime implementation with high-performance graph pattern matching by **scheduled search programs** on specifically tailored **O(1) data structures**; modifications cause change **events**, quite some functionality offered on top of them.

## File Types

- `.gm` - Graph model files (node/edge type definitions)
- `.grg` - Graph rewrite rules (built from patterns, also computations, overall transformations are defined)
- `.grs` - GrShell scripts with sequence interpretation (also used as test scripts) (a reduced version is also used as graph import/export format)
- `.grs.data` - Expected output for test scripts

## Runtime Requirements

- .NET Framework 4.7.2+ or Mono
- On Linux, executables run via `mono`: `mono bin/GrShell.exe script.grs`
- Java to compile a specification or for yComp graph visualization

## Key language concepts

Textual syntax also for graph patterns (example graphlet: `n:Node --> n;`).
C-like syntax for computations (expressions/statements) but Pascal/UML-style declarations `name:type`; curly-braces block nesting.
Rule language semantics: graphs, graph morphism(s) to describe matching, SPO-based graph rewriting, pair star graph grammars.
Top-down pattern matching (binding of pattern elements to graph elements, parameter passing), bottom-up yielding (assigning to def-variables), then rewrite followed by embedded sequences execution.
Query Command separation in the GrGen rule/computation language (carried through to the implementation/runtime) - pattern matching/functions are side-effect free, only rewrites/procedures modify the host graph (the sequences rule application control language only partially follows CQS).

### Brief languages summary/language features overview

Graph model built of class declarations (node/edge/object, with multiple inheritance) containing attribute and method declarations, and optional attribute indices.
Example: `node class N { a:int; as:string; function f(var i:int) : int { return(this.a + i); } }`
Graphlet syntax on left hand side and right hand side of rules with entity declarations that are referenced/used by name.
Nested patterns (e.g. alternative, iterated, negative), and subpattern declarations and uses.
Example: `pattern SpanningTree(root:Node) { iterated { root --> child:Node; st:SpanningTree(child); } }`
Besides default isomorphic can homomorphic matching be requested, plus other advanced matching options.
Rewrite modes/blocks modify and replace: replace adds elements newly declared on RHS, removes unreferenced LHS elements, keeps referenced elements; modify requires explicit delete.
Example: `rule r(m:M) { n:N --> n; modify { delete(n); nnew:N <-- m; } }`
Plus advanced rewrite operations like retyping or edge redirection.
Attribute conditions on LHS `if{ n.a >= 42 - n.f(0); }`, re-evaluation on RHS `eval { n.a = 42; (m.a) = pp(); }`.
Built-in set/map/array/deque containers, user-defined functions and procedures, many built-in functions (esp. neighbourhood and index queries) and procedures, some in packages.
Example: `procedure p(n:N, ref s:set<string>) { for(el:string in s) { n.as = n.as + el; } return; }`
Example: `test t { root:Root; n:N; if { isReachable(n, root); } --- def var numinc:int = countIncident(n); } \ orderAscendingBy<numinc>`
Matches filtering including sorting, lambda expressions, and result array accumulation methods.
Rule usage from sequences which are similar to boolean expressions, with Kleene-star iteration, all-bracketing, plus multiple advanced constructs.
Sublanguages are sequence computations and sequence expressions, the latter including pattern-based queries (esp. with LHS-pattern-only tests).
Shell language with many commands, e.g. new for graph and graph element creation, exec for sequence execution and eval for sequence expression evaluation, import/export of graphs, show for inspection and automated testing.
Example: `exec (::n)=init ;> (::n)=s(::n)* && rr`
Example: `eval [?t\orderAscendingBy<numinc>\keepFirst(3)].map<int>{el:match<t> -> el.n.a}.sum()`
See `doc/summaries/CLAUDE.md` for more on the languages (index of summaries of user manual chapters).

## Documentation

See `CLAUDE-AUX.md` for an overview of the project documentation files (and used technologies).
This includes an index of the CLAUDE.md files contained in the folder structure of the project, covering the user manual, the frontend pipeline steps, and the engine-net-2 projects (normally to be included in-place as needed).
