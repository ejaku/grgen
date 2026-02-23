# libGrPersistenceProviderSQLite

Persistence provider for loading GrGen graphs from/continuous storing to SQLite databases — an object-relational mapper (ORM) for GrGen graph models.

## Purpose

Enables persistent graph storage using SQLite. The graph is held in memory as usual; on first use the provider reads it from the database (or initialises the database schema), then registers as a listener to IGraph change events and mirrors every subsequent modification to the database. This makes the graph state durable across process restarts, useful for long-running simulations that must survive crashes or intentional shutdowns and be resumed from where they left off; or OLAP sessions. This dll is only loaded when needed at runtime.

## Platform Restriction

**x64 and x86 only** - AnyCPU is not supported due to SQLite native library requirements.

## Output

- `libGrPersistenceProviderSQLite.dll` - Library assembly
- `libGrPersistenceProviderSQLite.xml` - XML documentation (Release builds)

## Dependencies

- `libConsoleAndOS` - Console abstraction
- `libGr` - Core graph interfaces
- `System.Data.SQLite` (NuGet 2.0.2) - SQLite ADO.NET provider
- `SourceGear.sqlite3` (NuGet 3.50.4.5) - SQLite native library

## Key Files

| File | Size | Purpose |
|------|------|---------|
| `PersistenceProviderSQLite.cs` | 73KB | Main provider implementation |
| `PersistenceProviderSQLiteBase.cs` | 19KB | Base class with database infrastructure |
| `ModelInitializerAndUpdater.cs` | 112KB | Graph type model to database model mapper (also adapting the database to graph type model changes) / Database schema management |
| `HostGraphReader.cs` | 46KB | Reads persisted graphs from database |
| `HostGraphCleaner.cs` | 53KB | Garbage-collects the persisted host graph in the database |
| `specifications/RelationalTablesFromGraphModel.mwb` | 19KB | MySQL Workbench schema diagram (located in the repo-level `specifications/` folder) |

## Key Classes

### PersistenceProviderSQLite

Main provider implementing `IPersistenceProvider`:
- Reading a graph from the database and listening to graph change events (can be seen as node/edge CRUD operations, with attributes of scalar, container, and reference types (which are/may point to objects, graphs, nodes, edges))
- Persistence provider transaction management with commit/commit-and-restart (only very limited rollback), mapping to database transactions, which are used for performance, and internally also for data consistency during cleaning and model updating
- Uses prepared statements for performance

### PersistenceProviderSQLiteBase

Base class with shared infrastructure:
- Database connection management
- SQL command execution
- Error handling

### ModelInitializerAndUpdater

Database schema management:
- Creates tables for graph model types
- Updates schema when model changes
- Handles type inheritance mapping

### HostGraphReader

Reads graphs from database:
- Loads nodes and edges with attributes
- Reconstructs topology (graph structure including referenced graphs and objects)
- Handles container attributes (arrays, maps, sets, deques)

### HostGraphCleaner

Garbage-collects (mark-and-sweep style) the persisted host graph in the database: removes unreachable deleted entities and compactifies container storage by replacing accumulated event-change history with the current state.

## Database Schema

The schema (documented in `specifications/RelationalTablesFromGraphModel.mwb`) has three groups of tables:

**Infrastructure tables** (fixed, always present):
- `metadata` — key/value store for schema version and configuration
- `nodes` — topology table for all nodes: `nodeId`, `typeId`, `graphId`, `name`
- `edges` — topology table for all edges: `edgeId`, `typeId`, `sourceNodeId`, `targetNodeId`, `graphId`, `name`
- `graphs` — registry of all graph instances (host graph and referenced subgraphs): `graphId`, `typeId`, `name`
- `objects` — registry of all internal class objects: `objectId`, `typeId`, `name`
- `types` — type registry for all model types (node/edge/object/graph/enum classes): `typeId`, `kind`, `name`
- `attributeTypes` — attribute type registry (also stores enum cases): `attributeTypeId`, `typeId`, `attributeName`, `xgrsType`

**Per-type attribute tables** (one per node/edge/object type, named after the type):
- Primary key is a copy of the global id from the corresponding topology table (`nodes`/`edges`/`objects`)
- One column per scalar or reference attribute of that type, including inherited ones

**Per-attribute container tables** (one per container attribute per type, named `<type>_<attribute>`):
- Owner id column links to the topology table row
- `command` column records the mutation operation (event-sourcing style; compacted by `HostGraphCleaner` on every open)
- Sets: `entryId`, `ownerId`, `command`, `value`
- Maps: `entryId`, `ownerId`, `command`, `key`, `value`
- Arrays/Deques: `entryId`, `ownerId`, `command`, `value`, `key` (key = integer index)

## Usage

Create a persistent graph via shell:
```
new graph Model.gm "graphName" persist with "libGrPersistenceProviderSQLite.dll" to "Data Source=mydb.sqlite" "optional-options"
```

Or via API:
```csharp
backend.CreatePersistentNamedGraph(model, globals, "graphName",
    "libGrPersistenceProviderSQLite.dll", "Data Source=mydb.sqlite", "");
```

## Transactions

Persistence provider transactions (IPersistenceProviderTransactionManager) are mapped to database transactions; the latter ones are also used internally during e.g. cleanup.
Supported are commit and commit-and-restart - rollback is available technically but only used upon a program crash, because the in-memory graph would stay the same, so the data sources would get out of synch / diverge.
They are used in the sequences by the persistence provider transaction angles, not to be mistaken with the normal transaction angles.

## Limitations

- x64/x86 only (not AnyCPU)
- Performance overhead compared to in-memory only graphs
- An assignment to an (e.g. graph typed) attribute can trigger a long-lasting database update
