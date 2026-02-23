# Project Auxiliary Information

## Directory Structure

```
grgen/
├── doc/                # User manual (LaTeX), see doc/CLAUDE.md
│   └── summaries/      # Per-chapter .md reference cards, see summaries/CLAUDE.md
├── syntaxhighlighting/ # Editor support (vim, Emacs, Notepad++)
├── specifications/     # Feature and architecture specifications
├── todos/              # Feature ideas and TODOs
├── licenses/           # License texts for all components (LGPL, MIT, BSD, etc.)
└── ...                 # See CLAUDE.md for further code directories
```

## Documentation Files

- `README.rst` - Project description (github), links to homepage and user manual
- `README.txt` - Installation and usage instructions, brief version notes
- `ChangeLog.txt` - Detailed version history with per-release feature/fix descriptions
- `LICENSE.txt` - Licensing overview: GrGen itself is LGPL v3 (generated code is yours, extensions must be shared), but components have different licenses (yComp: academic use only; MSAGL: MIT; SQLite: public domain; ANTLR: BSD; user manual: CC BY-SA 3.0)
- `COMPONENTS.txt` - Software bill of materials: components, languages, dependencies, build/usage scenarios
- `AICodingPolicy.txt` - AI coding policy: AI-generated code forbidden in project core (compiler, interpreter, core tests), allowed in periphery (GUI, new shell features, unit tests)

## CLAUDE.md Files

- `CLAUDE.md` - Project overview, build commands, architecture, key design patterns
- `CLAUDE-AUX.md` - This file: auxiliary directories, documentation files, CLAUDE.md index
- `doc/CLAUDE.md` - User manual structure, build commands, LaTeX environment
- `doc/summaries/CLAUDE.md` - Categorized index of per-chapter summary files
- `frontend/CLAUDE.md` - Frontend compiler: architecture, packages, build, tests
- `frontend/test/CLAUDE.md` - Frontend compiler test creation guide
- `frontend/de/unika/ipd/grgen/ast/CLAUDE.md` - Abstract syntax tree classes and structure
- `frontend/de/unika/ipd/grgen/ir/CLAUDE.md` - Intermediate representation classes
- `frontend/de/unika/ipd/grgen/parser/CLAUDE.md` - ANTLR parser/lexer
- `frontend/de/unika/ipd/grgen/be/CLAUDE.md` - Backend code generation
- `frontend/de/unika/ipd/grgen/util/CLAUDE.md` - Utility classes
- `engine-net-2/CLAUDE.md` - Backend engine: solution structure, build, tests
- `engine-net-2/tests/CLAUDE.md` - Backend semantic test creation guide
- `engine-net-2/src/libGr/CLAUDE.md` - Core graph library
- `engine-net-2/src/lgspBackend/CLAUDE.md` - Search plan backend
- `engine-net-2/src/libGrShell/CLAUDE.md` - Shell scripting library
- `engine-net-2/src/GrGen/CLAUDE.md` - Compiler driver
- `engine-net-2/src/GrShell/CLAUDE.md` - Command-line shell
- `engine-net-2/src/GGrShell/CLAUDE.md` - GUI shell/workbench
- `engine-net-2/src/graphViewerAndSequenceDebugger/CLAUDE.md` - (Sequence) debugger and graph visualization
- `engine-net-2/src/graphViewerAndSequenceDebuggerWindowsForms/CLAUDE.md` - Windows Forms debugger GUI and MSAGL graph viewer
- `engine-net-2/src/libConsoleAndOS/CLAUDE.md` - Console/OS abstraction
- `engine-net-2/src/libConsoleAndOSWindowsForms/CLAUDE.md` - Windows Forms console
- `engine-net-2/src/libGrPersistenceProviderSQLite/CLAUDE.md` - SQLite persistence
