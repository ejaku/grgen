# Persistent Storage

Three levels of persistence:
- **Import/Export**: serialize/deserialize the full in-memory graph at a point in time
- **Record/Replay**: snapshot graph to `.grs` at recording start, then append ongoing changes; replay reconstructs the full recorded change history or only a part of it
- **Persistent graph**: in-memory graph mirrored to a SQLite database; changes written through immediately

### Creating a Persistent Graph
```
new graph "spec.grg" "graphname" persist with "libGrPersistenceProviderSQLite.dll"
to "Data Source=database.db;Version=3;"
```
- On first open: database is initialized from the model
- On reopen: graph state is read back from database into memory
- Use database transactions (enclosing sequence in `<: :>`) — omitting them causes ~500x slowdown
- Even with DB transactions: write-heavy sequences run ~15x slower than on a pure in-memory graph

### Persistent Graph Design
- Changes written through to database by listening to graph change events (graph structure + attribute values)
- Matching occurs entirely in-memory; database is read only once at load time
- At load: mark-and-sweep GC removes objects/nodes/edges not reachable from the host graph; container attributes compactified (change log replayed → current state written anew)
- Only objects reachable from the host graph are stored (not objects held only in sequence variables)
- Zombie nodes/edges: deleted or retyped elements become dangling references; still-referenced zombies reported as warnings at load — assign `null` or remove from container before deletion/retype
- Constraints vs. regular graph:
  - attributes containing node/edge refs require `node edge graph;` declaration
  - no `optimizereuse` (recycled elements conflict with persistent identity)
  - no parallel sequence execution
  - no external attribute types
- SQLite table mapping: topology tables (`nodes`, `edges`, `graphs`, `objects`); per-type attribute tables; separate tables for container attribute change logs; `types`/`attributeTypes`/`metadata` tables
- Offline SQL modification possible (strongly discouraged)

### Model Update
When the model changes between sessions, the persistence provider detects differences and:
- **Aborts** (because of a data loss risk): entity/enum type removed but instances/usages still exist in database
- **Asks for confirmation**: attribute or enum case removed or changed type
- **Auto-updates**: safe extensions — new types, new attributes initialized to GrGen defaults (not model-specified initializers), new enum cases
- **Does nothing**: no changes or no relevant ones (e.g. indices-only changes, which are memory-only)

On abort, a partial model file (name starts with `partial`, database name included) is written — incomplete (no methods/indices, inheritance flattened) but useful for migration.

Warning: after deleting all instances of a type, close the database, reopen (so GC can remove the entities), close again, then remove the type from the model — the intermediate open is required for GC to run.

Kind changes of types and type changes of attributes are modeled as deletion followed by addition.

### Persistent Graph Parameters
Passed as semicolon-separated text in the `new graph` command (third text argument):
- `update/entitytype.attr` — auto-confirm deletion or type change of attribute `attr` from `entitytype`
- `update/enumtype` — auto-confirm deletion of enum cases from `enumtype`
- `update` — auto-confirm all attribute deletions, type changes, and enum case deletions (use with caution)
- `initializeonfailure/enumtype` — initialize attributes of `enumtype` that fail to load (enum case gone) to the enum default value; useful when the database cannot be loaded due to prematurely removed enum cases

### Graph I/O (Non-Persistent)
Note: `.grs` is used for two distinct things — full GrShell scripts (containing any shell commands) and the graph serialization format (a reduced subset of graph construction commands, readable/writable without the shell).

- `save graph "file.grs"` — save full GrShell session: graph (nodes/edges with persistent names), global variables, and visualization styles; load back with `include`
- `export "file.grs[i][.gz]"` — export host graph in the lightweight GRS serialization format (recommended standard; importable at API level without GrShell)
  - `nonewgraph` variant omits the `new graph` command (for `include` only)
  - `skip/type.attr` — omit specific attributes from export; use when removing attributes from the model (otherwise import fails on unknown attributes)
- `export "file.gxl[.gz]"` — GXL format (inter-tool exchange; XML-bloated, barely human-readable; no container or object-type attributes)
- `export "file.xmi[.gz]"` — XMI/EMF format; requires ecore-derived model (underscore-prefixed types, node-type-prefixed edge types (name mangling), `[containment=true]` annotations)
- `export "file.grg[.gz]"` — GRG format: rule with empty pattern and large modify part; no importer exists, not for normal use
- `import "file.grs[i][.gz]"` — import from GRS file; optional model override: `Filename.gm` or `Filename.grg`
- `import "file.gxl[.gz]"` — import GXL; optional model override (use `new graph`, then `import file.gxl YourName.gm`, then `select actions` to apply typed actions)
- `import "file.ecore ... file.xmi"` — ecore+XMI import: creates intermediate `.gm` equivalent to the ecore, then imports XMI instance graph; optionally specify `.grg` for rules to apply
- `import add FileSpec` — import and add to current graph instead of replacing it

### Record and Replay
- `record "file.grs[.gz]" start` / `stop` — start or stop recording all graph changes; without start/stop: toggle
- Recording starts with a full GRS export, then appends ongoing changes; also records `record` statement values from sequences (for state marking)
- Multiple simultaneous recordings to different files are supported
- `recordflush` — flush recording buffers to disk (use to guarantee durability)
- `replay "file.grs[.gz]"` — replay full recording (reconstructs the graph state at recording end)
- `replay "file.grs[.gz]" from "text" to "text"` — optionally restrict to a portion: `from` skips up to (and including) that line; `to` stops before that line; use `record` statement markers as from/to anchors

### Model Migration
Adding types, attributes, or enum cases works out of the box (but note that GrGen defaults are used, not model-specified initializers).

Deletions require preparation (split by storage type):
- **Serialized `.grs` file**: delete instances/usages by rules or sequences, export with `skip` if removing attributes, then remove from model, re-import
- **Persistent database**: delete instances/usages, close+reopen+close (so GC runs), then remove type from model; for attributes: drop from model and confirm at next open, or supply `update` parameters for silent migration
- Enum case removal: first remove all enum case values from attributes, export/close, then remove cases, re-import/open; if cases already removed prematurely, use `initializeonfailure` to recover
- Kind changes and attribute type changes: handled as deletion + addition — handle the deletion part explicitly
