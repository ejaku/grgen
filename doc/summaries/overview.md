# System Overview

### Languages
- **Graph Model Language** (`.gm`) -- define node/edge types, attributes, inheritance, connection assertions
- **Rule Language** (`.grg`) -- define patterns (LHS), rewrites (RHS), functions, procedures, filter functions, sequences
- **Sequence Language** -- control rule application: chaining, loops, conditionals, backtracking
- **Shell Language** (`.grs`) -- interactive/scripted graph manipulation and rule execution via GrShell

### Tools
- **GrGen.exe** -- compiler driver: invokes Java frontend, compiles generated C# into DLLs
- **GrShell.exe** -- command-line shell for interactive use and scripting
- **GGrShell.exe** -- GUI shell (Windows Forms)
- **yComp** -- Java-based graph viewer (external, communicates via TCP)
- **MSAGL viewer** -- Microsoft graph layout viewer (built-in alternative to yComp)

### Workflow
1. Define graph model (`.gm`) and rules (`.grg`)
2. GrGen.exe compiles them into C# DLLs (model + actions)
3. Load in GrShell or embed in C# application via API
4. Execute sequences to apply rules, inspect/export results
