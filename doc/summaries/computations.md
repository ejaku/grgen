# Computations (Attribute Evaluation)

### Control Flow
- `if(expr) { ... } else if(expr) { ... } else { ... }` -- braces mandatory
- `switch(expr) { case constExpr { ... } else { ... } }` -- no fall-through
- `while(expr) { ... }`, `do { ... } while(expr)` -- braces mandatory
- `for(v:int in [lower:upper]) { ... }` -- ascending if lower<=upper, else descending
- `for(v:Type in container) { ... }` -- container iteration
- `for(k -> v in map) { ... }` -- map with key
- `for(i:int -> v in array) { ... }` -- array with index
- `for(v:Type in graphQueryFunc(...)) { ... }` -- graph function iteration
- `break;`, `continue;`

### Functions and Procedures
- `function name(params) : ReturnType { stmts }` -- side-effect free, must return value
- `procedure name(params) : (ReturnTypes) { stmts }` -- may modify graph, 0..k returns
- `exec(sequence);` allowed in procedures but not functions
- `lock(expr) { stmts }` -- synchronization

### Local Variables
- `def var v:Type = expr;` -- elementary variable
- `def ref v:Type = expr;` -- container/object variable
- `def n:NodeType = expr;` -- node variable
- `def -e:EdgeType-> = expr;` -- edge variable

### Assertions
- `assert(condition, message, args...)` -- fires when condition false and assertions enabled
- `assertAlways(condition, message, args...)` -- always fires
