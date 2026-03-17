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
└── ...                 # See CLAUDE.md for the code directories
```

## Documentation Files

- `README.rst` - Project description (github), links to homepage and user manual
- `README.txt` - Installation and usage instructions, brief version notes
- `ChangeLog.txt` - Detailed version history with per-release feature/fix descriptions
- `LICENSE.txt` - Licensing overview: GrGen itself is LGPL v3 (generated code is yours, extensions must be shared), but components have different licenses (yComp: academic use only; MSAGL: MIT; SQLite: public domain; ANTLR: BSD; user manual: CC BY-SA 3.0)
- `COMPONENTS.txt` - Software bill of materials: components, languages, dependencies, build/usage scenarios
- `AICodingPolicy.txt` - AI coding policy: AI-generated code forbidden in project core (compiler, interpreter, core tests), allowed in periphery (GUI, new shell features, unit tests)

## CLAUDE.md Files

The project contains multiple CLAUDE.md files in subfolders summarizing the content of the respective folders, to be used as references when working with code from that folders (together with the CLAUDE.md files from the parent folders).
Also further BUILDING.md files with building instructions and TESTING.md files with testing instructions, as well as TESTGENERATION.md files with instructions on generating tests, the latter reference the markdown files summarizing the contents of user manual chapters of relevance for generating tests in the GrGen-languages.

- `CLAUDE.md` - Project overview, build and test commands, architecture
- `CLAUDE-AUX.md` - This file: auxiliary directories and documentation files overview, plus a CLAUDE.md index and a technologies listing
- `doc/CLAUDE.md` - User manual structure, build commands, LaTeX environment
- `doc/summaries/CLAUDE.md` - Index of per-chapter summary files
- `frontend/CLAUDE.md` - Frontend compiler: architecture, packages
- `frontend/de/unika/ipd/grgen/*/CLAUDE.md` - instructive/overview files per compiler pipeline step
- `engine-net-2/CLAUDE.md` - Backend engine: solution structure, key design patterns
- `engine-net-2/src/*/CLAUDE.md` - instructive/overview files per C# project

## Technologies

- **C#** (.NET Framework 4.7.2) - Backend engine, shell, debugger
- **Java** (1.8+) - Frontend compiler
- **CSharpCC** - Parser generator for C# (sequence/shell/constant parsers)
- **ANTLR** - Parser generator for Java frontend (version 3.4)
- **SQLite** - Persistent graph storage (used by `libGrPersistenceProviderSQLite`)
- **MSAGL** - Graph visualization in debugger (.NET)
- **yComp** - External JAVA graph viewer application
- **Mono** - Cross-platform runtime (Linux)
