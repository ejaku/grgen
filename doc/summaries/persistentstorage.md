# Persistent Storage

Three levels of persistence:
- **Import/Export**: serialize/deserialize the full in-memory graph at a point in time
- **Record/Replay**: dump graph to `.grs`, then append ongoing changes; replays the full history
- **Persistent graph**: in-memory graph mirrored to a SQLite database; changes written through immediately

### Creating a Persistent Graph
```
new graph "spec.grg" "graphname" persist with "libGrPersistenceProviderSQLite.dll"
to "Data Source=database.db;Version=3;"
```
- On first open: database is initialized from the model
- On reopen: graph state is read back from database into memory
- Matching occurs entirely in-memory; database is only read once at load time
- Use database transactions (enclosing sequence in transaction) — omitting them causes ~500x slowdown

### Model Updates
When the model changes between sessions, the persistence provider detects differences and:
- Aborts (data loss risk): entity/enum type removed but instances/usages still exist in database
- Asks for confirmation: attribute or enum case removed or changed type
- Auto-updates: safe extensions (new types, new attributes initialized to defaults, new enum cases)
- Does nothing: no relevant changes

`update` persistent graph parameters (passed as semicolon-separated text in the `new graph` command) auto-confirm specific attribute/enum case removals, avoiding interactive prompts — useful for scripted migrations to user machines.

### Graph I/O (Non-Persistent)
Note: `.grs` is used for two distinct things — full GrShell scripts (containing any shell commands) and the graph serialization format (a reduced subset of graph construction commands, readable/writable without the shell).

- `save graph "file.grs"` — save full GrShell session: graph (nodes/edges with persistent names), global variables, and visualization styles; load back with `include`
- `export "file.grs[i][.gz]"` — export graph in the lightweight GRS serialization format (recommended standard; importable at API level without GrShell); `nonewgraph` variant omits the `new graph` command (for `include` only)
- `export "file.grs" skip/type.attr ...` — omit specific attributes from export; use when removing attributes from the model (otherwise import fails on unknown attributes)
- `import "file.grs"` — import from GRS file
- GXL format: `export "file.gxl"` / `import "file.gxl"` (inter-tool exchange format)
- XMI/EMF: frontend converts `.xmi` to `.gm`/`.grs` via `GrGen.exe`

### Record and Replay
- `record "file.grs" start` — start recording all graph changes
- `record "file.grs" stop` — stop recording
- `replay "file.grs"` — replay recorded changes
